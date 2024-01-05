using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBody : CharacterComponent {
    [SerializeField] private SpriteRenderer _head;
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private SpriteRenderer _rightHandForward;
    [SerializeField] private SpriteRenderer _rightHandBack;
    [SerializeField] private SpriteRenderer _leftHandForward;
    [SerializeField] private SpriteRenderer _leftHandBack;

    private Transform CacheTransform { get; set; }

    private void Awake () {
        CacheTransform = this.transform;
    }

    public override void OnCharacterInit (Character character) {
        base.OnCharacterInit (character);

        // random color
        // Color headColor = ColorTool.GetRandomColor ();
        // Color bodyColor = ColorTool.GetRandomColor ();
        // Color handForwardColor = ColorTool.GetRandomColor ();
        // Color handBackColor = ColorTool.GetRandomColor ();
        Color color = CharacterTool.GetCharacterColorByTeam (character.TeamID);

        // _head.color = headColor;
        _body.color = color;
        // _rightHandForward.color = handForwardColor;
        // _leftHandForward.color = handForwardColor;
        // _rightHandBack.color = handBackColor;
        // _leftHandBack.color = handBackColor;
        _rightHandBack.color = color;
        _leftHandBack.color = color;
    }

    public void FaceTo (Vector2 dir) {
        Vector3 scale = CacheTransform.localScale;
        if (dir.x > 0) {
            scale.x = Mathf.Abs (scale.x);
        } else if (dir.x < 0) {
            scale.x = -Mathf.Abs (scale.x);
        }
        CacheTransform.localScale = scale;
    } 

    public void FaceTo (CharacterFaceDirection dir) {
        Vector3 scale = CacheTransform.localScale;
        switch (dir) {
            case CharacterFaceDirection.Left:
                scale.x = -Mathf.Abs (scale.x);
                break;
            case CharacterFaceDirection.Right:
                scale.x = Mathf.Abs (scale.x);
                break;
        }
        CacheTransform.localScale = scale;
    }

}
