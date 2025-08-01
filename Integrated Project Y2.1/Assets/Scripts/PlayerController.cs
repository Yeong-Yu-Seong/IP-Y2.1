/*
Author: Yeong Yu Seong
Date Created: 28 July 2025
Description: Controls the player character's interactions and movements
*/
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Distance within which the player can interact with NPCs or objects
    /// </summary>
    float interactionDistance = 5f;
    /// <summary>
    /// Spawn point for NPCs
    /// </summary>
    [SerializeField]
    Transform spawnPoint;
    /// <summary>
    /// Flag to check if the player is interacting with an NPC
    /// </summary>
    bool isNpc = false;
    /// <summary>
    /// Current NPC being interacted with
    /// </summary>
    Npc currentNpc;
    /// <summary>
    /// GameManager instance to manage game state
    /// </summary>
    GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager in the scene
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player hits any NPC within interaction distance
        RaycastHit hitInfo;
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * interactionDistance, Color.red);
        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo, interactionDistance))
        {
            // Check if the hit object is an NPC
            if (hitInfo.collider.gameObject.CompareTag("Npc"))
            {
                isNpc = true; // Set the flag to true if an NPC is detected
                currentNpc = hitInfo.collider.gameObject.GetComponent<Npc>(); // Get the Npc component from the hit object
            }
            else
            {
                isNpc = false; // Reset the NPC interaction flag
                currentNpc = null; // Clear the current NPC reference
            }
        }
    }
    /// <summary>
    /// This method is called when the player interacts with an NPC or object.
    /// If the player is interacting with an NPC that had stolen, it updates the score based on the NPC
    /// </summary>
    void OnInteract()
    {
        if (isNpc)
        {
            // Interact with NPC that stolen an item
            if (currentNpc.stolen)
            {
                Debug.Log("Interacting with NPC who has stolen an item.");
                gameManager.UpdateScore(currentNpc.scoreValue); // Update score based on NPC's stolen item
                gameManager.npcInGame--; // Decrement the NPC count in the game
                Destroy(currentNpc.gameObject); // Remove NPC after interaction
            }
        }
        else
        {
            Debug.Log("Interacting with store.");
        }
    }
    
}
