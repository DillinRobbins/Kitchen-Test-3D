using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMoveUp : ICommand
{
    private Rigidbody rb;
    private float moveSpeed;

    public CommandMoveUp(Rigidbody rb, float moveSpeed)
    {
        this.rb = rb;
        this.moveSpeed = moveSpeed;
    }

    public void Execute()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
    }
}
