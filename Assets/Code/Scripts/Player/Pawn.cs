using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pawn : MonoBehaviour, IPossesable
{
    protected PlayerController controller;

    private void OnEnable()
    {
        SetUpPlayerInput();
    }

    public void Posses(PlayerController controllerWhoPosses)
    {
        controller = controllerWhoPosses;
        SetUpPlayerInput();
    }

    public virtual void SetUpPlayerInput()
    {
        //Set up inputs in player character level
        if (controller == null || controller.PlayerInputsComponent == null) { return; }

        //controller.PlayerInputsComponent.Player.Move;
    }

    public virtual void ReleasePlayerCharacterInput()
    {
        //Unbind/Release inputs
    }

    public void UnPosses()
    {
        controller = null;
        ReleasePlayerCharacterInput();
    }

    private void OnDisable()
    {
        ReleasePlayerCharacterInput();
    }
}
