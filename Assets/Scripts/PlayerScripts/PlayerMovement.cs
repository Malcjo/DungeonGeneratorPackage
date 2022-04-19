using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public Transform orientation;

    Vector2 input;
    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        myInput();
    }
    void myInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * input.y + orientation.right * input.x;

        if(rb.velocity.magnitude > 15)
        {
            rb.velocity = moveDirection * moveSpeed;
        }
        rb.velocity += moveDirection;
        if(input.x == 0.0f && input.y == 0.0f)
        {
            if (rb.velocity.magnitude > 5)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }
    }
}
