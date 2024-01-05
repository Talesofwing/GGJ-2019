using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour {
    
    public abstract SceneType SType { get; }

    public void Load () {
        OnLoad ();
    }
    protected virtual void OnLoad () {}

    public void Unload () {
        OnUnload ();
    }
    protected virtual void OnUnload () {

    }

}
