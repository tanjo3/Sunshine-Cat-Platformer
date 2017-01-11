using System.Collections;
using UnityEngine;

/**
 * Behaviour for a colour fade into the object disappearing for a set time.
 */
public class Disappearing : MonoBehaviour {

    private Color startingColor;            // initial color of the object
    public Color endingColor = Color.black; // fade out color

    public float fadeDuration;      // duration of fade-out process
    public float disappearDuration; // duration of time between faded out and resetting
    private WaitForSeconds wait;    // amount of time to wait before reseting after having faded out

    /*
     * This function is called before the first frame update.
     */
    private IEnumerator Start() {
        startingColor = GetComponent<Renderer>().material.color;
        wait = new WaitForSeconds(disappearDuration);

        while (true) {
            yield return StartCoroutine(Fade(fadeDuration));
            yield return StartCoroutine(Disappear(wait));
            ResetObject();
        }
    }

    /*
     * Fades out the color of the object over the set duration.
     */
    private IEnumerator Fade(float duration) {
        float t = 0F;
        float rate = 1F / duration;

        // gradually fade the object's color out over the set duration
        while (t < 1F) {
            t += rate * Time.deltaTime;
            GetComponent<Renderer>().material.color = Color.Lerp(startingColor, endingColor, t);
            yield return null;
        }
    }

    /*
     * Hide the object from the scene for a set time period.
     */
    private IEnumerator Disappear(WaitForSeconds wait) {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return wait;
    }

    /*
     * Reset the object to its initial state.
     */
    private void ResetObject() {
        GetComponent<Renderer>().material.color = startingColor;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
