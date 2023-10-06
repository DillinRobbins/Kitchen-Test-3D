using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommandInteract : ICommand
{
    PlayerController playerController;
    PlayerInventory playerInventory;

    public CommandInteract(PlayerController playerController, PlayerInventory playerInventory)
    {
        this.playerController = playerController;
        this.playerInventory = playerInventory;
    }

    public void Execute()
    {
        if (playerController.GetActiveInteractable() != null)
            playerController.GetActiveInteractable().Interact(playerInventory.CheckIngredient());
        else Debug.Log("No Interactable detected");
    }
}
