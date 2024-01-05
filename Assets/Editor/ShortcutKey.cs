using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class ShortcutKey {

	[MenuItem("Kuma/UnityEditorHelper/Activate &%A")]
	private static void ActivateSelectedGameObjects () {
		int length = Selection.gameObjects.Length;
		for (int i = 0; i < length; i++) {
			GameObject go = Selection.gameObjects [i];
			if (go != null) {
				bool active = !go.activeSelf;
				Undo.RecordObject (go, "set active");			
				go.SetActive (active);
			}

		}
		
	}

	[MenuItem ("Kuma/UnityEditorHelper/ClearPlayerPrefs %&Q")]
	private static void Clear () {
		PlayerPrefs.DeleteAll ();

		Debug.Log("Clear \"PlayerPrefs\" finished .");
	}

}
