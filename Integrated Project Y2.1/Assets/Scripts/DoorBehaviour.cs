/*
Author: Yeong Yu Seong
Date Created: 13 August 2025
Description: Controls the door's behavior
*/
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Called when an NPC or Player enters the trigger
    /// Opens the door/ keeps it open
    /// </summary>
    /// <param name="collider"></param>
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Npc") || collider.CompareTag("Player"))
        {
            Vector3 doorRotation = transform.eulerAngles;
            if (doorRotation.y == 0)
            {
                doorRotation.y += 90f; // Rotate the door by 90 degrees
            }
            transform.eulerAngles = doorRotation; // Apply the rotation
        }
    }
    /// <summary>
    /// Called when an NPC or Player exits the trigger
    /// Closes the door/ keeps it closed
    /// </summary>
    /// <param name="collider"></param>
    public void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Npc") || collider.CompareTag("Player"))
        {
            Vector3 doorRotation = transform.eulerAngles;
            if (doorRotation.y == 90f)
            {
                doorRotation.y -= 90f; // Rotate the door back to 0 degrees
            }
            transform.eulerAngles = doorRotation; // Apply the rotation
        }
    }
    /// <summary>
    /// Called when an NPC or Player is within the trigger
    /// Keeps the door open
    /// </summary>
    /// <param name="collider"></param>
    public void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Npc") || collider.CompareTag("Player"))
        {
            // Keep the door open
            Vector3 doorRotation = transform.eulerAngles;
            if (doorRotation.y != 90f)
            {
                doorRotation.y = 90f; // Ensure the door remains open
            }
            transform.eulerAngles = doorRotation; // Apply the rotation
        }
    }
}