using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //Movement Variables
    [SerializeField, Range (1, 20)] private float speed = 5;
    [SerializeField, Range(1, 20)] private float jumpForce = 10;
    [SerializeField, Range(0.01f, 1)] private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask isGroundLayer;
    
    //GroundCheck Stuff
    private Transform groundCheck;
    private bool isGrounded = false;

    //Component References
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        //Component References Filled
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //Checking values to ensure non-garbage data
        if (speed <= 0)
        {
            speed = 5;
            Debug.Log("Speed was set incorrectly");
        }

        if (jumpForce <= 0)
        {
            jumpForce = 10;
            Debug.Log("JumpForce was set incorrectly");
        }

        //Creating groundcheck object
        if (!groundCheck)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            groundCheck = obj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Grab horizontal axis - Check Project Settings > Input Manager to see the inputs defined
        float hInput = Input.GetAxis("Horizontal");

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        //Creat a small overlap collider to check if we are touching the ground
        IsGrounded();

        //Animation check for our physics
        if (curPlayingClips.Length > 0)
        {
            if (curPlayingClips[0].clip.name == "BasicAttack")
                rb.velocity = new Vector2(0, rb.velocity.y);
            else
            {
                rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
            }
        }

        //Button Input Checks
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //Basic attack animation
        if (Input.GetButtonDown("Fire1"))
        {
            //Jump attack animation
            if (!isGrounded && curPlayingClips[0].clip.name != "JumpAttack")
            {
                anim.SetTrigger("isJumpAttack");
            }
            else if (!curPlayingClips[0].clip.name.Contains("Attack"))
            {
                anim.SetTrigger("isBasicAttack");
            }
        }

        //Sprite Flipping
        if (hInput != 0) sr.flipX = (hInput < 0);

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
    }
    /// <summary>
    /// This function is used to check if we are grounded. When we jump, we disable checking if we are grounded until our velocity reaches negative on the y-axis. 
    ///This indicates that we are falling and we should start to check if we are grounded again. This is done to prevent us from flipping to grounded when we jump through a platform.
    /// </summary>
    void IsGrounded()
        {
            if (!isGrounded)
            {
                if (rb.velocity.y <= 0)
                {
                    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
                }
            }
            else
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        }

    void IncreaseGravity()
    {
        rb.gravityScale = 10;
    }
}

