using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler), typeof(Animator))]
public class BasicMovementModule : MonoBehaviour
{
    private InputHandler inputHandler;
    private CharacterController controller;
    private Animator animator;
    private Transform cameraTransform;

    private float basicMovementSpeed = 3f;
    private float rotationSpeed = 5f;
    private float animationSmoothTime = 0.15f;
    private float gravityValue = -9.81f;

    public bool isGrounded = true;

    private Vector3 playerVelocity;
    private Vector3 move;
    private Vector2 currentAnimationBlendVector;
    private Vector2 animationVelocity;

    private int speedXAnimationParameterID;
    private int speedYAnimationParameterID;

    private void OnEnable()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        speedXAnimationParameterID = Animator.StringToHash("MoveX");
        speedYAnimationParameterID = Animator.StringToHash("MoveZ");
        cameraTransform = Camera.main.transform;
        inputHandler = GetComponent<InputHandler>();

        GetComponent<InputHandler>().MovementTriggeredCallBack += MovePlayer;
        Debug.Log("Subscribed movement function to Movement Triggered");
    }

    private void OnDisable()
    {
        GetComponent<InputHandler>().MovementTriggeredCallBack -= MovePlayer;
        Debug.Log("Unsubscribed movement function to Movement Triggered");
    }

    private void MovePlayer()
    {
        
        Vector2 input = GetComponent<InputHandler>().ReturnInputValuesForMovement();
        currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, input, ref animationVelocity, animationSmoothTime);
        Vector3 _lastInput = input;

        move = new(currentAnimationBlendVector.x, 0, currentAnimationBlendVector.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;

        //controller.Move(basicMovementSpeed * Time.deltaTime * move);

        SmoothOutAnimationsInBlendTree();

        //AvoidSlopeBouncing();

        RotateMovementToCameraDirection(_lastInput);
    }

    private void Update()
    {
        //if (isGrounded && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}
        //isGrounded = GroundCheck();

        ////Apply Jump if module installed
        //if (GetComponent<JumpingModule>())
        //{
        //    Debug.Log("Getting Jump Velocity");
        //    playerVelocity = GetComponent<JumpingModule>().ReturnJumpVelocityModifier();
        //}
        //playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);
        //playerVelocity = Vector3.zero;
    }

    private void RotateMovementToCameraDirection(Vector3 _lastInput)
    {
        // Rotate towards camera direction when moving
        if (_lastInput.sqrMagnitude == 0) return;
        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void SmoothOutAnimationsInBlendTree()
    {
        //Smooth Animations for blend tree
        animator.SetFloat(speedXAnimationParameterID, currentAnimationBlendVector.x);
        animator.SetFloat(speedYAnimationParameterID, currentAnimationBlendVector.y);
    }

    private void AvoidSlopeBouncing()
    {
        bool jumpTriggered = inputHandler.JumpTriggeredCheckForDownForce();
        //Avoid bouncing on slopes
        if (isGrounded && !jumpTriggered) //NEED GROUND CHECK HERE TOO
        {
            controller.Move(-Vector3.up * 0.1f);
        }
    }
    private bool GroundCheck()
    {
        Vector3 dir = new(0, -1);
        //THIS ALSO DOESN'T WORK WHY??
        if (Physics.Raycast(transform.position, dir, controller.height * 0.5f + 0.1f))
            return true;
        else
            return false;
    }

    public Vector3 ReturnMoveVector3Values()
    {
        try
        {
            return basicMovementSpeed * Time.deltaTime * move;
        }
        finally
        {
            move = Vector3.zero;
        }
    }

}
