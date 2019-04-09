using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 8f;
    public float jumpSpeed = 8f;
    public float wallJumpVerticalSpeed = 8f;
    public float wallJumpHorizontalSpeed = 20f;
    public float runSpeedMultiplier = 1.5f;
    public float wallTouchRadius = 0.6f;

    private bool movementPaused = false;

    private HudManager hud = new HudManager();
    private PlayerAudioGenerator audioGenerator;

    private Rigidbody playerRigidbody;
    private Collider playerCollider;

    private bool playerHasJumped = false;
    private bool jumpedRightWall = false;
    private bool jumpedLeftWall = false;
    private bool doubleJumpAvailable = false;
    private bool enemyKicked = false;


    private bool punchingHand = false;
    private bool punchAvailable = true;
    private bool kickAvailable = true;

    private Animator animator;
    private bool walking = false;
    private bool running = false;
    private bool jumping = false;

    LayerMask leftWall;
    LayerMask rightWall;

    void Start()
    {
        leftWall = LayerMask.GetMask("LeftWall");
        rightWall = LayerMask.GetMask("RightWall");
        audioGenerator = GetComponent<PlayerAudioGenerator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        hud.Refresh();
    }


    void Update()
    {
        if(!movementPaused)
        {
            WalkHandler();
            JumpHandler();
            AttackHandler();
        }
    }

    private void FixedUpdate()
    {
        animator.SetBool("Walking", walking);
        animator.SetBool("Sprinting", running);
        animator.SetBool("Jumping", playerHasJumped);
        animator.SetBool("InAir", !IsGrounded());
        animator.SetBool("PunchingRight", !punchAvailable && !punchingHand);
        animator.SetBool("PunchingLeft", !punchAvailable && punchingHand);
        animator.SetBool("Kicking", !kickAvailable);
    }


    private void WalkHandler()
    {
        running = Input.GetButton("Run");
        walking = IsHoldingWalking();
        float distance = GetWalkHandlerDistance();
        Vector3 movement = GetWalkHandlerMovement(distance);
        animator.SetFloat("Walk", movement.x + movement.y);
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + movement;
        playerRigidbody.MovePosition(newPosition);
    }

    private bool IsHoldingWalking()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.D);
    }

    private float GetWalkHandlerDistance()
    {
        float distance = walkSpeed * Time.deltaTime;
        return IsGrounded() && Input.GetButton("Run") ? distance *= runSpeedMultiplier : distance;
    }

    private Vector3 GetWalkHandlerMovement(float distance)
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * distance, 0f, Input.GetAxis("Vertical") * distance);
        return transform.TransformDirection(movement);
    }

    void JumpHandler()
    {
        if (JumpKeyPressed())
        {
            bool touchingWallLeft = (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), wallTouchRadius, leftWall));
            bool touchingWallRight = (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), wallTouchRadius, rightWall));
            print("LEFT: " + Vector3.left + " RIGHT: " + Vector3.right);


            if (!playerHasJumped)
            {
                if (IsGrounded())
                {
                    playerHasJumped = true;
                    doubleJumpAvailable = true;
                    jumpedRightWall = false;
                    jumpedLeftWall = false;
                    playerRigidbody.velocity += new Vector3(0f, jumpSpeed, 0f);
                    audioGenerator.PlaySound(PlayerAudioIndex.JUMP);
                }
                else
                {
                    if (touchingWallLeft && !jumpedLeftWall)
                    {
                        doubleJumpAvailable = false;
                        playerHasJumped = true;
                        jumpedLeftWall = true;
                        jumpedRightWall = false;
                        //  playerRigidbody.velocity = new Vector3(-wallJumpHorizontalSpeed, wallJumpVerticalSpeed, 0f);
                        Vector3 rbVelocity = transform.TransformDirection(Vector3.right);
                        rbVelocity *= wallJumpHorizontalSpeed;
                        rbVelocity += new Vector3(0f, wallJumpVerticalSpeed, 0f);
                        playerRigidbody.velocity = rbVelocity;
                        audioGenerator.PlaySound(PlayerAudioIndex.JUMP);
                    }
                    if (touchingWallRight && !jumpedRightWall)
                    {
                        doubleJumpAvailable = false;
                        playerHasJumped = true;
                        jumpedRightWall = true;
                        jumpedLeftWall = false;
                        //playerRigidbody.velocity = new Vector3(wallJumpHorizontalSpeed, wallJumpVerticalSpeed, 0f);
                        Vector3 rbVelocity = transform.TransformDirection(Vector3.left);
                        rbVelocity *= wallJumpHorizontalSpeed;
                        rbVelocity += new Vector3(0f, wallJumpVerticalSpeed, 0f);
                        playerRigidbody.velocity = rbVelocity;
                        audioGenerator.PlaySound(PlayerAudioIndex.JUMP);
                    }
                    if (doubleJumpAvailable)
                    {
                        playerHasJumped = true;
                        doubleJumpAvailable = false;
                        jumpedRightWall = false;
                        jumpedLeftWall = false;
                        playerRigidbody.velocity = new Vector3(0f, jumpSpeed, 0f);
                        audioGenerator.PlaySound(PlayerAudioIndex.JUMP);
                    }
                }
            }
        }
        else
        {
            playerHasJumped = false;
        }

    }

    // If an attack key is pressed, go to the method below
    void AttackHandler()
    {
        if (PunchKeyPressed())
        {
            if (punchAvailable && kickAvailable)
            {
                StartCoroutine(Punch());
            }
        }

        if (KickKeyPressed())
        {
            if (punchAvailable && kickAvailable)
            {
                StartCoroutine(Kick());
            }
        }
    }

    private IEnumerator Punch()
    {
        punchAvailable = false;
        punchingHand = !punchingHand;
        audioGenerator.PlaySound(PlayerAudioIndex.PUNCH);
        yield return new WaitForSeconds(.6f);
        punchAvailable = true;
    }

    public bool IsPunching()
    {
        return !punchAvailable;
    }

    public bool IsKicking()
    {
        return !kickAvailable;
    }

    public void EnemeyKicked()
    {
        enemyKicked = true;
    }

    public bool Kicked()
    {
        return enemyKicked;
    }

    //same as punch animation method above
    IEnumerator Kick()
    {
        kickAvailable = false;
        audioGenerator.PlaySound(PlayerAudioIndex.KICK);
        yield return new WaitForSeconds(1.2f);
        kickAvailable = true;
        enemyKicked = false;
    }

    private bool JumpKeyPressed()
    {
        return Input.GetAxis("Jump") > 0f;
    }
    private bool PunchKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private bool KickKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.Mouse1);
    }

    private bool IsGrounded()
    {
        if(IsOnRock())
        {
            return true;
        }
        else
        {
            float distance = playerCollider.bounds.extents.y + 0.1f;
            return Physics.Raycast(transform.position, -Vector3.up, distance);
        }
    }

    private bool IsOnRock()
    {
        float distance = playerCollider.bounds.extents.y + 0.1f;
        LayerMask rock = LayerMask.GetMask("Rock");
        return Physics.Raycast(transform.position, -Vector3.up, distance, rock);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Death")
        {
            audioGenerator.PlaySound(PlayerAudioIndex.DEATH);


            GameObject player = this.gameObject;
            PlayerHealth playerH = (PlayerHealth)player.GetComponent(typeof(PlayerHealth));
            playerH.playerDeath();

        }
    }

    public void ToggleMovement()
    {
        movementPaused = !movementPaused;
    }
}