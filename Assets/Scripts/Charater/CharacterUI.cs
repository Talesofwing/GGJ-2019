using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : CharacterComponent {
    [SerializeField] private Text _playerIDText;
    [SerializeField] private Outline _outline;

    public override void OnCharacterInit (Character character) {
        base.OnCharacterInit (character);

        _playerIDText.text = "PLAYER " + character.CharacterID;
        _outline.effectColor = CharacterTool.GetCharacterColorByTeam (character.TeamID);
    }
    
}
