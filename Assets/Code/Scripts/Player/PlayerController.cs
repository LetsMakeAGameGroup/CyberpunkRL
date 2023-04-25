using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected PlayerInputs playerInputs;

    public Pawn pawnToPosses;

    public PlayerInputs PlayerInputsComponent { get { return playerInputs; } }

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        pawnToPosses.Posses(this);
    }

    private void OnEnable()
    {
        playerInputs.Enable();
        SetUpPlayerControllerInputs();
    }

    private void SetUpPlayerControllerInputs()
    {
        //Set up inputs in player controller level
    }

    private void ReleasePlayerControllerInputs()
    {
        //Unbind/Release inputs
    }

    public void Possess(IPossesable possesable)
    {
        possesable?.Posses(this);
    }

    public void UnPossess(IPossesable possesable) 
    {
        possesable?.UnPosses();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
        ReleasePlayerControllerInputs();
    }
}
