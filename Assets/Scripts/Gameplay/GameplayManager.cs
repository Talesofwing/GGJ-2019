using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnterGameData {
    public GameMode GameplayMode;

    public EnterGameData (GameMode mode) {
        GameplayMode = mode;
    }

}

public struct ExitGameData {
    public int WinTeam;

    public ExitGameData (int winTeam) {
        WinTeam = winTeam;
    }

}

public class GameplayManager : MonoSingleton<GameplayManager> {
    private const int MaterialCount = 10;

    [SerializeField] private Transform _team1Spawn1;
    [SerializeField] private Transform _team2Spawn1;
    [SerializeField] private GameUI _ui;
    [SerializeField] private EndUI _endUI;
    [SerializeField] private GameObject _team1Light;
    [SerializeField] private GameObject _team2Light;

    private int _team1Material = 0;
    private int _team2Material;
    private Character _team1Character;
    private Character _team2CHaracter;

    public bool GameOver { get; private set; }

    public void EnterGame (EnterGameData data) {
        switch (data.GameplayMode) {
            case GameMode.ONE_VS_ONE:
                ONEVSONE ();
                break;
            case GameMode.TWO_VS_TWO:
                TWOVSTWO ();
                break;
        }
    }

    private void ONEVSONE () {
        Character characterPrefab = Resources.Load<Character> (GameContstant.Path.PATH_CHARACTER + "Character");
        // 創建角色
        _team1Character = Instantiate<Character> (characterPrefab);
        _team1Character.CacheTransform.SetParent (_team1Spawn1);
        _team1Character.CacheTransform.localPosition = Vector3.zero;
        _team1Character.Init (1, 1);
        _team1Character.Body.FaceTo (CharacterFaceDirection.Right);

        _team2CHaracter = Instantiate<Character> (characterPrefab);
        _team2CHaracter.CacheTransform.SetParent (_team2Spawn1);
        _team2CHaracter.CacheTransform.localPosition = Vector3.zero;
        _team2CHaracter.Init (2, 2);
        _team2CHaracter.Body.FaceTo (CharacterFaceDirection.Left);

        GameStart ();
    }

    private void TWOVSTWO () {

    }

    private void GameStart () {
        GameOver = false;

        MaterialManager.Instance.CreateNewMaterial ();
        HouseManager.Instance.AddHouse (1);
        HouseManager.Instance.AddHouse (2);
    }

    public void ExitGame (ExitGameData data) {
        GameOver = true;

        if (data.WinTeam == 1) {
            _team2CHaracter.Lose ();
            _team1Character.Win ();
            _team1Light.SetActive (true);
        } else if (data.WinTeam == 2) {
            _team2CHaracter.Win ();
            _team1Character.Lose ();
            _team2Light.SetActive (true);
        }

        _endUI.Init (data.WinTeam);

        StartCoroutine (DelayToShowEndUI ());

        AudioManager.Instance.PlaySoundGameOver ();
    }

    private IEnumerator DelayToShowEndUI () {
        yield return new WaitForSeconds (2f);

        _endUI.gameObject.SetActive (true);
    }

    public void AddMaterial (int teamId, int count) {
        if (teamId == 1) {
            _team1Material += count;
            if (_team1Material >= MaterialCount) {
                _team1Material -= MaterialCount;
                HouseManager.Instance.AddHouse (1);
            }
            _ui.SetTeam1Material (_team1Material);
        } else if (teamId == 2) {
            _team2Material += count;
            if (_team2Material >= MaterialCount) {
                _team2Material -= MaterialCount;
                HouseManager.Instance.AddHouse (2);
            }
            _ui.SetTeam2Material (_team2Material);
        }
    }

}
