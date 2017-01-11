using UnityEngine;

/**
 * Class use to store spawn data for a checkpoint.
 * The class also contains behvaiour such that when the player touches the checkpoint, they will respawn from there.
 */
public class Checkpoint : MonoBehaviour {
    public Vector3 spawnPosition;   // spawn point
    public Vector3 spawnEuler;      // spawn orientation

    /*
     * This function is called when this collider has begun touching another collider.
     */
    private void OnCollisionStay(Collision collision) {
        // set this checkpoint to be the one the player respawns at
        if (collision.collider.tag == "Player" && GameObject.Find("Game Controller").GetComponent<GameController>().hasCheckpoints) {
            collision.collider.GetComponent<PlayerController>().lastCheckpoint = this;
        }
    }
}
