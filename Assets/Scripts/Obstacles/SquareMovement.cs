using System.Collections;
using UnityEngine;

/*
 * Behaviour for moving an object in a square path.
 */
public class SquareMovement : MonoBehaviour {

    // four corners of the square
    public enum Corner { BOT_RIGHT, TOP_RIGHT, TOP_LEFT, BOT_LEFT }

    public Corner startingCorner;   // corner at which the object starts
    public float sideLength;        // length of a side of the square
    public float moveSpeedInv;      // speed of back and forth movement (lower value means faster)

    private Vector3 botLeftPos;     // position of object in the bottom-left corner
    private Vector3 topLeftPos;     // position of object in the top-left corner
    private Vector3 botRightPos;    // position of object in the bottom-right corner
    private Vector3 topRightPos;    // position of object in the top-left corner
    private int index;              // index representing one of the four corners

    /*
     * This function is called before the first frame update.
     */
    private IEnumerator Start() {
        // calculate corner positions
        switch (startingCorner) {
        case Corner.BOT_RIGHT:
            botRightPos = transform.position;
            topRightPos = transform.position + sideLength * transform.forward;
            topLeftPos = topRightPos - sideLength * transform.right;
            botLeftPos = transform.position - sideLength * transform.right;
            break;
        case Corner.TOP_RIGHT:
            topRightPos = transform.position;
            topLeftPos = transform.position - sideLength * transform.right;
            botLeftPos = topLeftPos - sideLength * transform.forward;
            botRightPos = transform.position - sideLength * transform.forward;
            break;
        case Corner.TOP_LEFT:
            topLeftPos = transform.position;
            botLeftPos = transform.position - sideLength * transform.forward;
            botRightPos = botLeftPos + sideLength * transform.right;
            topRightPos = transform.position + sideLength * transform.right;
            break;
        case Corner.BOT_LEFT:
            botLeftPos = transform.position;
            botRightPos = transform.position + sideLength * transform.right;
            topRightPos = botRightPos + sideLength * transform.forward;
            topLeftPos = transform.position + sideLength * transform.forward;
            break;
        default:
            botLeftPos = topLeftPos = botRightPos = topRightPos = transform.position;
            break;
        }
        index = ((int) startingCorner + 1) % 4;

        while (true) {
            yield return StartCoroutine(MoveToCorner(index, moveSpeedInv));
            index = (index + 1) % 4;
        }
    }

    /*
     * Moves the object to the corner specified by the index.
     */
    private IEnumerator MoveToCorner(int index, float speed) {
        float t = 0F;
        float rate = 1F / speed;

        Vector3 start, end;
        switch (index) {
        case 0:
            start = botLeftPos;
            end = botRightPos;
            break;
        case 1:
            start = botRightPos;
            end = topRightPos;
            break;
        case 2:
            start = topRightPos;
            end = topLeftPos;
            break;
        case 3:
            start = topLeftPos;
            end = botLeftPos;
            break;
        default:
            yield break;
        }

        // move the object incrementally until we reach the corner
        while (t < 1F) {
            t += rate * Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
    }

    /*
     * This function is called once per frame for every collider that is touching collider.
     */
    private void OnCollisionStay(Collision collision) {
        // attach player as child to object
        if (collision.collider.tag == "Player") {
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
