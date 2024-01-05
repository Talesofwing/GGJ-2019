using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager> {
    public bool Player1_Horizontal { get; private set; }
    public bool Player1_Vertical { get; private set; }
    public bool Player2_Horizontal { get; private set; }
    public bool Player2_Vertical { get; private set; }

    private void Update () {

        if (Input.GetKeyDown (KeyCode.W)) {
            Player1_Horizontal = true;
        }

        

    }

}
