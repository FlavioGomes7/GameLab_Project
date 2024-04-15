using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            cc.Move(movement * dashSpeed * Time.deltaTime);
            yield return null;
        }


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
        instance = this;
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
