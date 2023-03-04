using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float walkSpeed = 400.0F;

    [SerializeField]
    [Range(0.01F, 0.3F)]
    float smoothSpeed = 0.3F;

    [SerializeField]
    bool isFacingRight = true;

    [Header("Jump")]
    [SerializeField]
    float jumpForce = 500.0F;

    [SerializeField]
    float fallMultiplier = 15.0F;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    float jumpGraceTime = 0.25F;

    [Header("Animation")]
    [SerializeField]
    Animator animator;

    Rigidbody2D rb;

    Vector2 gravity;
    Vector2 velocityZero = Vector2.zero;

    float inputX;
    float lastTimeJumpPressed;

    bool isMoving;
    bool isJumpPressed;

    public StarManager sm;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = -Physics2D.gravity;
    }

    /// <summary>
    /// Se ejecuta cuadro a cuadro
    /// </summary>
    void Update()
    {
        HandleInputs();
    }

    void FixedUpdate()
    {
        HandleJump();
        HandleMove();
        HandleFlipX();
    }

    /// <summary>
    /// Verifica las entradas del jugador por cada cuadro
    /// </summary>
    void HandleInputs()
    {
        // Movimiento horizontal
        inputX = Input.GetAxisRaw("Horizontal");
        isMoving = inputX != 0.0F;

        isJumpPressed = Input.GetButtonDown("Jump");
        if (isJumpPressed)
        {
            lastTimeJumpPressed = Time.time;
        }
    }

    /// <summary>
    /// Mueve el personaje horizontalmente
    /// </summary>
    void HandleMove()
    {
        // Calcula la velocidad en X
        float velocityX = inputX * walkSpeed * Time.fixedDeltaTime;
        animator.SetFloat("speed", Mathf.Abs(velocityX));

        // Crea el vector de direccion y asigna la velocidad
        Vector2 direction = new Vector2(velocityX, rb.velocity.y);

        //rb.velocity = direction;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, direction, ref velocityZero, smoothSpeed);
    }

    /// <summary>
    /// Gira el personaje de derecha a izquierda o viceversa cuando esta en movimiento
    /// </summary>
    void HandleFlipX()
    {
        if (isMoving)
        {
            // Calcula hacia donde esta mirando el personaje
            bool facingRight = inputX > 0.0F;
            if(isFacingRight != facingRight)
            {
                // Rota el personaje en el eje Y
                isFacingRight = facingRight;
                Vector2 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
        }
    }

    void HandleJump()
    {
        if(lastTimeJumpPressed > 0.0F && Time.time - lastTimeJumpPressed <= jumpGraceTime)
        {
            isJumpPressed = true;
        }
        else
        {
            lastTimeJumpPressed = 0.0F;
        }

        if (isJumpPressed && IsGrounded())
        {
            rb.velocity += Vector2.up * jumpForce * Time.fixedDeltaTime;
            return;
        }

        if(rb.velocity.y < -0.01F)
        {
            rb.velocity -= gravity * fallMultiplier * Time.fixedDeltaTime;
        }
    }

    bool IsGrounded()
    {
        return
            Physics2D.OverlapCapsule
            (groundCheck.position, new Vector2(0.75F, 0.02F), 
                CapsuleDirection2D.Horizontal, 0.0F, groundMask);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Star"))
        {
            Destroy(other.gameObject);
            sm.starCount++;
        }
    }
}
