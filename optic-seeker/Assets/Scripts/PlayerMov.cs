using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZDoor;

public class PlayerMov : MonoBehaviour
{

    public SonarFx cameraobject;
    public EnemyFOVAdvanced Enemybehavior;
    public EnemyFOVAdvanced2 Enemybehavior2;

    [Header("Hidden")]
    public bool isHidden = false;


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
    public Image knifeimagebackground;
    public RawImage knifeimage;
    public Image fuelimagebackground;
    public RawImage fuelimage;
    public Image keyimagebackground;
    public RawImage keyimage;
    public float Knife;
    public float FuelTank;
    public float Key;

    [Header("Health")]
    public Image healthBar;
    public float healthAmount;
    private float invulnerability = 1;
    public float invulnerabilitytimer;

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


    public Key RustyKey;
    public string playerTag;
    private KeyContainer keyContainer;
        
    [Header("Car Text")]
    public GameObject FuelText;
    public GameObject noFuelText;

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
        if (healthAmount <= 0)
        {
            Death();
        }
            
        if (invulnerability > 0)
        {
            invulnerability -= Time.deltaTime;
        }

        // half player height + 0.2f and need the ground to have a layer mask
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 1f + 0.1f, whatIsGround);
        if (isHidden == false)
        {
            MyInput();
        }
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
        if (isHidden == false)
        {
            MovePlayer();
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
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
        else if (grounded && Input.GetKey(sprintKey))
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
        if (collision.gameObject.tag == "Knife")
        {
            knifeimage.enabled = true;
            knifeimagebackground.enabled = true;
            Destroy(collision.gameObject);
            Knife++;
        }


        if (collision.gameObject.tag == "Fuel")
        {
            fuelimage.enabled = true;
            fuelimagebackground.enabled = true;
            Destroy(collision.gameObject);
            FuelTank++;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            if (Knife > 0.5f)
            {
                Knife = 0;
                knifeimage.enabled = false;
                knifeimagebackground.enabled = false;
                cameraobject.enabled = false;
                Enemybehavior.enabled = false;
                Enemybehavior2.enabled = true;
            }

            if (Knife < 0.5f && invulnerability <= 0)
            {
                TakeDamage(20);
                invulnerability = invulnerabilitytimer;
                    
            }
        }

        if (collision.gameObject.tag == "Key")
        {
            keyContainer.keys.Add(RustyKey);
            Destroy(collision.gameObject);
            keyimage.enabled = true;
            keyimagebackground.enabled = true;
            Key++;
        }

        if (collision.gameObject.tag == "Interactable")
        {
            if (Key > 0.5f)
            {
                keyimage.enabled = false;
                keyimagebackground.enabled = false;
                //Debug.Log("DOOR HIT");
            }
        }
    }

    public void TakeDamage(float Damage)
    {
        healthAmount -= Damage;
        healthBar.fillAmount = healthAmount / 100;
    }

    private void Death()
    {
        //Put function here to happen when the player runs out of health
        Destroy(gameObject);

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            if (FuelTank > 0.5f)
            {
                fuelimage.enabled = false;
                fuelimagebackground.enabled = false;
                FuelText.SetActive(true);

            }
            else
            {

                noFuelText.SetActive(true);

            }
        }
    }
        
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            noFuelText.SetActive(false);
            FuelText.SetActive(false);

        }
    }


    private void Awake()
    {
         keyContainer = GameObject.FindWithTag(playerTag).GetComponent<KeyContainer>();
    }
}

