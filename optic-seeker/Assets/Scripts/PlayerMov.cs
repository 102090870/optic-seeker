using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public SonarFx cameraobject;
    public EnemyFOVAdvanced Enemybehavior;
    public EnemyFOVAdvanced2 Enemybehavior2;

    [Header("Movement")]
    private float moveSpd;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Inventory")]
    public float Knife;
    public float FuelTank;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYscale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;

    [Header("Slope Handler")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        crouching,
        walking,
        sprinting,
        air
    }

    void Start()
    {
        readyToJump = true;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        // half player height + 0.2f and need the ground to have a layer mask
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 1f + 0.1f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        //handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //crouch
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        //stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        // Mode - Crouching
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpd = crouchSpeed;
        }

        // Mode - Sprinting
        else if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpd = sprintSpeed;
        }

        //Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpd = walkSpeed;
        }

        //Mode - Air
        else
        {
            state = MovementState.air;
            grounded = false;
        }
    }

    private void MovePlayer()
    {
        //calculate mov direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpd * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpd * 10f, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpd * 10f * airMultiplier, ForceMode.Force);

        //turn off gravity while on slope

    }

    private void SpeedControl()
    {
        // limiting speed onSlope
        if (OnSlope() && exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpd)
                rb.velocity = rb.velocity.normalized * moveSpd;
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpd)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpd;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;
        //reset y velo
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool OnSlope()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 1f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Knife")
        {
            Destroy(collision.gameObject);
            Knife++;
        }

        if (collision.gameObject.tag == "Fuel")
        {
            Destroy(collision.gameObject);
            FuelTank++;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            if(Knife > 0.5f)
            {
                cameraobject.enabled = false;
                Enemybehavior.enabled = false;
                Enemybehavior2.enabled = true;
            }
        }
    }
}
