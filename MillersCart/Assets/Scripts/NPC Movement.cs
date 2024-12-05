using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public string checkpointPrefix = "Checkpoint"; // Prefix for checkpoint names.
    public float moveSpeed = 10f; // Speed of the NPC.
    public float rotationSpeed = 5f; // Rotation speed for smooth turns.
    public float stopAccelerationDistance = 5f; // Distance to slow down near checkpoints.
    public float checkpointReachThreshold = 2f; // Distance threshold to consider a checkpoint reached.

    private Rigidbody npcRigidbody;
    private List<GameObject> checkpoints = new List<GameObject>();
    private int currentCheckpointIndex = 0;

    void Start()
    {
        npcRigidbody = GetComponent<Rigidbody>();
        LoadCheckpoints();
    }

    void FixedUpdate()
    {
        if (checkpoints.Count > 0)
        {
            MoveToNextCheckpoint();
        }
    }

    void LoadCheckpoints()
    {
        // Find all GameObjects with the checkpoint prefix in the scene.
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith(checkpointPrefix))
            {
                checkpoints.Add(obj);
            }
        }

        // Sort checkpoints based on their names to ensure proper order.
        checkpoints.Sort((a, b) => string.Compare(a.name, b.name));
        
        if (checkpoints.Count == 0)
        {
            Debug.LogError("No checkpoints found in the scene with the prefix " + checkpointPrefix);
        }
    }

    void MoveToNextCheckpoint()
    {
        if (currentCheckpointIndex >= checkpoints.Count)
        {
            Debug.Log("Race finished!"); 
            return;
        }

        // Get the current checkpoint.
        GameObject currentCheckpoint = checkpoints[currentCheckpointIndex];

        // Calculate direction to the current checkpoint.
        Vector3 directionToCheckpoint = (currentCheckpoint.transform.position - transform.position).normalized;
        directionToCheckpoint.y = 0; // Prevent vertical movement.

        // Rotate towards the checkpoint.
        Quaternion lookRotation = Quaternion.LookRotation(directionToCheckpoint);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Check distance to the current checkpoint.
        float distanceToCheckpoint = Vector3.Distance(transform.position, currentCheckpoint.transform.position);

        // Adjust speed if near the checkpoint or on sharp turns.
        if (distanceToCheckpoint <= stopAccelerationDistance)
        {
            float speedFactor = Mathf.Clamp01(distanceToCheckpoint / stopAccelerationDistance);
            npcRigidbody.velocity = transform.forward * moveSpeed * speedFactor;
        }
        else
        {
            // Move forward at full speed.
            npcRigidbody.velocity = transform.forward * moveSpeed;
        }

        // Check if the checkpoint has been reached.
        if (distanceToCheckpoint <= checkpointReachThreshold)
        {
            currentCheckpointIndex++; 
        }
    }
}