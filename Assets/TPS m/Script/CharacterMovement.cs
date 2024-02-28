using System;
using UnityEngine;
using Unity.Netcode;

public class CharacterMovement : NetworkBehaviour
{
    private CharacterController controller;
    private Animator animator;
    public Transform cam;

    public float speed = 3f;
    public float turnSmooth = 0.1f;
    public float turnSmoothVelocity;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    public float gravity = -9.81f;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        // Movement Input 
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            animator.SetFloat("Speed", speed);
        }

        else
        {
            animator.SetFloat("Speed", 0f);
        }

        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask); // Ground Check

        // Gravity apply the player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
