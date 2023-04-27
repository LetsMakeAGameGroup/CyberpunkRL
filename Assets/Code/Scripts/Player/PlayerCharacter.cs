using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPossesable 
{
    void Posses(PlayerController controllerWhoPosses);

    void UnPosses();
}

[RequireComponent(typeof(PlayerMovementComponent))]
public class PlayerCharacter : Pawn
{
    public Transform playerMesh;
    public Transform cameraT;
    Animator animator;
    PlayerMovementComponent movementComponent;

    public void Awake()
    {
        movementComponent = GetComponent<PlayerMovementComponent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        AddMovementInput(controller.PlayerInputsComponent.Player.Move.ReadValue<Vector2>());

        //playerMesh.rotation = Quaternion.LookRotation(cameraT.up, cameraT.forward);
        playerMesh.rotation = Quaternion.LookRotation(cameraT.forward, cameraT.up);


        //For now, just to preview movement
        Vector3 inputOnFrame = movementComponent.velocity;

        if (inputOnFrame != Vector3.zero)
        {
            animator.SetFloat("Horizontal", inputOnFrame.x);
            animator.SetFloat("Vertical", inputOnFrame.z);
        }
    }

    public override void SetUpPlayerInput()
    {
        base.SetUpPlayerInput();

        if (controller != null && controller.PlayerInputsComponent != null)
        {

        }
    }

    public override void ReleasePlayerCharacterInput()
    {
        base.ReleasePlayerCharacterInput();

        if (controller != null && controller.PlayerInputsComponent != null)
        {

        }
    }

    void AddMovementInput(Vector2 MoveInput) 
    {
        Debug.Log("Moving!");

        if(movementComponent == null) { return; }

        Vector3 DesireDirection = new Vector3(MoveInput.x, 0, MoveInput.y);
        DesireDirection = transform.TransformDirection(DesireDirection);

        movementComponent.ReceiveInput(DesireDirection);
    }
}
