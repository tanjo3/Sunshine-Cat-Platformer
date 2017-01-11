using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * General game behaviour.
 */
public class GameController : MonoBehaviour {

    public const int playerLives = 3;   // starting number of lives the player has

    public bool hasCheckpoints; // whether checkpoints will be enabled
    public bool unlimitedLives; // whether the player has unlimited lives

    /*
     * This function is called before the first frame update.
     */
    private void Awake() {
        hasCheckpoints = true;
        unlimitedLives = true;

        // keep game controller between scenes
        DontDestroyOnLoad(transform.gameObject);

        // go to main menu
        SceneManager.LoadScene("Menu");
    }

    /*
     * Turns on/off checkpoints.
     */
    public void SetCheckpoints(bool value) {
        hasCheckpoints = value;
    }

    /*
     * Turns on/off infinite lives.
     */
    public void SetLives(bool value) {
        unlimitedLives = value;
    }
}
