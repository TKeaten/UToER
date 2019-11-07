using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Transform firePoint;
    public float runSpeed = 40f;
    public Animator animator;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        //Setting the animator variable "Speed" for registering when we are running
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            firePoint.position = new Vector3(firePoint.position.x, firePoint.position.y - .125f, firePoint.position.z);
        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            firePoint.position = new Vector3(firePoint.position.x, firePoint.position.y + .125f, firePoint.position.z);
        }
    }


    private void FixedUpdate()
    {
        // Move our character, ensuring it only moves at a certain rate no matter how many times the class is called during a timeframe
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    // This is made to trigger on an event which is created in the Character Controller script.
    public void OnLanding() 
    {
        animator.SetBool("IsJumping", false);
    }

    // This is made to trigger on an event which is created in the Character Controller script.
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
}
