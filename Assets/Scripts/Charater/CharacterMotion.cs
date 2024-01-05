using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotion : CharacterComponent {
    [HideInInspector] public string Move_Horizontal = "Horizontal ";
    [HideInInspector] public string Move_Vertical = "Vertical ";
    [HideInInspector] public KeyCode Attack = KeyCode.J;
    [HideInInspector] public KeyCode Trigger = KeyCode.K;

    public override void OnCharacterInit (Character character) {
        base.OnCharacterInit (character);

        Move_Horizontal += character.CharacterID; 
        Move_Vertical += character.CharacterID;

        if (character.CharacterID == 1) {
            Attack = KeyCode.J;
            Trigger = KeyCode.K;
        } else if (character.CharacterID == 2) {
            Attack = KeyCode.Keypad4;
            Trigger = KeyCode.Keypad5;
        }
    }

}
