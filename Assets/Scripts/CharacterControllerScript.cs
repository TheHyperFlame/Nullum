using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public float moveSpeed = 10f;

    bool isFacingRight = true;
    Animator anim;
    public float move;
    public bool isGrounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float checkRadius;
    public int jumpCount;
    public float jumpForce = 300;
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Run();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", isGrounded);
        anim.SetFloat("vSpeed", rigidBody.velocity.y);
        if (!isGrounded)
            return;


    }

    void Update()
    {
        Lunge();
        CursorFlip();
        Jump();
    }

    public void Run()
    {
        move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));

        rigidBody.velocity = new Vector2(move * moveSpeed, rigidBody.velocity.y);
        if (move > 0 && !isFacingRight)
            Flip();
        else if (move < 0 && isFacingRight)
            Flip();
    }

    void Flip()
    {

        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;
    }
    public int lungeImpulse = 1000;
    void Lunge()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !lockLunge)
        {
            lockLunge = true;
            Invoke("LungeLock", 2f);
            anim.StopPlayback();
            rigidBody.velocity = new Vector2(0, 0);
            if (!isFacingRight) { rigidBody.AddForce(Vector2.left * lungeImpulse); }
            else { rigidBody.AddForce(Vector2.right * lungeImpulse); }
        }

    }
    void Jump()
    {
        if ((jumpCount > 0) && Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("Ground", false);
            rigidBody.velocity = Vector2.up * jumpForce;
            jumpCount--;
        }
        if (isGrounded == true)
            jumpCount = 1;
    }
    private bool lockLunge = false;

    void LungeLock()
    {
        lockLunge = false;
    }
    void CursorFlip()
    {
        transform.localScale = new Vector3(Input.mousePosition.x > (Screen.width * 0.5f) ? 1 : -1, 1, 1);
    }

}