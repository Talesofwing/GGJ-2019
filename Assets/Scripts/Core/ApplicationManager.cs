using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoSingleton<ApplicationManager> {

    private void Awake () {
        DataManager.Load ();

        SceneManager.LoadScene (SceneType.Menu);
    }

}
