using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMaterial : MonoBehaviour {
    [SerializeField] private Vector2 _speedRange = new Vector2 (10f, 15f);
    [SerializeField] private Text _countText;
    
    public int Count { get; private set; }

    protected float _speed;
    protected bool _inGround = false;

    public void Init (int count) {
        _speed = Random.Range (_speedRange.x, _speedRange.y);
        _countText.text = count.ToString ();
        Count = count;

        OnInit (count);
    }
    public virtual void OnInit (int count) {}

    public bool Take () {
        if (!_inGround)
            return false;

        this.gameObject.SetActive (false);

        OnTake ();

        return true;
    }
    public virtual void OnTake () {}

    private void Update () {
        if (_inGround)
            return;

        Vector2 movement = Vector2.down * _speed * Time.deltaTime;
        this.transform.Translate (movement);
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == Tags.Ground.ToString ()) {
            AudioManager.Instance.PlaySoundGround ();
            _inGround = true;
        }
    }

}
