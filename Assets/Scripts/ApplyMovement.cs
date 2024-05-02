using UnityEngine;

public class ApplyMovement : MonoBehaviour
{
    private CharacterController controller;
    private InputHandler _inputHandler;
    private BasicMovementModule _basicMovementModule;

    [SerializeField]
    private Vector3 playerVelocity;
    private float vertVelocity;
    private bool isGrounded = true;

    private float gravityValue = 9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _inputHandler = GetComponent<InputHandler>();
        _basicMovementModule = GetComponent<BasicMovementModule>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ground Check
        isGrounded = GroundCheck();
        playerVelocity = _basicMovementModule.ReturnMoveVector3Values();

        if (isGrounded) 
        {
            vertVelocity = 0;
            if (GetComponent<JumpingModule>() && _inputHandler.JumpTriggeredCheckForDownForce()) //If we are jumping we should change the vertical velocity to be that of the jump
            {
                vertVelocity = GetComponent<JumpingModule>().ReturnJumpVelocityModifier();
            }
        }

        //Apply Gravity to velocity
        vertVelocity -= gravityValue * Time.deltaTime;
        playerVelocity.y = vertVelocity;

        //Reduce Slope bounce
        bool jumpTriggered = _inputHandler.JumpTriggeredCheckForDownForce();
        if (isGrounded && !jumpTriggered)
        {
            playerVelocity.y = -0.1f;
        }

        //Do the movings
        controller.Move(playerVelocity * Time.deltaTime);

    }

    public bool GroundCheck()
    {
        Vector3 dir = new(0, -1);
        if (Physics.Raycast(transform.position, dir, controller.height * 0.5f + 0.1f))
            return true;
        else
            return false;
    }

}
