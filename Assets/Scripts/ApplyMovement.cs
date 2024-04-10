using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyMovement : MonoBehaviour
{
    private CharacterController controller;
    private InputHandler _inputHandler;
    private BasicMovementModule _basicMovementModule;

    private Vector3 playerVelocity;
    private bool isGrounded = true;

    private float gravityValue = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _inputHandler = GetComponent<InputHandler>();
        _basicMovementModule = GetComponent<BasicMovementModule>();
    }

    // Update is called once per frame
    void Update()
    {
        //Slam to ground
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        isGrounded = GroundCheck();

        controller.Move(_basicMovementModule.ReturnMoveVector3Values());

        //Apply Jump if module installed
        if (GetComponent<JumpingModule>())
        {
            Debug.Log("Getting Jump Velocity");
            playerVelocity = GetComponent<JumpingModule>().ReturnJumpVelocityModifier();
        }

        //Gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        AvoidSlopeBouncing();

    }
    public bool GroundCheck()
    {
        Vector3 dir = new(0, -1);
        //THIS ALSO DOESN'T WORK WHY??
        if (Physics.Raycast(transform.position, dir, controller.height * 0.5f + 0.1f))
            return true;
        else
            return false;
    }

    private void AvoidSlopeBouncing()
    {
        bool jumpTriggered = _inputHandler.JumpTriggeredCheckForDownForce();

        //Avoid bouncing on slopes
        if (isGrounded && !jumpTriggered) //NEED GROUND CHECK HERE TOO
        {
            controller.Move(-Vector3.up * 0.1f);
        }
    }

}
