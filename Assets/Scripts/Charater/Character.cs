using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private CharacterBody _body;
    [SerializeField] private CharacterUI _ui;
    [SerializeField] private CharacterMotion _motion;
    [SerializeField] private CharacterAttack _attack;
    [SerializeField] private CharacterHealth _health;
    [SerializeField] private CharacterTrigger _trigger;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _holdItem;
    [SerializeField] private GameObject[] _changeLayerObjects;
    [SerializeField] private GameObject _downStarAnim;

    public Transform CacheTransform { get; private set; }
    public int CharacterID { get; private set; }
    public int TeamID { get; private set; }
    public CharacterBody Body { get { return _body; } }
    public bool InDown { get; private set; }
    public bool Puting { get; private set; }

    private float _walkSoundDuration = 0f;
    private const float _walkSoundInterval = 0.75f;

    private bool _hasItem;
    private bool HasItem {
        get { return _hasItem; }
        set {
            _hasItem = value;
            _animator.SetBool (CharacterAnimatonId.HasItem.ToString (), _hasItem);
            _holdItem.SetActive (_hasItem);
        }
    }
    private BaseMaterial _currentMaterial;

    public bool _inGoal = false;

    private void Awake () {
        CacheTransform = this.transform;
        
        this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, -30f);
    }

    public void Init (int characterId, int teamId) {
        CharacterID = characterId;
        TeamID = teamId;

        _body.OnCharacterInit (this);
        _ui.OnCharacterInit (this);
        _motion.OnCharacterInit (this);
        _attack.OnCharacterInit (this);
        _health.OnCharacterInit (this);
        _trigger.OnCharacterInit (this);

#if UNITY_EDITOR
        this.name = "Character " + characterId;
#endif

        int length = _changeLayerObjects.Length;
        for (int i = 0; i < length; i++) {
            if (TeamID == 1) {
                _changeLayerObjects [i].layer =  8;
            } else if (TeamID == 2) {
                _changeLayerObjects [i].layer =  9;
            }
        }

        _walkSoundDuration = _walkSoundInterval;
    }

    private void FixedUpdate () {
        if (GameplayManager.Instance.GameOver)
            return;

        float dt = Time.deltaTime;
        
        Move (dt);
        Attack ();
        Trigger ();
    }

    private void Move (float dt) {
        if (InDown)
            return;

        float horizontal = Input.GetAxisRaw (_motion.Move_Horizontal);
        if (horizontal == 0) {
            return;
        }
        Vector2 movement = new Vector2 (horizontal, 0) * dt * _moveSpeed;
        if (HasItem) {
            movement /= 2f;
        }
        CacheTransform.Translate (movement);
        bool moving = Mathf.Abs (horizontal) > 0;// || Mathf.Abs (vertical) > 0;
        _animator.SetBool (CharacterAnimatonId.Moving.ToString (), moving);

        _body.FaceTo (movement);

        _walkSoundDuration += dt;
        if (_walkSoundDuration >= _walkSoundInterval) {
            AudioManager.Instance.PlaySoundWalk ();
            _walkSoundDuration = 0f;
        }
    }

    private void Attack () {
        if (HasItem)
            return;

        // if (_attack.Attack || _trigger.Pickup || Puting)
        //     return;

        if (Input.GetKeyDown (_motion.Attack)) {
            _attack.Attack = true;
            _animator.SetTrigger (CharacterAnimatonId.Attack.ToString ());
        }
    }

    private void Trigger () {

        // if (_attack.Attack || _trigger.Pickup || Puting)
        //     return;

        if (Input.GetKeyDown (_motion.Trigger)) {
            if (!HasItem) {
                _trigger.Pickup = true;
                _animator.SetTrigger (CharacterAnimatonId.Pickup.ToString ());
            } else {
                Puting = true;
                _animator.SetTrigger (CharacterAnimatonId.Put.ToString ());
            }
        }
    }

    public void AttackFrameEnter () {
        AudioManager.Instance.PlaySoundAttack ();
    }

    public void AttackFrameExit () {
        _attack.Attack = false;
    }

    public void PickupFrameExit () {
        _trigger.Pickup = false;
    }

    public void PutFrameEnter () {
        
        HasItem = false;
        if (!_inGoal) {
            AudioManager.Instance.PlaySoundPut ();
            _currentMaterial.gameObject.SetActive (true);
            Vector2 position = _holdItem.transform.position;
            position.y = _currentMaterial.transform.position.y;
            _currentMaterial.transform.position = position;
            _currentMaterial = null;
        } else {
            AudioManager.Instance.PlaySoundPutInGoal ();
            GameplayManager.Instance.AddMaterial (TeamID, _currentMaterial.Count);
            Destroy (_currentMaterial);
            _currentMaterial = null;
            MaterialManager.Instance.CreateNewMaterial ();
        }
    }

    public void PutFrameExit () {
        Puting = false;
    }

    public void Pickup (BaseMaterial material) {
        _currentMaterial = material;
        HasItem = true;
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == Tags.Team1_Goal.ToString ()) {
            if (TeamID == 1)
                _inGoal = true;
        } else if (other.tag == Tags.Team2_Goal.ToString ()) {
            if (TeamID == 2)
                _inGoal = true;
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other.tag == Tags.Team1_Goal.ToString ()) {
            if (TeamID == 1)
                _inGoal = false;
        } else if (other.tag == Tags.Team2_Goal.ToString ()) {
            if (TeamID == 2)
                _inGoal = false;
        }
    }

    private void OnCollisionEnter2D (Collision2D other) {
        if (other.collider.tag == Tags.Ground.ToString ()) {
            AudioManager.Instance.PlaySoundGround ();
        }
    }

    public void Hited () {

        if (InDown)
            return;

        _animator.SetTrigger (CharacterAnimatonId.Hited.ToString ());
    }

    public void Down () {
        InDown = true;

        _animator.SetTrigger (CharacterAnimatonId.Down.ToString ());
        _downStarAnim.SetActive (false);
        _downStarAnim.SetActive (true);

        if (HasItem) {
            _currentMaterial.gameObject.SetActive (true);
            Vector2 position = _holdItem.transform.position;
            position.y = _currentMaterial.transform.position.y;
            _currentMaterial.transform.position = position;
            _currentMaterial = null;
            _holdItem.SetActive (false);
            HasItem = false;
        }
    }

    public void DownFrameExit () {
        InDown = false;
    }

    public void Win () {
        _animator.SetTrigger (CharacterAnimatonId.Win.ToString ());
    }

    public void Lose () {
        _animator.SetTrigger (CharacterAnimatonId.Lose.ToString ());
    }

}
