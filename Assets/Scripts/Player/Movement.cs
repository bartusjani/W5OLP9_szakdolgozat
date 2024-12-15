
using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private float horizontal;
    public float speed = 5f;
    private float jumpingPower = 20f;
    private bool isFacingRight = true;
    bool isGrounded = true;
    bool isShiftPressed = false;
    Animator animator;


    private bool canDash=true;
    private bool isDashing;
    private float dashingPower = 400f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxis("Horizontal");

        if(Input.GetButton("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            isGrounded = false;

            animator.SetBool("isJumping",true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        if(Input.GetKeyDown(KeyCode.E) && canDash)
        {  
            StartCoroutine(Dash());
        }
        else if(Input.GetKeyDown(KeyCode.E) && canDash && !isGrounded==false)
        {
            StartCoroutine(JumpDash());
        }

       

        if (Input.GetKey(KeyCode.LeftShift) && IsGrounded())
        {
            Run();
        }
        else
        {
            speed = 5f;
            animator.SetBool("isRunning", false);
        }

        Flip();
        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Run()
    {
        animator.SetBool("isRunning", true);
        speed = 10f;
    }
    private IEnumerator Dash()
    {

        animator.SetBool("isDashing", canDash);
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("isDashing", canDash);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    private IEnumerator JumpDash()
    {
        animator.SetBool("isDashing", canDash);
        animator.SetBool("isJumping", true);
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("isDashing", canDash);
        animator.SetBool("isJumping", false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);


        

    }

    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
