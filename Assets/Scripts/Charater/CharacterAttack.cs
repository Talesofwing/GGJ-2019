using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : CharacterComponent {
    public bool Attack { get; set; }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!Attack)
            return;

        if (other.tag == Tags.Character.ToString ()) {
            other.GetComponent<CharacterHealth> ().TakeDamage (1);
        }
    }

}
