using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : CharacterComponent {
    [SerializeField] private int _startingHealth = 10;

    private int _currentHealth;

    public override void OnCharacterInit (Character character) {
        base.OnCharacterInit (character);

        _currentHealth = _startingHealth;
    }

    public void TakeDamage (int damage) {
        if (CacheCharacter.InDown)
            return;

        _currentHealth -= damage;

        if (_currentHealth <= 0) {
            AudioManager.Instance.PlaySoundDown ();
            CacheCharacter.Down ();
            _currentHealth = _startingHealth;
        } else {
            AudioManager.Instance.PlaySoundHurt ();
            CacheCharacter.Hited ();
        }
    }

}
