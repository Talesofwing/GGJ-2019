using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrigger : CharacterComponent {
    public bool Pickup { get; set; }

    public override void OnCharacterInit (Character character) {
        base.OnCharacterInit (character);

        transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (!Pickup)
            return;
        
        if (other.tag == Tags.Material.ToString ()) {
            if (other.GetComponent<BaseMaterial> ().Take ()) {
                AudioManager.Instance.PlaySoundPickup ();
                CacheCharacter.Pickup (other.GetComponent<BaseMaterial> ());
            }
        }
    }

}
