using UnityEngine;

[RequireComponent(typeof(BasicMovementModule))]
public class JumpingModule : MonoBehaviour
{
    private CharacterController controller;
    private BasicMovementModule _basicMovementModule;

    private float jumpHeight = 3f;
    private float gravityValue = -9.81f;

    private bool isGrounded;

    [SerializeField]
    private Vector3 playerVelocity;
    private Vector3 oldVelocity;
    private Vector3 move;

    private void OnEnable()
    {
        controller = GetComponent<CharacterController>();
        _basicMovementModule = GetComponent<BasicMovementModule>();

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
        
        isGrounded = GroundCheck();

        if (isGrounded)
        {
            Debug.Log("Grounded");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            isGrounded = false;
        }

        //Jump control
        playerVelocity.y += gravityValue * Time.deltaTime;
        playerVelocity = new();
        Debug.Log("Jumping");
    }

    private bool GroundCheck()
    {
        Vector3 dir = new(0, -1);
        //THIS ALSO DOESN'T WORK WHY??
        if (Physics.Raycast(transform.position, dir, controller.height * 0.5f + 0.3f))
            return true;
        else
            return false;
    }

    public Vector3 ReturnJumpVelocityModifier()
    {
        return playerVelocity;
    }

}
