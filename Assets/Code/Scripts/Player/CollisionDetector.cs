using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FCollisionInfo
{
    public GameObject LastOverlapped;
    public Vector3 otherPosition;

    public Vector3 direction;
    public float distance;
}

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] int MaxCollisionNum = 16; // maximum amount of collisions to detect
    [SerializeField] bool CanDetectCollision = true;

    public bool CollidingTrigger;

    public float radius = 0.3f;

    private Collider[] collidersAround;

    private CapsuleCollider ColliderComponent;
    [SerializeField] public FCollisionInfo LastCollisionInfo;

    private void Awake()
    {
        ColliderComponent = GetComponent<CapsuleCollider>();
    }

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        //Initialize
        collidersAround = new Collider[MaxCollisionNum];
    }

    void Update()
    {
        if (CanDetectCollision)
        {
            DetectCollision();
        }
    }

    void DetectCollision()
    {
        int count = Physics.OverlapCapsuleNonAlloc(ColliderComponent.bounds.min, ColliderComponent.bounds.max, radius, collidersAround);

        for (int i = 0; i < count; ++i)
        {
            //if its a trigger collider, ignore but cache the object
            if (collidersAround[i].isTrigger)
            {
                continue;
            }

            //Cache
            Collider collider = collidersAround[i];

            //Ignore self
            if (collider == ColliderComponent)
            {
                continue;
            }

            Vector3 otherPosition = collider.gameObject.transform.position;
            Quaternion otherRotation = collider.gameObject.transform.rotation;

            bool overlapped = Physics.ComputePenetration(ColliderComponent, transform.position, transform.rotation, collider, otherPosition, otherRotation, out LastCollisionInfo.direction, out LastCollisionInfo.distance);

            if (overlapped)
            {
                Debug.DrawRay(transform.position, LastCollisionInfo.direction * LastCollisionInfo.distance, Color.green);
                //Debug.Log(LastCollisionInfo.direction);
                PerformCollisionMove();
            }
            
        }
    }

    public T GetComponentInCollider<T>()
    {
        for (int i = 0; i < collidersAround.Length; i++)
        {
            if (collidersAround[i] != null)
            {
                T comp = collidersAround[i].GetComponent<T>();

                if (comp != null)
                {
                    return comp;
                }

            }
        }

        return default(T);
    }

    private void PerformCollisionMove()
    {
        Debug.Log("FixingCollision");
        transform.Translate(transform.InverseTransformDirection(LastCollisionInfo.direction * LastCollisionInfo.distance));
    }

    public CapsuleCollider GetCapsuleCollider()
    {
        return ColliderComponent;
    }
}
