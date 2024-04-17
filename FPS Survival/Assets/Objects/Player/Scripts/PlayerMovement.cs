using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float speed = 10f;
    public float runSpeed = 15f;

    public float jumpForce = 200;

    public bool _isGrounded;

    private Animator animator;

    private void Start()
    {
        InputManager.Instance.GetInputMaster().Movement.Jump.started += _ => Jump();
        animator = GetComponent<Animator>();
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

        float forward = InputManager.Instance.GetInputMaster().Movement.Forward.ReadValue<float>();
        float right = InputManager.Instance.GetInputMaster().Movement.Right.ReadValue<float>();

        Vector3 move = transform.forward * forward + transform.right * right;

        move.Normalize();

        move *= InputManager.Instance.GetInputMaster().Movement.Run.ReadValue<float>() == 0 ? speed : runSpeed;

        float velocityX = Vector3.Dot(move.normalized, transform.right);
        float velocityZ = Vector3.Dot(move.normalized, transform.forward);

        animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);

        rb.velocity = new(move.x, rb.velocity.y, move.z);
    }
}
