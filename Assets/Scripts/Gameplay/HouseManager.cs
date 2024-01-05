using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoSingleton<HouseManager> {
    [SerializeField] private GameObject[] _team1Houses;
    [SerializeField] private GameObject[] _team2Houses;

    private int _team1Index = -1;
    private int _team2Index = -1;

    private void Awake () {
        for (int i = 0; i < _team1Houses.Length; i++) {
            _team1Houses [i].SetActive (false);
        }
        for (int i = 0; i < _team2Houses.Length; i++) {
            _team2Houses [i].SetActive (false);
        }
    }

    public void AddHouse (int teamId) {
        if (teamId == 1) {
            _team1Index++;
            _team1Houses [_team1Index].SetActive (true);
        } else if (teamId == 2) {
            _team2Index++;
            _team2Houses [_team2Index].SetActive (true);
        }
        if (_team1Index >= _team1Houses.Length - 1) {
            ExitGameData data = new ExitGameData (1);
            GameplayManager.Instance.ExitGame (data);
        }
        if (_team2Index >= _team2Houses.Length - 1) {
            ExitGameData data = new ExitGameData (2);
            GameplayManager.Instance.ExitGame (data);
        }
    }

}
