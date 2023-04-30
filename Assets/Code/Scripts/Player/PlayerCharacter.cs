using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPossesable 
{
    void Posses(PlayerController controllerWhoPosses);

    void UnPosses();
}

[System.Serializable]
public class PlayerCharacterAbilities 
{
    public Ability primarlyAbility;
    public Ability secondaryAbility;
    public Ability firstAbility;
    public Ability secondAbility;
    public Ability thirdAbility;
    public Ability ultimateAbility;
}

public interface IPlayerCharacterAbilities 
{
    public Ability PrimaryAbility { get; }
    public Ability SecondaryAbility { get; }
    public Ability FirstAbility { get; }
    public Ability SecondAbility { get; }
    public Ability ThirdAbility { get; }
    public Ability UltimateAbility { get; }
}

[RequireComponent(typeof(PlayerMovementComponent))]
public class PlayerCharacter : Pawn, IPlayerCharacterAbilities, IAbilityUser
{
    public Transform playerMesh;
    public Transform cameraT;
    Animator animator;
    PlayerMovementComponent movementComponent;

    [SerializeField] PlayerCharacterAbilities characterAbilities;

    public Ability PrimaryAbility { get { return characterAbilities.primarlyAbility; } }
    public Ability SecondaryAbility { get { return characterAbilities.secondaryAbility; } }
    public Ability FirstAbility { get { return characterAbilities.firstAbility; } }
    public Ability SecondAbility { get { return characterAbilities.secondAbility; } }
    public Ability ThirdAbility { get { return characterAbilities.thirdAbility; } }
    public Ability UltimateAbility { get { return characterAbilities.ultimateAbility; } }

    public Transform UserTransform { get { return transform; } }

    public void Awake()
    {
        movementComponent = GetComponent<PlayerMovementComponent>();
        animator = GetComponentInChildren<Animator>();

        if (cameraT == null) 
        {
            cameraT = Camera.main.transform;
        }

        //Initialize all Abilities
        PrimaryAbility.InitializeAbility(this);
        SecondaryAbility.InitializeAbility(this);
        FirstAbility.InitializeAbility(this);
        SecondAbility.InitializeAbility(this);
        ThirdAbility.InitializeAbility(this);
        UltimateAbility.InitializeAbility(this);
    }

    private void Update()
    {
        if (controller != null && controller.PlayerInputsComponent != null)
        {
            AddMovementInput(controller.PlayerInputsComponent.Player.Move.ReadValue<Vector2>());
        }

        //playerMesh.rotation = Quaternion.LookRotation(cameraT.up, cameraT.forward);
        playerMesh.rotation = Quaternion.LookRotation(cameraT.forward, cameraT.up);


        //For now, just to preview movement
        Vector3 inputOnFrame = movementComponent.velocity;

        if (inputOnFrame != Vector3.zero)
        {
            animator?.SetFloat("Horizontal", inputOnFrame.x);
            animator?.SetFloat("Vertical", inputOnFrame.z);
        }
    }

    public override void SetUpPlayerInput()
    {
        base.SetUpPlayerInput();

        if (controller != null && controller.PlayerInputsComponent != null)
        {
            //Abilities Binding
            controller.PlayerInputsComponent.CharacterAbilities.PrimarlyAbility.started += ctx => characterAbilities.primarlyAbility.TriggerAbility();
            controller.PlayerInputsComponent.CharacterAbilities.PrimarlyAbility.canceled += ctx => characterAbilities.primarlyAbility.EndAbility();

            controller.PlayerInputsComponent.CharacterAbilities.SecondaryAbility.started += ctx => characterAbilities.secondaryAbility.TriggerAbility();
            controller.PlayerInputsComponent.CharacterAbilities.SecondaryAbility.canceled += ctx => characterAbilities.secondaryAbility.EndAbility();

            controller.PlayerInputsComponent.CharacterAbilities.FirstAbility.started += ctx => characterAbilities.firstAbility.TriggerAbility();
            controller.PlayerInputsComponent.CharacterAbilities.FirstAbility.canceled += ctx => characterAbilities.firstAbility.EndAbility();

            controller.PlayerInputsComponent.CharacterAbilities.SecondAbility.started += ctx => characterAbilities.secondAbility.TriggerAbility();
            controller.PlayerInputsComponent.CharacterAbilities.SecondAbility.canceled += ctx => characterAbilities.secondAbility.EndAbility();

            controller.PlayerInputsComponent.CharacterAbilities.ThirdAbility.started += ctx => characterAbilities.thirdAbility.TriggerAbility();
            controller.PlayerInputsComponent.CharacterAbilities.ThirdAbility.canceled += ctx => characterAbilities.thirdAbility.EndAbility();

            controller.PlayerInputsComponent.CharacterAbilities.UltimateAbility.started += ctx => characterAbilities.ultimateAbility.TriggerAbility();
            controller.PlayerInputsComponent.CharacterAbilities.UltimateAbility.canceled += ctx => characterAbilities.ultimateAbility.EndAbility();
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
        if(movementComponent == null || MoveInput == Vector2.zero) { return; }

        Vector3 DesireDirection = new Vector3(MoveInput.x, 0, MoveInput.y);
        DesireDirection = transform.TransformDirection(DesireDirection);

        movementComponent.ReceiveInput(DesireDirection);
    }
}
