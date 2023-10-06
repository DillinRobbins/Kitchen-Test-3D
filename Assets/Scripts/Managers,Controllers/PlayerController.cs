using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 9f;
    private IInteractable activeInteractable;

    private Dictionary<KeyCode, ICommand> moveKeyBindings = new();
    private Dictionary<KeyCode, ICommand> pressKeyBindings = new();

    KeyCode down = KeyCode.S;
    KeyCode up = KeyCode.W;
    KeyCode left = KeyCode.A;
    KeyCode right = KeyCode.D;
    KeyCode interact = KeyCode.Space;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Map input keys to commands.
        moveKeyBindings[up] = new CommandMoveUp(rb, moveSpeed);
        moveKeyBindings[down] = new CommandMoveDown(rb, moveSpeed);
        moveKeyBindings[left] = new CommandMoveLeft(rb, moveSpeed);
        moveKeyBindings[right] = new CommandMoveRight(rb, moveSpeed);
        pressKeyBindings[interact] = new CommandInteract(this, GetComponent<PlayerInventory>());
    }

    private void Update()
    {
        NoKeyPressedResetVelocity();
        GetKeysExecuteCommands();
        ClampDiagonalVelocity();
    }

    private void GetKeysExecuteCommands()
    {
        foreach (var kvp in moveKeyBindings)
        {
            if (Input.GetKey(kvp.Key))
            {
                kvp.Value.Execute();
            }
        }

        foreach(var kvp in pressKeyBindings)
        {
            if(Input.GetKeyDown(kvp.Key))
            {
                kvp.Value.Execute();
            }
        }
    }

    private void NoKeyPressedResetVelocity()
    {
        if (!Input.GetKey(left) && !Input.GetKey(right))
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);

        if (!Input.GetKey(down) && !Input.GetKey(up))
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
    }

    private void ClampDiagonalVelocity()
    {
        Vector3 currentVelocity = rb.velocity;

        // Calculate the current speed (magnitude of the velocity).
        float currentSpeed = currentVelocity.magnitude;

        // Check if the current speed exceeds the maximum speed.
        if (currentSpeed > moveSpeed)
        {
            // Clamp the velocity to the maximum speed while preserving the direction.
            rb.velocity = currentVelocity.normalized * moveSpeed;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if(activeInteractable == null)
            activeInteractable = collision.GetComponent<IInteractable>();
    }

    private void OnTriggerExit(Collider collision)
    {
        activeInteractable = null;
    }

    public IInteractable GetActiveInteractable()
    {
        return activeInteractable;
    }
}
