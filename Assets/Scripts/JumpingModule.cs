using UnityEngine;

[RequireComponent(typeof(BasicMovementModule))]
public class JumpingModule : MonoBehaviour
{
    private CharacterController controller;
    private BasicMovementModule _basicMovementModule;
    private ApplyMovement _applyMovement;

    private float jumpHeight = 3f;
    private float gravityValue = -9.81f;

    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    private Vector3 playerVelocity;
    private Vector3 lastVelocity;
    private Vector3 move;

    private void OnEnable()
    {
        controller = GetComponent<CharacterController>();
        _basicMovementModule = GetComponent<BasicMovementModule>();
        _applyMovement = GetComponent<ApplyMovement>();

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

        isGrounded = _applyMovement.GroundCheck();

        if (isGrounded)
        {
            Debug.Log("Grounded");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            isGrounded = false;
        }
        else
        {
            playerVelocity = new();
        }

        //Jump control
        playerVelocity.y += gravityValue * Time.deltaTime;
        lastVelocity = playerVelocity;
        Debug.Log("Jumping");
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

    public Vector3 ReturnJumpVelocityModifier()
    {
        try
        {
            return playerVelocity;
        }
        finally
        {
            playerVelocity = Vector3.zero;
        }
    }

}
