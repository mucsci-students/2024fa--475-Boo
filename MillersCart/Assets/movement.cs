using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public float dynamicFrictionCoefficient = .05f;
    private Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        AdjustForFriction();
        MovePlayer();
    }

    void AdjustForFriction()
    {
        playerRigidbody.velocity -= playerRigidbody.velocity * 0.05f;
    }

    void MovePlayer()
    {
        // Check for input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // increase the players velocity in the direction they are facing
            playerRigidbody.velocity += player.transform.forward * 2.0f;
            
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            // check to see if the player is moving forward
            if (playerRigidbody.velocity.normalized == player.transform.forward)
            {
                playerRigidbody.velocity -= player.transform.forward * 2.25f;
            }
            else 
            {
                playerRigidbody.velocity -= player.transform.forward * 2.0f;
            }
            
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // rotate the player 15 degrees to the left
            player.transform.Rotate(0, -4, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.Rotate(0, 4, 0);
        }
    }
}