/*
Author: Yeong Yu Seong
Date Created: 28 July 2025
Description: Manages the game state, score, and NPC spawning
*/

using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// UIManager instance to manage UI elements
    /// </summary>
    UIManager uiManager;
    /// <summary>
    /// Player GameObject reference
    /// </summary>
    [SerializeField]
    GameObject player;
    /// <summary>
    /// TextMeshProUGUI component to display the score
    /// </summary>
    [SerializeField]
    TextMeshProUGUI scoreText;
    /// <summary>
    /// Prefab for the NPC to spawn
    /// </summary>
    [SerializeField]
    GameObject npcPrefab;
    /// <summary>
    /// Prefab for the child NPC that follows the main NPC
    /// </summary>
    [SerializeField]
    GameObject npcchildPrefab;
    /// <summary>
    /// Spawn point for NPCs
    /// </summary>
    [SerializeField]
    Transform spawnPoint;
    /// <summary>
    /// Current score of the player
    /// </summary>
    public int currentScore = 0;
    /// <summary>
    /// Flag to check if NPCs are currently spawning
    /// </summary>
    bool isSpawning = false;
    /// <summary>
    /// Number of NPCs currently in the game
    /// </summary>
    [SerializeField]
    public int npcInGame = 0;
    /// <summary>
    /// Array of locations for NPCs to walk to
    /// </summary>
    [SerializeField]
    Transform[] locations;
    /// <summary>
    /// Total number of NPCs to spawn in the game
    /// </summary>
    int totalNpcToSpawn = 5;
    /// <summary>
    /// Flag to check if the game is over
    /// </summary>
    bool isGameOver = false;
    /// <summary>
    /// Current number of NPCs spawned in the game
    /// </summary>
    public int currentNpcCount = 0;
    /// <summary>
    /// TextMeshProUGUI component to display the number of thieves caught
    /// </summary>
    [SerializeField]
    TextMeshProUGUI thievesCaughtText;
    /// <summary>
    /// TextMeshProUGUI component to display the number of thieves not caught
    /// </summary>
    [SerializeField]
    TextMeshProUGUI thievesNotCaughtText;
    /// <summary>
    /// TextMeshProUGUI component to display the final score when the game is over
    /// </summary>
    [SerializeField]
    TextMeshProUGUI scoreTextFinal;
    /// <summary>
    /// Number of thieves not caught in the game
    /// </summary>
    public int thievesNotCaught = 0;
    /// <summary>
    /// Count of thieves caught in the game
    /// </summary>
    public int thievesCaught = 0;
    /// <summary>
    /// Main camera in the scene
    /// </summary>
    [SerializeField]
    Camera mainCamera;
    /// <summary>
    /// Menu camera in the scene
    /// </summary>
    [SerializeField]
    Camera menuCamera;
    /// <summary>
    /// Prefab for the roaming NPC
    /// </summary>
    [SerializeField]
    GameObject roamingNPC;
    /// <summary>
    /// Transform for the roaming NPC spawn point
    /// </summary>
    [SerializeField]
    Transform roamingNPCSpawn;
    /// <summary>
    /// Particle system for the red dust effect when a thief escapes
    /// </summary>
    public ParticleSystem redDustEffect;
    /// <summary>
    /// Array of adult NPC prefabs
    /// </summary>
    [SerializeField]
    GameObject[] adultNPCs;
    /// <summary>
    /// Array of child NPC prefabs
    /// </summary>
    [SerializeField]
    GameObject[] childNPCs;
    /// <summary>
    /// List of toys that can be stolen in area 1
    /// </summary>
    public List<GameObject> toysArea1;
    /// <summary>
    /// List of toys that can be stolen in area 2
    /// </summary>
    public List<GameObject> toysArea2;
    /// <summary>
    /// List of toys that can be stolen in area 3
    /// </summary>
    public List<GameObject> toysArea3;
    /// <summary>
    /// List of toys that can be stolen in area 4
    /// </summary>
    public List<GameObject> toysArea4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>(); // Find the UIManager in the scene
        scoreText.text = "Score: " + currentScore.ToString(); // Initialize the score text
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is in the main menu
        if (uiManager.Menu.enabled)
        {
            menuCamera.enabled = true; // Enable the menu camera when in menu
            mainCamera.enabled = false; // Disable the main camera when in menu
            Cursor.visible = true; // Show the cursor when in menu
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor when in menu
            player.SetActive(false); // Disable the player GameObject when in menu
            isSpawning = false; // Reset the spawning flag when in menu
            CancelInvoke("SpawnNPC"); // Stop invoking the SpawnNPC method if in menu
        }
        else
        {
            menuCamera.enabled = false; // Disable the menu camera when not in menu
            mainCamera.enabled = true; // Enable the main camera when not in menu
            if (!isSpawning) // Check if NPCs are not currently spawning
            {
                isSpawning = true; // Set the flag to true to prevent multiple coroutines
                InvokeRepeating("SpawnNPC", 10f, 10f); // Start spawning NPCs at regular intervals
            }
            if (currentNpcCount >= totalNpcToSpawn) // Stop spawning if the total number of NPCs has been reached
            {
                isGameOver = true; // Set the game over flag to true
                CancelInvoke("SpawnNPC"); // Stop invoking the SpawnNPC method
                if (isGameOver && npcInGame == 0) // Check if the game is over and no NPCs are left
                {
                    uiManager.GameOverCanvas.enabled = true; // Show the game over canvas if no NPCs are in the game
                    thievesCaughtText.text = "Thieves Caught: " + thievesCaught.ToString(); // Update the caught thieves text
                    thievesNotCaughtText.text = "Thieves Not Caught: " + thievesNotCaught.ToString(); // Update the not caught thieves text
                    scoreTextFinal.text = "Final Score: " + currentScore.ToString(); // Update the final score text
                    Cursor.visible = true; // Show the cursor when game is over
                    Cursor.lockState = CursorLockMode.None; // Unlock the cursor when game is over
                }
            }
            else
            {
                Cursor.visible = true; // Show the cursor when not in menu
                Cursor.lockState = CursorLockMode.None; // Lock the cursor when not in menu
                player.SetActive(true); // Enable the player GameObject when not in menu
                mainCamera.enabled = true; // Enable the main camera when not in menu
            }
        }
        // Check if the player presses the Q key to go back to the main menu
        if (Input.GetKeyDown(KeyCode.Q))
        {
            uiManager.BackToMenu(); // Go back to the main menu when Q is pressed
        }
        // update all current npc's toys list
        foreach (Npc npc in FindObjectsOfType<Npc>())
        {
            npc.UpdateToysList();
        }
    }

    /// <summary>
    /// This method updates the player's score and updates the score text UI.
    /// It is called when the player interacts with an NPC or object that has a score value.
    /// </summary>
    /// <param name="score"></param>
    public void UpdateScore(int score)
    {
        currentScore += score; // Increment the current score by the score value
        scoreText.text = "Score: " + currentScore.ToString(); // Update the score text UI with the new score
    }

    /// <summary>
    /// This method spawns a new NPC at the specified spawn point.
    /// It assigns the locations for the NPC to navigate and increments the NPC count.
    /// It also spawns a child NPC that will follow the newly spawned NPC.
    /// Increment the npcInGame and currentNpcCount variables to keep track of the number of NPCs in the game.
    /// It is called repeatedly at a set interval to spawn multiple NPCs in the game.
    /// </summary>
    void SpawnNPC()
    {
        GameObject randomAdult = adultNPCs[Random.Range(0, adultNPCs.Length)]; // Get a random adult NPC prefab
        GameObject newNpc = Instantiate(randomAdult, spawnPoint.position, spawnPoint.rotation); // Instantiate a new NPC at the spawn point
        newNpc.GetComponent<Npc>().locations = locations; // Assign the locations to the NPC
        int randomChildCount = Random.Range(0, 2); // Randomly decide how many child NPCs to spawn (0 or 1)
        for (int i = 0; i < randomChildCount; i++)
        {
            GameObject randomChild = childNPCs[Random.Range(0, childNPCs.Length)]; // Get a random child NPC prefab
            GameObject childnewNpc = Instantiate(randomChild, spawnPoint.position, spawnPoint.rotation); // Instantiate a new child NPC at the spawn point
            childnewNpc.GetComponent<Children>().target = newNpc.transform; // Set the target of the child NPC to the newly spawned NPC
            NavMeshAgent childNpcAgent = childnewNpc.GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component of the child NPC
            childNpcAgent.speed = newNpc.GetComponent<NavMeshAgent>().speed; // Set the child's speed to match the parent's speed
        }
        npcInGame++; // Increment the number of NPCs in the game
        currentNpcCount++; // Increment the current NPC count
    }
}