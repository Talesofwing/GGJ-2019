using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager {
    private const string k_Mode = "GameMode";

    public static TempData GameplayData { get; private set; }

    public static void Load () {
        GameplayData = new TempData ();
    }

}
