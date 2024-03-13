using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public InputManager inputManager;
    public Rigidbody rb;

    public float speed = 10f;
    public float runSpeed = 15f;

    public float jumpForce = 200;

    public bool _isGrounded;

    private void Start()
    {
        inputManager.inputMaster.Movement.Jump.started += _ => Jump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    public void Move()
    {

        float forward = inputManager.inputMaster.Movement.Forward.ReadValue<float>();
        float right = inputManager.inputMaster.Movement.Right.ReadValue<float>();

        Vector3 move = transform.forward * forward + transform.right * right;

        move.Normalize();

        move *= inputManager.inputMaster.Movement.Run.ReadValue<float>() == 0 ? speed : runSpeed;

        rb.velocity = new(move.x, rb.velocity.y, move.z);
    }
}
