using UnityEngine;

/**
 * Behaviour for an object which will respawn the player upon touching them.
 */
public class DeathCollider : MonoBehaviour {

    /*
     * This function is called when the Collider other enters the trigger.
     */
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<PlayerController>().DecrementLives(1);
            other.GetComponent<PlayerController>().SpawnPlayer();
        }
    }
}
