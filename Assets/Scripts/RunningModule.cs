using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicMovementModule))]
public class RunningModule : MonoBehaviour
{
    private float runningSpeed = 6f;

    private void OnEnable()
    {
        GetComponent<InputHandler>().RunTriggeredCallback += PlayerRun;
        Debug.Log("Subscribed run function to Running Triggered");
    }

    private void OnDisable()
    {
        GetComponent<InputHandler>().RunTriggeredCallback -= PlayerRun;
        Debug.Log("Unsubscribed run function to Running Triggered");
    }

    private void PlayerRun()
    {
        Debug.Log("Running");
    }
}
