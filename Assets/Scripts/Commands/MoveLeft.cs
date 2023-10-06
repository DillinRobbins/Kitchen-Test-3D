using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMoveLeft : ICommand
{
    private Rigidbody rb;
    private float moveSpeed;

    public CommandMoveLeft(Rigidbody rb, float moveSpeed)
    {
        this.rb = rb;
        this.moveSpeed = moveSpeed;
    }

    public void Execute()
    {
        rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, rb.velocity.z);
    }
}
