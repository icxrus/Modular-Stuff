using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    //Input Actions - Add all new actions here following the format (action must be present in Player Inputs).
    private InputAction movementAction, jumpAction, runAction, crouchAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        //Assign correct key to Actions - Name must be identical to Player Inputs name.
        movementAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        runAction = playerInput.actions["Run"];
        crouchAction = playerInput.actions["Crouch"];
    }

    #region Delegates
    //Create a subscribable function for each action - Subscribe to function from seperate modules
    public delegate void MovementTriggered();
    public event MovementTriggered MovementTriggeredCallBack;

    public delegate void JumpTriggered();
    public event JumpTriggered JumpTriggeredCallback;

    public delegate void RunTriggered();
    public event RunTriggered RunTriggeredCallback;

    public delegate void CrouchTriggered();
    public event CrouchTriggered CrouchTriggeredCallback;
    #endregion

    private void Update()
    {
        //Invoke each delegate if the corresponding button is triggered
        if (movementAction.triggered)
            MovementTriggeredCallBack?.Invoke();

        if (jumpAction.triggered)
            JumpTriggeredCallback?.Invoke();

        if (runAction.triggered)
            RunTriggeredCallback?.Invoke();

        if (crouchAction.triggered)
            CrouchTriggeredCallback?.Invoke();
    }

}
