using System;
using UnityEngine;
using UnityEngine.InputSystem; // Required for the new Input System
// https://www.youtube.com/watch?v=WIl6ysorTE0

public class ThirdPersonMovement : Load // Inherits from a custom class (likely for component loading)
{
    private PlayerInputActions playerActionAsset; // Stores input action map generated from Input System
    private InputAction move; // Reference to the "Move" input action , which is a 2D Vector 

    [SerializeField] protected Rigidbody rb; // Rigidbody for applying movement through physics
    public Rigidbody Rb => rb;
    [SerializeField] protected float movementForce = 10f; // Controls how fast the character moves
    [SerializeField] protected float maxMovementForce = 20f; // Controls how fast the character moves
    [SerializeField] private float jumpForce = 6f;     // Controls how high the character jumps
    [SerializeField] private float maxSpeed = 6f;      // Maximum speed limit for character movement
    private Vector3 forceDirection = Vector3.zero;     // Direction in which movement force is applied

    [SerializeField] protected Camera playerCamera; // Reference to main camera for movement direction

    private bool alreadySprint = false;
    private float groundCheckDistance = 0.3f;

    private void FixedUpdate()
    {
        CameraRelativeMovement();
        AvoidSliding();
        LookAt();
        FallingWithGravity();   
        AvoidRotateWhileJumping();
    }

    // the straight movement direction should correspond to camera direction
    private void CameraRelativeMovement()
    {
        // Adds movement force in the right direction based on camera orientation and player input
        // forceDirection += x * F * r (r  = right vector of the camera (unit vector in world space))
        // forceDirection += y * F * f (f = forward vector of the camera (unit vector in world space))

        // move.ReadValue<Vector2>().x get x component of move input action (2D)
        // move.ReadValue<Vector2>().y get y component of move input action (2D)
        forceDirection = forceDirection + move.ReadValue<Vector2>().x * movementForce * GetCameraRight(playerCamera);
        //Debug.Log(move.ReadValue<Vector2>());
        forceDirection = forceDirection + move.ReadValue<Vector2>().y * movementForce * GetCameraForward(playerCamera);

        // Apply the calculated force to the Rigidbody
        rb.AddForce(forceDirection, ForceMode.Impulse);

        // Reset the forceDirection to avoid continuous movement without input
        forceDirection = Vector3.zero;
    }

    private void AvoidSliding()
    {
        // Clamp the character's horizontal movement speed to avoid sliding or going too fast
        Vector3 horizontalVelocity = rb.velocity;

        // Ignore the vertical component 
        horizontalVelocity.y = 0f;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            // Normalize speed and preserve vertical velocity (e.g., during jumps/falls)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }
    }

    private void LookAt()
    {
        // Get the current direction of movement (x z of movement)
        Vector3 directionOfMovement = rb.velocity;

        // This makes direction only has x and z componenets
        directionOfMovement.y = 0f; 
        // Debug.Log("direction " + direction);

        // Only rotate the character if:
        // - there's meaningful input from the player (magnitude > 0.1),
        // - AND the character is actually moving in the world (velocity > 0.1)
        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && directionOfMovement.sqrMagnitude > 0.1f)
        {
            // Rotate the Rigidbody to face the movement direction
            // In this case we rotate x and z based on y (y as pivot for rotation)
            this.rb.rotation = Quaternion.LookRotation(directionOfMovement, Vector3.up);
            //Debug.Log("LookRotation output: " + Quaternion.LookRotation(directionOfMovement, Vector3.up));
            // WHY: Makes the character "look where it's going," creating a natural visual response to movement
        }
        else
        {
            // If no movement input, stop rotating the character
            rb.angularVelocity = Vector3.zero;
            // WHY: Prevents unwanted spinning or rotation when idle
        }
    }

    private void FallingWithGravity()
    {
        // Add extra downward force to make falling feel snappier and less floaty
        // if the velocity is < 0 meaning the object is falling downward
        if (rb.velocity.y < 0f)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime * 2f;
        }
    }

    // Returns the forward direction of the camera flattened on the Y-axis (prevents upward/downward movement)
    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward; // z axis of camera transform
        forward.y = 0f; // prevents upward/downward movement
        return forward.normalized;
    }

    // Returns the right direction of the camera flattened on the Y-axis
    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right; // x axis of camera transform
        right.y = 0f; // prevents upward/downward movement
        //Debug.Log(right);
        return right.normalized;
    }

    // Called when the script or object is initialized
    protected override void LoadComponent()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock and hide cursor to center screen for third-person camera control

        this.LoadRigidBody();         // Load the Rigidbody component needed for physics-based movement
        this.LoadPlayerInputActions(); // Initialize and assign input actions
        this.LoadMainCamera();         // Find and assign the main camera to calculate movement direction
    }

    // Fetch and store reference to Rigidbody
    protected virtual void LoadRigidBody()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Instantiate the input action map and get the Move action
    protected virtual void LoadPlayerInputActions()
    {
        playerActionAsset = new PlayerInputActions(); // Generated class from Input Actions asset
        move = playerActionAsset.Player.Move;         // Assign the Move action from the Player map
        playerActionAsset.Enable();                   // Enable the input actions so they start receiving input
    }

    // Find the camera in the scene by name (assumes it's named "Main Camera")
    protected virtual void LoadMainCamera()
    {
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Run code when the object is enabled
    private void OnEnable()
    {
        // In computer programming, a callback is executable code that is passed as an argument to other code.
        // use callback to prevent polling 
        // started is a callback of InputAction
        // Think of it like: “Hey Unity, when this event happens (player starts pressing the fire button), please ALSO run this method.”
        playerActionAsset.Player.Jump.started += DoJump; // Bind the jump logic to the jump input action
        playerActionAsset.Player.Sprint.started += DoSprint; // started when press sprint button
        playerActionAsset.Player.Sprint.canceled += SprintStop; // stopped when release sprint button
        playerActionAsset.Player.SprintWithLeftStick.started += DoSprintWithGamePad;
    }

    private void DoSprintWithGamePad(InputAction.CallbackContext context)
    {
        // Debug.Log("Sprint With gamepad called");
        if (alreadySprint == false)
        {
            alreadySprint = true;
            movementForce = maxMovementForce;
        }

        else if (alreadySprint == true)
        {
            movementForce = 10f;
            alreadySprint = false;
        }
    }

    private void SprintStop(InputAction.CallbackContext context)
    {
        movementForce = 10f;
    }

    private void DoSprint(InputAction.CallbackContext context)
    {
        Debug.Log("Sprint");
        movementForce = maxMovementForce;
    }

    // Handles the actual jump logic when triggered
    private void DoJump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump");
        if (IsGrounded()) // Only allow jumping when on the ground
        {
            forceDirection += Vector3.up * jumpForce; // Add upward force to simulate a jump
        }
    }

    // Check if character is standing on the ground using raycast
    public bool IsGrounded()
    {
        // create a new ray that cast from transform + 0.25f in down direction
        Ray ray = new Ray(transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, groundCheckDistance))
        {
            //Debug.Log(isGrounded)
            return true;
        }
        //Debug.Log(isGrounded);

        else
        {
            return false; // No ground detected below
        }
    }

    // Unsubscribe from input event when the object is disabled
    private void OnDisable()
    {
        playerActionAsset.Player.Jump.started -= DoJump; // Prevents memory leaks and event duplication
        playerActionAsset.Player.Sprint.started -= DoSprint;
        playerActionAsset.Player.Sprint.canceled -= SprintStop;
        playerActionAsset.Player.Disable(); // disable the player action asset
        playerActionAsset.Player.Jump.started -= DoSprintWithGamePad;
    }

    protected virtual void AvoidRotateWhileJumping()
    {
        if (!IsGrounded())
        {
            this.rb.constraints |= RigidbodyConstraints.FreezeRotation;
            //movementForce = 1f;
        }

        else if (IsGrounded())
        {
            this.rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        }
    }
}
