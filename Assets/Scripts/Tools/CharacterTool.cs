using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterTool {

    public static Color GetCharacterColorByTeam (int teamId) {
        Color color = new Color ();

        switch (teamId) {
            case 1:
                return new Color (0.7f, 0.21f, 0.05f);
            case 2:
                return new Color (0.04f, 0.44f, 0.52f);
            default:
                return Color.white;
        }

    }

}

public enum CharacterAnimatonId {
    Moving,
    Attack,
    Pickup,
    Put,
    Pickup_Moving,
    HasItem,
    Hited,
    Down,
    Win,
    Lose
}

public enum CharacterFaceDirection {
    Right,
    Left
}