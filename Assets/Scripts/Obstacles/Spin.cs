using UnityEngine;

/**
 * Simple spin behaviour.
 */
public class Spin : MonoBehaviour {

    public float speed; // spin speed

    /*
     * This function is called every frame.
     */
    private void Update () {
        // simply spin the object
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
