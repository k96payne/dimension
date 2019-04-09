using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRock : MonoBehaviour
{
    float translationTime;
    float speed;

    Rigidbody movingPlatformRigidBody;
    Rigidbody playerRigidBody;
    int movementDirection = 0;

    void Start()
    {
        translationTime = Random.Range(1.0f, 5.0f);
        speed = Random.Range(0.2f, 0.7f);
        movementDirection = Random.Range(0.0f, 2.0f) >= 1 ? 0 : 1;

        movingPlatformRigidBody = GetComponent<Rigidbody>();
        playerRigidBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        AdjustConstraints();
        InvokeRepeating("ChangeVelocity", 1f, translationTime);

    }

    void AdjustConstraints()
    {
            movingPlatformRigidBody.constraints = RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | 
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    void ChangeVelocity()
    {
        movingPlatformRigidBody.velocity = GetMovement();
    }

    private Vector3 GetMovement()
    {
        return movementDirection++ % 2 == 0 ? new Vector3(0, speed, 0) :
            new Vector3(0, -speed, 0);
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, new Vector3(0, 1, 0), 0.5f))
        {
            playerRigidBody.velocity = new Vector3(0f, playerRigidBody.velocity.y, 0f);
            playerRigidBody.velocity += movingPlatformRigidBody.velocity;
        }
    }

}