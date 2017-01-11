using System.Collections;
using UnityEngine;

/*
 * Behaviour to move an object back and forth a set distance.
 * At the end points, the object flips over 180 degrees.
 */
public class MoveBackAndForthAndFlip : MonoBehaviour {

    public float distance;      // distance for the object to travel
    public float moveSpeedInv;  // speed of back and forth movement (lower value means faster)
    public float flipSpeedInv;  // speed of flipping (lower value means faster)

    private Vector3 startingPosition;   // starting position of the object
    private Vector3 endingPosition;     // position of object after completing movement
    private bool canAttachChild;        // can the object take on children

    /*
     * This function is called before the first frame update.
     */
    private IEnumerator Start() {
        startingPosition = transform.position;
        endingPosition = transform.position + transform.forward * distance;

        // move back and forth, flipping when we hit the ends
        while (true) {
            yield return StartCoroutine(Move(startingPosition, endingPosition, moveSpeedInv));
            yield return StartCoroutine(Flip(flipSpeedInv));
            yield return StartCoroutine(Move(endingPosition, startingPosition, moveSpeedInv));
            yield return StartCoroutine(Flip(flipSpeedInv));
        }
    }

    /*
     * Moves the object from the starting point to the ending point.
     */
    private IEnumerator Move(Vector3 start, Vector3 end, float speed) {
        canAttachChild = true;

        float t = 0F;
        float rate = 1F / speed;

        // move the object incrementally until we reach the end point
        while (t < 1F) {
            t += rate * Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
    }

    /*
     * Flips the object over 180 degrees.
     */
    private IEnumerator Flip(float speed) {
        canAttachChild = false;

        // detach all children
        foreach (Transform child in transform) {
            child.parent = null;
        }


        float t = 0F;
        float degsRate = 1F / speed;

        // end rotation is 180 degrees larger than start rotation
        // rotate on x-axis
        float start = transform.eulerAngles.x;
        float end = transform.eulerAngles.x + 180;

        // rotate the object incrementally until we flip over 180 degrees
        while (t < 1F) {
            t += degsRate * Time.deltaTime;
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(start, end, t), 0, 0);
            yield return null;
        }
    }

    /*
     * This function is called once per frame for every collider that is touching collider.
     */
    private void OnCollisionStay(Collision collision) {
        // attach player as child to object
        if (collision.collider.tag == "Player" && canAttachChild) {
            collision.collider.transform.parent = transform;
        }
    }

    /*
     * This function is called when this collider has stopped touching another collider.
     */
    private void OnCollisionExit(Collision collision) {
        // detach player from object
        if (collision.collider.tag == "Player") {
            collision.collider.transform.parent = null;
        }
    }
}
