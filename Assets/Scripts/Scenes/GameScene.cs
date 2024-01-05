using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene {

    public override SceneType SType { get { return SceneType.Game; } } 

    protected override void OnLoad () {
        GameMode gameMode = DataManager.GameplayData.GameplayMode;
        EnterGameData data = new EnterGameData (gameMode);
        GameplayManager.Instance.EnterGame (data);
    }

    protected override void OnUnload () {

    }

}
