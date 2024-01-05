using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI {
    [SerializeField] private Text _team1MaterialText;
    [SerializeField] private Text _team2MaterialText;
    [SerializeField] private Animation _team1Animation;
    [SerializeField] private Animation _team2Animation;

    private void Awake () {
        _team1MaterialText.text = "00000";
        _team2MaterialText.text = "00000";
    }

    public void SetTeam1Material (int count) {
        _team1MaterialText.text = count.ToString ().PadLeft (5, '0');
        _team1Animation.Play ();
    }

    public void SetTeam2Material (int count) {
        _team2MaterialText.text = count.ToString ().PadLeft (5, '0');
        _team2Animation.Play ();
    }

}
