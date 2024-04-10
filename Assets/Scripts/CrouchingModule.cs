using UnityEngine;

[RequireComponent(typeof(BasicMovementModule))]
public class CrouchingModule : MonoBehaviour
{
    private float crouchingSpeed = 1.5f;

    private void OnEnable()
    {
        GetComponent<InputHandler>().CrouchTriggeredCallback += CrouchMovement;
        Debug.Log("Subscribed crouch function to Crouch Triggered");
    }

    private void OnDisable()
    {
        GetComponent<InputHandler>().CrouchTriggeredCallback -= CrouchMovement;
        Debug.Log("Unsubscribed crouch function to Crouch Triggered");
    }

    private void CrouchMovement()
    {
        Debug.Log("Crouching");
    }
}
