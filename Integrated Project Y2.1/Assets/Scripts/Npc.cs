/*
Author: Yeong Yu Seong
Date Created: 28 July 2025
Description: Controls the NPC behavior and state transitions
*/
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Npc : MonoBehaviour
{
    /// <summary>
    /// Flag to indicate if the NPC has reached a shopping point
    /// </summary>
    public bool reachedShopPoint = false;
    /// <summary>
    /// Array of locations for the NPC to walk to
    /// </summary>
    public Transform[] locations;
    /// <summary>
    /// Index of the current location in the NPC's path
    /// </summary>
    private int currentLocationIndex = 0;
    /// <summary>
    /// Current location the NPC is walking to
    /// </summary>
    [SerializeField]
    Transform currentLocation;
    /// <summary>
    /// NavMeshAgent component for the NPC to navigate the environment
    /// </summary>
    NavMeshAgent myAgent;
    /// <summary>
    /// Stores the current state of the NPC
    /// Possible states: "Walking", "Idle", "Stolen"
    /// </summary>
    public string currentState;
    /// <summary>
    /// Flag to indicate if the item has been stolen
    /// </summary>
    public bool stolen = false;
    /// <summary>
    /// Score value to be added when the player interacts with this NPC
    /// </summary>
    public int scoreValue = 1;
    /// <summary>
    /// GameManager instance to manage game state and interactions
    /// </summary>
    GameManager gameManager;
    /// <summary>
    /// Sound for when NPC exits the shop when stolen
    /// </summary>
    [SerializeField]
    AudioClip exitSound;
    /// <summary>
    /// Random speed for the NPC's movement
    /// </summary>
    float randomSpeed;
    /// <summary>
    /// Random chance set for the NPC to steal an item
    /// </summary>
    float randomChance;
    /// <summary>
    /// Stores the speed of the NPC for later use
    /// </summary>
    float speed;
    /// <summary>
    /// Audio clip to play when the NPC steals an item
    /// </summary>
    [SerializeField]
    AudioClip stealSound;
    /// <summary>
    /// Reference to the ThiefAlert script for handling stealing behavior
    /// </summary>
    ThiefAlert thiefAlert;
    /// <summary>
    /// List of toys that the NPC can steal in area 1
    /// </summary>
    [SerializeField]
    List<GameObject> toysToStealArea1;

    /// <summary>
    /// List of toys that the NPC can steal in area 2
    /// </summary>
    [SerializeField]
    List<GameObject> toysToStealArea2;

    /// <summary>
    /// List of toys that the NPC can steal in area 3
    /// </summary>
    [SerializeField]
    List<GameObject> toysToStealArea3;

    /// <summary>
    /// List of toys that the NPC can steal in area 4
    /// </summary>
    [SerializeField]
    List<GameObject> toysToStealArea4;
    int currentArea = 0;

    void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component attached to this NPC
        currentLocationIndex = 0; // Initialize the current location index
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager in the scene
        randomChance = Random.Range(0.05f, 0.1f); // Set the random chance for the NPC to steal an item
        randomSpeed = Random.Range(2f, 3f); // Set a random speed for the NPC's movement
        myAgent.speed = randomSpeed; // Set the NPC's speed to a random value
        speed = myAgent.speed; // Store the NPC's speed for later use
    }

    void Start()
    {
        StartCoroutine(SwitchState("Idle")); // Start the NPC in the Idle state
        thiefAlert = GetComponent<ThiefAlert>(); // Get the ThiefAlert component attached to this NPC
        // Initialize the toys to steal based on the area
        toysToStealArea1 = gameManager.toysArea1;
        toysToStealArea2 = gameManager.toysArea2;
        toysToStealArea3 = gameManager.toysArea3;
        toysToStealArea4 = gameManager.toysArea4;
    }
    public void UpdateToysList()
    {
        // Update the toys list in the GameManager
        gameManager.toysArea1 = toysToStealArea1;
        gameManager.toysArea2 = toysToStealArea2;
        gameManager.toysArea3 = toysToStealArea3;
        gameManager.toysArea4 = toysToStealArea4;
    }

    /// <summary>
    /// This method switches the NPC's state to a new state.
    /// It stops the current state coroutine if it exists and starts the new state coroutine.
    /// </summary>
    /// <param name="newState"></param>
    /// <returns></returns>
    IEnumerator SwitchState(string newState)
    {
        // Check if the new state is the same as the current state
        if (currentState == newState)
        {
            yield break; // Exit if the state is already the same
        }
        StopCoroutine(currentState); // Stop the current state coroutine if it exists

        currentState = newState; // Update the current state

        StartCoroutine(currentState); // Start the new state coroutine
    }

    /// <summary>
    /// This coroutine handles the walking behavior of the NPC.
    /// It sets the destination to the current location and waits until the NPC reaches it.
    /// </summary>
    /// <returns></returns>
    IEnumerator Walking()
    {
        reachedShopPoint = false; // Reset the flag at the start of walking
        currentLocation = locations[currentLocationIndex]; // Set the initial walking location
        myAgent.SetDestination(currentLocation.position); // Set the agent's destination to the current walking point
        while (currentState == "Walking")
        {
            // Check if the NPC has reached the destination
            if (reachedShopPoint)
            {
                // Go to next location in the route
                if (currentLocationIndex == locations.Length - 1)
                {
                    gameManager.npcInGame--; // Decrement the NPC count in the game
                    if (stolen)
                    {
                        gameManager.thievesNotCaught++; // Increment the count of thieves not caught
                    }
                    Destroy(gameObject); // Destroy NPC if reached the last location
                }
                else
                {
                    currentLocationIndex += 1; // Set the next location index
                    if (currentLocationIndex == (locations.Length - 2) && stolen)
                    {
                        currentLocationIndex += 1; // Skip the next location if the item is stolen
                    }
                }
                // Check if the current location is a shopable place or a neutral point
                if (!currentLocation.CompareTag("Neutral"))
                {
                    currentArea += 1;
                    StartCoroutine(SwitchState("Stolen")); // Switch to stolen state if reached a shopable place
                }
                else
                {
                    currentArea += 1;
                    StartCoroutine(SwitchState("Idle")); // Switch to idle state if reached a neutral point
                }
                yield break; // Exit the Walking coroutine
            }
            yield return null; // Wait for the next frame
        }
    }

    /// <summary>
    /// This coroutine handles the idle behavior of the NPC.
    /// </summary>
    /// <returns></returns>
    IEnumerator Idle()
    {
        float idleTime = 0f; // Timer for idle state
        while (currentState == "Idle")
        {
            // Perform idle behavior here
            idleTime += Time.deltaTime; // Increment idle time
            if (idleTime >= 5f) // If idle for 5 seconds, switch to walking
            {
                StartCoroutine(SwitchState("Walking")); // Switch to walking state
                yield break; // Exit the idle coroutine
            }
            yield return null; // Wait for the next frame
        }
    }

    /// <summary>
    /// This coroutine handles the stolen behavior of the NPC.
    /// It waits for a random chance to determine if the item is stolen.
    /// </summary>
    /// <returns></returns>
    IEnumerator Stolen()
    {
        // Generate a random number to determine if the item is stolen
        float randomChanceAtShelf = Random.Range(0f, 1f);
        if (randomChanceAtShelf < randomChance) // If the random chance is less than the defined chance
        {
            if (speed <= randomSpeed)
            {
                myAgent.speed *= 1.5f; // Set the NPC's speed to a higher value if the item is stolen
                speed = myAgent.speed; // Update the stored speed value
            }

            thiefAlert.StartStealing(); // Start the stealing animation
            // Select a toy based on the current area
            List<GameObject> toysToSteal = null;
            switch (currentArea)
            {
                case 1:
                    toysToSteal = toysToStealArea1;
                    break;
                case 2:
                    toysToSteal = toysToStealArea2;
                    break;
                case 3:
                    toysToSteal = toysToStealArea3;
                    break;
                case 4:
                    toysToSteal = toysToStealArea4;
                    break;
            }
            int randomIndex = Random.Range(0, toysToSteal.Count); // Select a random toy from the list
            GameObject selectedToy = toysToSteal[randomIndex]; // Select a random toy from the list
            selectedToy.SetActive(false); // Deactivate the selected toy
            toysToSteal.Remove(selectedToy); // Remove the stolen toy from the list
            UpdateToysList(); // Update the toys list in the GameManager
            stolen = true; // Set the stolen flag to true
            AudioSource.PlayClipAtPoint(stealSound, transform.position); // Play the steal sound effect
            int randomIdleTime = Random.Range(10, 15); // Random idle time between 10 to 15 seconds
            yield return new WaitForSeconds(randomIdleTime); // Wait for random idle time before switching state
            StartCoroutine(SwitchState("Walking")); // Switch back to walking state
        }
        else
        {
            StartCoroutine(SwitchState("Idle")); // Switch to idle state if item is not stolen
        }
    }

    /// <summary>
    /// This method is called when the NPC collides with another object.
    /// It checks if the npc reached the location and sets the flag accordingly.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Location") || other.gameObject.CompareTag("Neutral")) // Check if the collided object is a location
        {
            if (other.transform == currentLocation) // Check if the NPC reached the current location
            {
                reachedShopPoint = true; // Set the flag to true when reaching a location
            }

        }
        else if (other.gameObject.CompareTag("Exit")) // Check if the collided object is an exit point
        {
            if (stolen) // Check if the NPC has stolen an item
            {
                thiefAlert.LeaveStore(); // Call the exit method in ThiefAlert to handle exiting the store
                gameManager.redDustEffect.Play(); // Play the red dust effect
                if (gameManager.currentScore > 0)
                {
                    gameManager.UpdateScore(-scoreValue); // Update the score when exiting with a stolen item
                }
                AudioSource.PlayClipAtPoint(exitSound, transform.position); // Play the exit sound if stolen
            }
        }
    }
    /// <summary>
    /// This method is called when the NPC exits a trigger collider.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        // Reset the reachedShopPoint flag when exiting a location point
        if (other.gameObject.CompareTag("Location") || other.gameObject.CompareTag("Neutral"))
        {
            reachedShopPoint = false; // Reset the flag when exiting the trigger
        }
    }
}