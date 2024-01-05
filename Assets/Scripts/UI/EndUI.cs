using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : BaseUI {
    [SerializeField] private Text _titleText;

    public void Init (int teamId) {
        _titleText.text = "PLAYER " + teamId + " WIN!";
    }

    public void Restart () {
        SceneManager.LoadScene (SceneType.Menu);

        AudioManager.Instance.PlaySoundClick ();
    }

}
