using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    //Player Components
    private CharacterController cc;
    private Animator animator;

    //Player Attributes 
    private Vector3 movement;
    [SerializeField] private float speed;

    public void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        cc.Move(movement * speed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 direction = value.ReadValue<Vector2>();
        movement = new Vector3(direction.x, 0, direction.y);
        cc.Move(movement * speed * Time.deltaTime);
        
        if(direction.x != 0 || direction.y != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void OnAttack()
    {

    }
}
