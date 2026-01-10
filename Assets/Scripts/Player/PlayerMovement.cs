using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Transform orientation;
    float inputX;
    float inputY;

    Vector3 moveDirection;
    Rigidbody rb;


    // ground check
    public float playerHeight;
    public float groundDrag; // how much to slow player when on ground
    public LayerMask groundLayer; // layer assigned to ground objects
    bool grounded; // is player on the ground


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // keeps player from falling over

    }

    private void Update(){

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        PlayerInput();
        SpeedLimiter();


        // apply drag
        if (grounded)
        {
            rb.linearDamping = groundDrag; // applies drag when on ground
        }
        else
        {
            rb.linearDamping = 0; // no drag when in air
        }
    }

    private void FixedUpdate()
	{
        MovePlayer();
	}

	// keyboard input function
	private void PlayerInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
    }

    // player movement function
	private void MovePlayer()
	{
        // calculate movement direction - ensures movement in direction in which the camera is facing
        moveDirection = orientation.forward * inputY + orientation.right * inputX;
	    // applies continuous force - in this case helps accelate player in a consistent manner
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedLimiter()
    {
        // takes the velocity of the player 
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed - if player goes over move speed, limit it
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }


}
