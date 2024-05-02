using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Reference")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string attack = "Attack";
    [SerializeField] private string dash = "Dash";

    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction dashAction;

    public Vector2 moveInput { get; private set; }
    public bool attackTriggered { get; private set; }
    public bool dashTriggered { get; private set; }

    public static InputHandler instance { get; private set; }


    private void RegisterInputActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        attackAction.performed += context => attackTriggered = true;
        attackAction.canceled += context => attackTriggered = false;


        dashAction.performed += context => dashTriggered = true;
        dashAction.canceled += context => dashTriggered = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        attackAction.Enable();
        dashAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        attackAction.Disable();
        dashAction.Disable();
    }

    public void SwichInput(string action, bool isEnable)
    {
        InputAction inputAction = playerControls.FindActionMap(actionMapName).FindAction(action);
        if(isEnable == true)
        { inputAction.Enable(); }
        else
        { inputAction.Disable(); }
    }

    public IEnumerator Delay(float delay, string action)
    {
        InputAction inputAction = playerControls.FindActionMap(actionMapName).FindAction(action);
        inputAction.Disable();
        yield return new WaitForSeconds(delay);
        inputAction.Enable();
        StopCoroutine(Delay(delay, action));
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
     
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        attackAction = playerControls.FindActionMap(actionMapName).FindAction(attack);
        dashAction = playerControls.FindActionMap(actionMapName).FindAction(dash);
        RegisterInputActions();

    }


}
