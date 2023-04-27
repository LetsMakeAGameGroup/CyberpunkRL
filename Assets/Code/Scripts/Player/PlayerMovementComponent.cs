using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovementComponent : MonoBehaviour
{
    [Header("Movement Settings")]

    public float maxWalkSpeed = 6;
    public float jumpForce = 10.0f;
    public float gravityForce = 10;
    public float maxGravityVelocity = 20;

    public bool canJump = true;
    public int maxJumpCount = 2;
    public int jumpCount;
    public bool canMoveInAir = true;
    public float airMovementMultiplier = 1.0f;

    bool _IsJumping;
    public bool isJumping { get { return _IsJumping; } set { _IsJumping = value; } }


    RaycastHit _LastGroundHit;
    public RaycastHit lastGroundHit { get { return _LastGroundHit; } private set { } }

    public Vector3 velocity;
    Vector3 _RecivedInputs;

    private void Awake()
    {

    }

    void Update()
    {
        Vector3 movementDelta = ConsumeInput();
        SetVelocity(new Vector3(movementDelta.x * maxWalkSpeed, velocity.y, movementDelta.z * maxWalkSpeed));

        //velocity.y = Mathf.Clamp(velocity.y, -maxGravityVelocity, velocity.y);
        //
        //if (!_IsJumping)
        //{
        //    ApplySlopeSliding();
        //}

        Move(velocity);
    }

    public bool WantsToMove()
    {
        if (ConsumeInput() != Vector3.zero)
        {
            return true;
        }

        return false;
    }

    public Vector3 ConsumeInput()
    {
        Vector3 returnVal = _RecivedInputs;
        _RecivedInputs = Vector3.zero;
        return returnVal;
    }

    public void ApplyGravity()
    {
        velocity.y -= gravityForce * Time.deltaTime;
    }

    public bool IsGrounded()
    {
        //Debug.DrawRay(transform.position, transform.up * 0.2f, Color.red);
        bool grounded = GroundRaycast(0, 0, 0) || GroundRaycast(0, 0, .5f) || GroundRaycast(0, 0, -.5f) || GroundRaycast(.5f, 0, 0) || GroundRaycast(-.5f, 0, 0);
        return grounded;
    }

    public bool GroundRaycast(float xOffset, float yOffset, float zOffset)
    {
        Vector3 offset = new Vector3(xOffset, yOffset, zOffset);

        Debug.DrawRay((transform.position + offset) + (transform.up * 0.1f), -transform.up * 0.2f, Color.red);

        return Physics.Raycast((transform.position + offset) + (transform.up * 0.1f), -transform.up, out _LastGroundHit, 0.2f);
    }

    public bool TouchedCeiling()
    {
        return false;
        //return _CharacterController.collisionFlags == CollisionFlags.Above;
    }

    public void Move(Vector3 direction)
    {
        //_CharacterController.Move(direction);

        transform.position += direction * Time.deltaTime;
    }

    public void ReceiveInput(Vector3 NewInput)
    {
        _RecivedInputs = NewInput;
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
    }
}
