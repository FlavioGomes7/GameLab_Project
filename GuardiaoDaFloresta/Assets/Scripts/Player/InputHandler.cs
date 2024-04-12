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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        attackAction = playerControls.FindActionMap(actionMapName).FindAction(attack);
        dashAction = playerControls.FindActionMap(actionMapName).FindAction(dash);
        RegisterInputActions();

    }

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

    public IEnumerator DashDelay(float delay)
    {
        dashAction.Disable();
        yield return new WaitForSeconds(delay);
        dashAction.Enable();
        StopCoroutine(DashDelay(delay));
    }



    //public static InputHandler instance;

    ////Player Components
    //private CharacterController cc;
    //private Animator animator;

    ////Player Movement 
    //private Vector3 movement;
    //[SerializeField] private float speed;
    //[SerializeField] private float turnSmoothTime;
    //private float turnSmoothVelocity;

    ////Player DashCooldown
    //[SerializeField] private float dashSpeed;
    //[SerializeField] private float dashTime;

    //public bool canReceiveInput;
    //public bool inputReceived;


    //private void Awake()
    //{
    //    instance = this;
    //}

    //private void Start()
    //{
    //    cc = GetComponent<CharacterController>();
    //    animator = GetComponent<Animator>();
    //}

    //private void Update()
    //{
    //    if(movement.magnitude >= 1)
    //    {

    //        cc.Move(movement * speed * Time.deltaTime);
    //        float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
    //        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
    //        transform.rotation = Quaternion.Euler(0f, angle, 0f);

    //    }

    //}

    //public void OnMove(InputAction.CallbackContext value)
    //{
    //    Vector2 direction = value.ReadValue<Vector2>();
    //    movement = new Vector3(direction.x, 0, direction.y);
    //    if(direction.x != 0 || direction.y != 0)
    //    {
    //        animator.SetBool("isWalking", true);
    //    }
    //    else
    //    {
    //        animator.SetBool("isWalking", false);
    //    }
    //}

    //public void OnAttack(InputAction.CallbackContext context)
    //{
    //    if(context.performed)
    //    {
    //        if (canReceiveInput)
    //        {
    //            inputReceived = true;
    //            canReceiveInput = false;
    //        }
    //        else
    //        {
    //            return;
    //        }
    //    }
    //}

    //public void OnDash(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        StartCoroutine(Dash());
    //    }
    //}

    //public void AttackManger()
    //{
    //    if(!canReceiveInput)
    //    {
    //        canReceiveInput = true;
    //    }
    //    else
    //    {
    //        canReceiveInput = false;
    //    }
    //}

    //IEnumerator Dash()
    //{
    //    float startTime = Time.time;

    //    while(Time.time < startTime + dashTime)
    //    {
    //        cc.Move(movement * dashSpeed * Time.deltaTime);
    //        yield return null;
    //    }


    //}


}
