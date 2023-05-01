using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected PlayerInputs playerInputs;

    public Pawn pawnToPosses;

    private Ray GroundRay;
    Vector3 cursorPos;
    public GameObject DebugCursor;

    public Vector3 CursorPos { get { return cursorPos; } }


    public PlayerInputs PlayerInputsComponent { get { return playerInputs; } }

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Enable();
        SetUpPlayerControllerInputs();

        pawnToPosses?.Posses(this);
    }

    private void OnEnable()
    {
        playerInputs.Enable();
        SetUpPlayerControllerInputs();
    }

    private void Update()
    {
        HandleCursorPosition();
    }

    public void HandleCursorPosition() 
    {
        //Create a ray from the Mouse click position
        GroundRay = Camera.main.ScreenPointToRay(playerInputs.Player.MousePosition.ReadValue<Vector2>());

        RaycastHit hit;

        if (Physics.Raycast(GroundRay, out hit, 100f))
        {
            cursorPos = hit.point;

            if (DebugCursor != null)
            {
                DebugCursor.transform.SetPositionAndRotation(hit.point, Quaternion.identity);
            }
        }
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
