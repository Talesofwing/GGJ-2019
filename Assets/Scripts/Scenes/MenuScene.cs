using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : BaseScene {

    public override SceneType SType { get { return SceneType.Menu; } } 

    public void StartGame (int mode) {
        GameMode gameplayMode = (GameMode)mode;
        DataManager.GameplayData.GameplayMode = gameplayMode;
        SceneManager.LoadScene (SceneType.Game);

        AudioManager.Instance.PlaySoundClick ();
    }

}
