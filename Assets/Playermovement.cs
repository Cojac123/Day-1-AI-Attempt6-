using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Playermovement : MonoBehaviour
{


    // movement values 
    [Header("Movement Values")]
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float jumpSpeed = 10.0f;

    // references
    Rigidbody rb;
    [SerializeField] Animator animator;

    // variables
    Vector3 movementVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("walkSpeed", movementVector.magnitude);
    }


    /// <summary>
    /// This jump function multiples a force by some jumpspeed
    /// </summary>
    /// <param name="ctx"></param>
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)    
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }

    public void OnMovement(InputValue v) // Send messages
    {
        Vector2 inputVector = v.Get<Vector2>();
        movementVector = new Vector3(inputVector.x, 0, inputVector.y);
    }

    public void OnMovement(InputAction.CallbackContext ctx) // invoke unity events
    {
        Vector2 inputVector = ctx.ReadValue<Vector2>();
        movementVector = new Vector3(inputVector.x, 0, inputVector.y);

        animator.transform.forward = movementVector.normalized;
    }

    private void FixedUpdate()
    {
        rb.AddForce(movementVector * moveSpeed, ForceMode.Acceleration);
    }


}
