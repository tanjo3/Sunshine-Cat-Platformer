using UnityEngine;

/**
 * This class defines the camera's behaviour.
 */
public class CameraController : MonoBehaviour {

    public Transform target;    // target of camera

    public float distanceBehind;    // distance the camera should be behind target
    public float cameraHeight;      // height the camera should be above target 

    /*
     * This function is called all other Update functions are called.
     */
    private void LateUpdate() {
        // determine the camera's next position
        Vector3 to = target.transform.position - distanceBehind * target.transform.forward;
        to.y += cameraHeight;

        // apply transformation
        transform.position = to;

        // always look at the target
        transform.LookAt(target.transform);
    }
}
