using UnityEngine;

/*
 * Behaviour for moving back and forth wile spinning.
 */
public class MoveBackAndForthAndSpin : MonoBehaviour {

    public float distance;      // distance for the object to travel
    public float moveSpeedInv;  // speed of back and forth movement (lower value means faster)
    public float spinRate;      // speed of rotation

    private Vector3 startingPosition;   // starting position of the object
    private Vector3 endingPosition;     // position of object after completing movement

    /*
     * This function is called before the first frame update.
     */
    private void Start() {
        startingPosition = transform.position;
        endingPosition = transform.position + transform.right * distance;
    }

    /*
     * This function is called every frame.
     */
    private void Update() {
        // set the object to move back and forth
        transform.position = Vector3.Lerp(startingPosition, endingPosition, Mathf.PingPong(Time.time / moveSpeedInv, 1F));

        // spin the object
        transform.Rotate(Vector3.up, spinRate * Time.deltaTime);
    }
}
