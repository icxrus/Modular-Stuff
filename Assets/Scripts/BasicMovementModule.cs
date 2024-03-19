using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class BasicMovementModule : MonoBehaviour
{
    private float basicMovementSpeed = 3f;

    private void OnEnable()
    {
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
        Debug.Log("Pressed WASD, Current movement speed " + basicMovementSpeed);
    }
}
