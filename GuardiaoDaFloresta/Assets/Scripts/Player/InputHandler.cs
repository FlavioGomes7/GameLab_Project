using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    //Player Components
    private CharacterController cc;
    private Animator animator;

    //Player Movement 
    private Vector3 movement;
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime;
    [SerializeField] private float turnSmoothVelocity;

    //Player Stats
    private int maxHealth;
    private int currentHealth;
    [SerializeField] private int attackDamage;

    public void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if(movement.magnitude >= 1)
        {
            cc.Move(movement * speed * Time.deltaTime);
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
       
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 direction = value.ReadValue<Vector2>();
        movement = new Vector3(direction.x, 0, direction.y);
        if(direction.x != 0 || direction.y != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void OnAttack(InputAction.CallbackContext callback)
    {
        
        Debug.Log(callback.phase);
        if(callback.phase == InputActionPhase.Started)
        {
            animator.SetTrigger("isAttacking");
        }

        
    }


}
