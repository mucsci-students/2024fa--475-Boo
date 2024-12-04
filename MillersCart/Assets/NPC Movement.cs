using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public GameObject ball;             // Reference to the ball
    public float moveSpeed = 10f;       // Speed at which the NPC moves.
    public float rotationSpeed = 5f;    // Speed for rotating.
    public float stopAccelerationDistance = 10f; // Distance at which the NPC will stop accelerating before a turn.

    private Rigidbody npcRigidbody;

    void Start()
    {
        npcRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveToNextCheckpoint();
    }

    void MoveToNextCheckpoint()
    {
        // Calculate direction to the ball
        Vector3 directionToCheckpoint = (checkpoint.transform.position - transform.position).normalized;
        directionToCheckpoint.y = 0; // Prevents the NPC from moving up or down

        // Rotate NPC towards the ball
        Quaternion lookRotation = Quaternion.LookRotation(directionToBall);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Move forward
        npcRigidbody.AddForce(transform.forward * moveSpeed);

        // Check distance to the next checkpoint.
        float distanceToCheckpoint = Vector3.Distance(transform.position, checkpoint.transform.position);

        
        // Check to see if the npc is too far the checkpoint.
        if (distanceToCheckpoint >= stopAccelerationDistance && Vector3.Angle(transform.forward, directionToBall) > 30f)
        {
            // Slow down if too far from the ball
            if (npcRigidbody.velocity.normalized == transform.forward) 
            {
                npcRigidbody.velocity -= transform.forward * moveSpeed;
            }
            else
            {
                npcRigidbody.velocity = Vector3.zero;
            }
        }
    }
}