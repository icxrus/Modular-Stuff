using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicMovementModule))]
public class JumpingModule : MonoBehaviour
{
    private float jumpingHeight = 3f;

    private void OnEnable()
    {
        GetComponent<InputHandler>().JumpTriggeredCallback += Jumping;
        Debug.Log("Subscribed jump function to Jump Triggered");
    }

    private void OnDisable()
    {
        GetComponent<InputHandler>().JumpTriggeredCallback -= Jumping;
        Debug.Log("Unsubscribed jump function to Jump Triggered");
    }

    private void Jumping()
    {
        Debug.Log("Jumping");
    }
}
