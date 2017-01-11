using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This class manages the UI for a level scene.
 */
public class LevelUIManager : MonoBehaviour {

    public Text livesText;          // lives text field
    public Text winLoseText;        // text field for win/lose message
    public PlayerController player; // player controller script

    /*
     * This function is called before the first frame update.
     */
    public void Start() {
        // clear out text fields
        livesText.text = "";
        winLoseText.text = "";
    }

    /*
     * This function is called once per frame.
     */
    private void Update() {
        if (player.numLives == int.MaxValue) {
            livesText.text = "Lives: ∞";
        } else {
            livesText.text = "Lives: " + player.numLives;
        }
    }

    /*
     * Sets the losing message.
     */
    public void SetLoseText() {
        winLoseText.text = "You Lost!\nNow that's embarrassing.";
    }

    /*
     * Sets the winning message.
     */
    public void SetWinText() {
        winLoseText.text = "You Won!\nI'm proud of you, buddy.";
    }

    /*
     * Sends the player back to the main menu.
     */
    public void ReturnToMain() {
        SceneManager.LoadScene("Menu");
    }
}
