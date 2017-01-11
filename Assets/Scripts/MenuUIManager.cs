using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This class manages the UI for the main menu.
 */
public class MenuUIManager : MonoBehaviour {

    public Toggle checkpointToggle; // checkpoints toggle
    public Toggle livesToggle;      // unlimited lives toggle

    private GameController control;

    /*
     * This class manages the UI for a level scene.
     */
    private void Start() {
        // get game controller
        control = GameObject.Find("Game Controller").GetComponent<GameController>();

        // set toggles
        checkpointToggle.isOn = control.hasCheckpoints;
        livesToggle.isOn = control.unlimitedLives;
    }

    /*
     * Turns on/off checkpoints.
     */
    public void SetCheckpoints(bool value) {
        control.SetCheckpoints(value);
    }

    /*
     * Turns on/off infinite lives.
     */
    public void SetLives(bool value) {
        control.SetLives(value);
    }

    /*
     * Load first level.
     */
    public void GoToLevelOne() {
        SceneManager.LoadScene("Level1");
    }

    /*
     * Load second level.
     */
    public void GoToLevelTwo() {
        SceneManager.LoadScene("Level1");
    }

    /*
     * Load third level.
     */
    public void GoToLevelThree() {
        SceneManager.LoadScene("Level1");
    }
}
