using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// is where the actions declared
public class State 
{
    public Player player;
    public StateMachine stateMachine;

    // protected Vector3 gravityVelocity;
    // protected Vector3 velocity;
    protected Vector2 moveInput;

    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction jumpAction;
    public InputAction sprintAction;

    // other states will use these
    public bool grounded;
    public float gravityValue;
    public Vector3 forceDirection;

    // bind the action with the input through this constructor
    public State(Player player, StateMachine stateMachine)
    {
        this.player = player; 
        this.stateMachine = stateMachine; // the state machine property = state machine parameter

        moveAction = player.playerActionAsset.Player.Move;         // Assign the Move action from the Player map
        sprintAction = player.playerActionAsset.Player.Sprint;
        jumpAction = player.playerActionAsset.Player.Jump;  
    }

    public virtual void Enter()
    {
        Debug.Log("enter state: " + this.ToString());
        moveInput = Vector2.zero;
    }

    public virtual void HandleInput()
    {
    }

    // UPDATE STATE TRANSITION
    public virtual void LogicUpdate()
    {
    }

    // BELONG TO FIXED UPDATE DO NOT CALL IN UPDATE
    public virtual void PhysicsUpdate()
    {
        // handle inputs for moving normally
        forceDirection = forceDirection + moveInput.x * player.movementForce * player.GetCameraRight(player.playerCamera);
        //Debug.Log(move.ReadValue<Vector2>());
        forceDirection = forceDirection + moveInput.y * player.movementForce * player.GetCameraForward(player.playerCamera);

        // Apply the calculated force to the Rigidbody
        player.rb.AddForce(forceDirection, ForceMode.Impulse);

        // Reset the forceDirection to avoid continuous movement without input
        forceDirection = Vector3.zero;

        // CLAMPING THE SPEED OF PLAYER TO MAX SPEED
        // Debug.Log("Old normalized " + player.rb.velocity.normalized); // mag = 1
        // if magnitude of velocity > max speed
        if (player.rb.velocity.sqrMagnitude > player.maxSpeed * player.maxSpeed)
        {
            //Debug.Log(player.rb.velocity.y);

            // Debug.Log("Old: " + player.rb.velocity); // mag > 1
            // then set the velocity to max speed 
            // player.rb.velocity.normalized * player.maxSpeed means: mag = 1 * maxSpeed
            // Vector3.up * player.rb.velocity.y = (0, 1, 0) * vy -> preserving the vertical velocity in case of jumping or falling
            player.rb.velocity = player.rb.velocity.normalized * player.maxSpeed + Vector3.up * player.rb.velocity.y;
            //Debug.Log(player.rb.velocity);
        }

        // Get the current direction of movement (x z of movement)
        Vector3 directionOfMovement = player.rb.velocity;

        // This makes direction only has x and z componenets
        directionOfMovement.y = 0f;
        // Debug.Log("direction " + direction);

        // Only rotate the character if:
        // - there's meaningful input from the player (magnitude > 0.1),
        // - AND the character is actually moving in the world (velocity > 0.1)
        if (moveInput.sqrMagnitude > 0.1f && directionOfMovement.sqrMagnitude > 0.1f)
        {
            // Rotate the Rigidbody to face the movement direction
            // In this case we rotate x and z based on y (y as pivot for rotation)
            this.player.rb.rotation = Quaternion.LookRotation(directionOfMovement, Vector3.up);
            //Debug.Log("LookRotation output: " + Quaternion.LookRotation(directionOfMovement, Vector3.up));
            // WHY: Makes the character "look where it's going," creating a natural visual response to movement
        }
        else
        {
            // If no movement input, stop rotating the character
            player.rb.angularVelocity = Vector3.zero;
            // WHY: Prevents unwanted spinning or rotation when idle
        }
    }

    public virtual void Exit()
    {
    }
}
