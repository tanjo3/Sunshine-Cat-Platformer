using UnityEngine;

/*
 * Behaviour for the object you need to touch in order to win.
 */
public class WinObject : MonoBehaviour {

    public LevelUIManager ui;   // UI manager for the level

    /*
     * This function is called when this collider has begun touching another collider.
     */
    private void OnCollisionEnter(Collision collision) {
        // attach player as child to object
        if (collision.collider.tag == "Player") {
            ui.SetWinText();
        }
    }
}
