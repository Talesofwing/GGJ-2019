using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneManager {
    public static BaseScene CurrentScene { get; private set; }

    public static void LoadScene (SceneType sceneType) {
        BaseScene scene = GameObject.Instantiate<BaseScene> (Resources.Load<BaseScene> (GameContstant.Path.PATH_SCENE + sceneType.ToString ()));
        if (scene == null) {
            Debug.LogError ("找不到該場景: " + sceneType.ToString ());
            return;
        }
        if (CurrentScene != null) {
            CurrentScene.Unload ();
            GameObject.Destroy(CurrentScene.gameObject);
        }
        CurrentScene = scene;
        CurrentScene.Load ();
    }

    public static void UnloadScene () {
        if (CurrentScene == null)
            return;

        CurrentScene.Unload ();
        CurrentScene = null;
    }

}
