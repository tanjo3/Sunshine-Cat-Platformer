using UnityEngine;

/*
 * Overall behaviour and state of the player.
 */
public class PlayerController : MonoBehaviour {

    [System.Serializable]
    public class TripleJump {
        public float jumpStrength = 4F;             // strength of up force applied when jumping
        public float tripleJumpMultiplier = 1.5F;   // number of times more powerful a triple jump is
        public float jumpTimeBuffer = 1F;           // amount of time between jumps to count as part of a triple jump

        public AudioSource firstJump;   // audio for the first jump
        public AudioSource secondJump;  // audio for the second jump in a row
        public AudioSource thirdJump;   // audio for the third jump in a row
    }

    public LevelUIManager ui;           // UI manager for the level
    public Checkpoint lastCheckpoint;   // last checkpoint player touched
    public float walkSpeed = 2F;        // speed at which the player walks
    public float turnSpeed = 4F;        // speed at which the player turns (rotates)
    public int numLives = 3;            // number of lives the player currently has

    public TripleJump tripleJump;   // triple jump public variables

    private bool isJumping;      // is the player jumping?
    private float lastJumpTime; // time the player last jumped
    private float lastLandTime; // time the player last landed
    private int jumpsInARow;    // number of jumps in a row

    private Rigidbody rb;       // rigidbody component
    private Animator animator;  // animator component

    /* Axes names */
    private const string VERTICAL_AXIS = "Vertical";
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string JUMP_AXIS = "Jump";

    /* Animator hashes */
    private int walkHash = Animator.StringToHash("Walk");
    private int jumpHash = Animator.StringToHash("Jump");

    /*
     * This function is called before the first frame update.
     */
    private void Awake() {
        // get components
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // put player at spawn point
        SpawnPlayer();

        // set number of lives
        GameObject controller = GameObject.Find("Game Controller");
        if (controller) {
            if (controller.GetComponent<GameController>().unlimitedLives) {
                numLives = int.MaxValue;
            } else {
                numLives = GameController.playerLives;
            }
        }
    }

    /*
     * This function can be called multiple times per frame.
     */
    private void FixedUpdate() {
        // check to see if we're on the ground
        CheckIfGrounded();

        // if the player is not currently jumping, check for jump key press
        if (!isJumping && Input.GetButton(JUMP_AXIS)) {
            // make the player jump
            Jump();
        }

        // get movement input values
        float vertical = Input.GetAxisRaw(VERTICAL_AXIS);
        float horizontal = Input.GetAxisRaw(HORIZONTAL_AXIS);

        // move the player
        Move(vertical);

        // rotate the player
        Rotate(horizontal);
    }

    /*
     * Places the player on the spawn point. Resets all values except lives.
     */
    public void SpawnPlayer() {
        transform.parent = null;
        transform.position = lastCheckpoint.spawnPosition;
        transform.eulerAngles = lastCheckpoint.spawnEuler;

        isJumping = false;
        lastJumpTime = -1F;
        lastLandTime = 0F;
        jumpsInARow = 0;

        // disable player if they run out of lives
        if (numLives == 0) {
            ui.SetLoseText();
            gameObject.SetActive(false);
        }
    }

    /*
     * Decrement the number of player lives.
     */
    public void DecrementLives(int amount) {
        // don't decrement if we have infinite lives
        if (numLives != int.MaxValue) {
            // decrement the number of lives, ensuring that it doesn't drop below zero
            numLives -= amount;

            if (numLives < 0) {
                numLives = 0;
            }
        }
    }

    /*
     * Applies a vertical force upwards on the player.
     */
    private void Jump() {
        // time the jump
        lastJumpTime = Time.time;
        CheckForJumpsInRow();

        // add force and play sound
        switch (jumpsInARow) {
        case 1:
            rb.AddForce(new Vector3(0, tripleJump.jumpStrength, 0), ForceMode.Impulse);
            tripleJump.firstJump.Play();
            break;
        case 2:
            rb.AddForce(new Vector3(0, tripleJump.jumpStrength, 0), ForceMode.Impulse);
            tripleJump.secondJump.Play();
            break;
        case 3:
            rb.AddForce(new Vector3(0, tripleJump.jumpStrength * tripleJump.tripleJumpMultiplier, 0), ForceMode.Impulse);
            jumpsInARow = 0;
            tripleJump.thirdJump.Play();
            break;
        default:
            jumpsInARow = 1;
            tripleJump.firstJump.Play();
            break;
        }

        // set flag
        isJumping = true;
        animator.SetTrigger(jumpHash);
    }

    /*
     * Checks to see if our velocity in the y-axis is zero. If so, assume we're grounded.
     */
    private void CheckIfGrounded() {
        if (isJumping && Mathf.Abs(rb.velocity.y) < 0.001F) {
            isJumping = false;

            // time the landing
            lastLandTime = Time.time;
        }
    }

    /*
     * Checks how many times the player has jumped in a row.
     */
    private void CheckForJumpsInRow() {
        if (lastJumpTime < 0F || lastJumpTime - lastLandTime <= tripleJump.jumpTimeBuffer) {
            jumpsInARow++;
        } else {
            jumpsInARow = 1;
        }
    }

    /*
     * Applies a translation to the player.
     */
    private void Move(float amount) {
        // determine the target direction to move
        Vector3 target = transform.forward * amount;

        // apply movement to player transform and animate
        if (target != Vector3.zero) {
            rb.MovePosition(transform.position + target * walkSpeed * Time.deltaTime);
            animator.SetBool(walkHash, true);
        } else {
            animator.SetBool(walkHash, false);
        }
    }

    /*
     * Applies a rotation to the player.
     */
    private void Rotate(float amount) {
        // determine amount to rotate
        Quaternion delta = Quaternion.AngleAxis(amount * turnSpeed, transform.up);

        // apply rotation
        rb.MoveRotation(delta * transform.rotation);
    }
}
