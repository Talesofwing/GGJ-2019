using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager> {
    [SerializeField] private AudioClip _pickupClip;
    [SerializeField] private AudioClip _walkClip;
    [SerializeField] private AudioClip _attackClip;
    [SerializeField] private AudioClip _hurtClip;
    [SerializeField] private AudioClip _downClip;
    [SerializeField] private AudioClip _putClip;
    [SerializeField] private AudioClip _goalClip;
    [SerializeField] private AudioClip _clickClip;
    [SerializeField] private AudioClip _gameOverClip;
    [SerializeField] private AudioClip _groundClip;

    private AudioSource _player;

    private void Awake () {
        _player = GetComponent<AudioSource> ();
    }

    public void PlaySoundPickup () {
        _player.PlayOneShot (_pickupClip, 10f);
    }

    public void PlaySoundAttack () {
        _player.PlayOneShot (_attackClip, 10f);
    }

    public void PlaySoundHurt () {
        _player.PlayOneShot (_hurtClip, 5f);
    }

    public void PlaySoundDown () {
        _player.PlayOneShot (_downClip, 5f);
    }

    public void PlaySoundWalk () {
        _player.PlayOneShot (_walkClip, 2f);
    }

    public void PlaySoundPut () {
        _player.PlayOneShot (_putClip, 10f);
    }

    public void PlaySoundPutInGoal () {
        _player.PlayOneShot (_goalClip);
    }

    public void PlaySoundClick () {
        _player.PlayOneShot (_clickClip, 5f);
    }

    public void PlaySoundGameOver () {
        _player.PlayOneShot (_gameOverClip, 10f);
    }

    public void PlaySoundGround() {
        _player.PlayOneShot (_groundClip, 7f);
    }

}
