using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// player is where all control properties are defined and all components loaded
// this is where the callbacks are subscribed
// the loops are called
public class Player : Load
{
    [Header("Controls")]
    public float maxMovementForce = 16f; // Controls how fast the character moves
    public float movementForce; // Controls how fast the character moves
    public float maxSpeed = 6f;      // Maximum speed limit for character movement
    public float defaultMovementForce = 8f;
    private float groundCheckDistance = 0.3f;
    public float jumpForce;
    public float allowedJumpForce = 4f;

    public StateMachine movementSM;
    public IdleState idle;
    public JumpingState jumping;
    //public CrouchingState crouching;
    public LandingState landing;
    public SprintingState sprinting;
    //public SprintJumpState sprintjumping;
    public PlayerInputActions playerActionAsset; // Stores input action map generated from Input System

    [Header("Components")]
    [SerializeField] public Animator animator;
    [SerializeField] public Camera playerCamera;
    [SerializeField] public Rigidbody rb; // Rigidbody for applying movement through physics

    protected override void LoadComponent()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock and hide cursor to center screen for third-person camera control

        //controller = GetComponent<CharacterController>();
        LoadPlayerAnimator();
        //playerInput = GetComponent<PlayerInput>();
        LoadMainCamera();
        LoadPlayerInputActions();
        rb = GetComponent<Rigidbody>();

        movementSM = new StateMachine();
        idle = new IdleState(this, movementSM);
        sprinting = new SprintingState(this, movementSM);
        jumping = new JumpingState(this, movementSM);
        landing = new LandingState(this, movementSM);

        // set starting state as idle
        movementSM.Initialize(idle);
    }

    private void Update()
    {
        movementSM.currentState.HandleInput();

        movementSM.currentState.LogicUpdate();
    }

    // FixedUpdate can run once, zero, or several times per frame, depending on how many physics frames per second are set in the time settings, and how fast/slow the framerate is.
    private void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();
    }

    public Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward; // z axis of camera transform
        forward.y = 0f; // prevents upward/downward movement
        return forward.normalized;
    }

    // Returns the right direction of the camera flattened on the Y-axis
    public Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right; // x axis of camera transform
        right.y = 0f; // prevents upward/downward movement
        //Debug.Log(right);
        return right.normalized;
    }

    private void LoadMainCamera()
    {
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Instantiate the input action map and get the Move action
    protected void LoadPlayerInputActions()
    {
        playerActionAsset = new PlayerInputActions(); // Generated class from Input Actions asset
        playerActionAsset.Enable();                   // Enable the input actions so they start receiving input
    }

    protected virtual void LoadPlayerAnimator()
    {
        animator = transform.GetComponentInChildren<Animator>();
    }

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
}

