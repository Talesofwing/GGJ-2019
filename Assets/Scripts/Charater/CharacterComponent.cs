using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterComponent : MonoBehaviour {
    public Character CacheCharacter { get; private set; }

    public virtual void OnCharacterInit (Character character) {
        CacheCharacter = character;
    }

}
