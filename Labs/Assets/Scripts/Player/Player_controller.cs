using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class Player_controller : MonoBehaviour
{
    //Component references
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    //Movement variables
    [Range(.5f, 10)]
    public float speed = 6.5f;
    [Range(.5f, 10)]
    public float jumpForce = 6.5f;

    //Ground check variables
    Transform groundCheck;
    bool isGrounded = false;
    [Range(0.01f, 0.1f)]
    public float groundCheckRadius = 0.02f;
    public LayerMask isGroundLayer;

    bool isFiring = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // ground check initialization
        GameObject newGameObject = new GameObject(); // creates new game object in scene and names it 
        newGameObject.transform.SetParent(transform); // childs the new game object under what ever uses the player controller script
        newGameObject.transform.localPosition = Vector3.zero; // Zeros the new object location local to its' parent
        newGameObject.name = "GroundCheck"; // renames the game object in for the hierarchy
        groundCheck = newGameObject.transform; // Sets and returns the ground check objects trans values to global variable.
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGrounded();

        float hInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire1")) // Firing
        {
            isFiring = true;
        }
        else isFiring = false;

        // sprite flipping
        if (hInput != 0) { sr.flipX = (hInput < 0);  }

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsFiring", isFiring);
    }
    void CheckIsGrounded()
    {
        if (!isGrounded)
        {
            if (rb.velocity.y <= 0 && !isGrounded)
               isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer); // Should set the global bool value based on overlap and layer mask.
        }
        else
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
    }
}
