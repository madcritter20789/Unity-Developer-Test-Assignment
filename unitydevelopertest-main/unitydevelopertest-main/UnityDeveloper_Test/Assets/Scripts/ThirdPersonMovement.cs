using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;      //Refernce to CharacterController
    public Transform cam;                       //Refrence to Main Camera
    public Animator animator;                   //Reference to Animator 
    public float moveSpeed = 5f;                //Speed of character for movement
    public float jumpHeight = 1.5f;             //Height allowed to jump
    public float gravity = -9.81f;              //Gravity affecting the character

    private Vector3 velocity;                   //Velocity of character when it runs
    private bool isGrounded;                    //To check if player is on ground or not

    public float turnSmoothTime = 0.1f;         //Time to turn charcter smoothly
    float turnSmoothVelocity;                   //Spped to turn character smoothly

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //checked if player is touching the ground
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Take input for movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        //Move the character according to input
        if (direction.magnitude >= 0.1f)
        {
            //Used to move character where camera is facing and turn the character smoothly
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg+cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

            //Change animation state to running
            animator.SetBool("isRunning", true);
        }
        else
        {
            //Change animation state to not running
            animator.SetBool("isRunning", false);
        }


        //Jump the chanracter usimg Space button
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
