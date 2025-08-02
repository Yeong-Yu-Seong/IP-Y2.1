/*
Author: Yeong Yu Seong
Date Created: 28 July 2025
Description: Manages the game state, score, and NPC spawning
*/

using UnityEngine;
using TMPro;
using System.Collections;

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
    int currentScore = 0;
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
    /// Number of thieves not caught in the game
    /// </summary>
    public int thievesNotCaught = 0;

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
            Cursor.visible = true; // Show the cursor when in menu
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor when in menu
            player.SetActive(false); // Disable the player GameObject when in menu
            isSpawning = false; // Reset the spawning flag when in menu
            CancelInvoke("SpawnNPC"); // Stop invoking the SpawnNPC method if in menu
        }
        else
        {
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
                    thievesCaughtText.text = "Thieves Caught: " + currentScore.ToString(); // Update the caught thieves text
                    thievesNotCaughtText.text = "Thieves Not Caught: " + thievesNotCaught.ToString(); // Update the not caught thieves text
                    Cursor.visible = true; // Show the cursor when game is over
                    Cursor.lockState = CursorLockMode.None; // Unlock the cursor when game is over
            }
            }
            else
            {
                Cursor.visible = false; // Hide the cursor when not in menu
                Cursor.lockState = CursorLockMode.Locked; // Lock the cursor when not in menu
                player.SetActive(true); // Enable the player GameObject when not in menu
            }
        }
        // Check if the player presses the Q key to go back to the main menu
        if (Input.GetKeyDown(KeyCode.Q))
        {
            uiManager.BackToMenu(); // Go back to the main menu when Q is pressed
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
        GameObject newNpc = Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation); // Instantiate a new NPC at the spawn point
        newNpc.GetComponent<Npc>().locations = locations; // Assign the locations to the NPC
        GameObject childnewNpc = Instantiate(npcchildPrefab, spawnPoint.position, spawnPoint.rotation); // Instantiate a new child NPC at the spawn point
        childnewNpc.GetComponent<Children>().target = newNpc.transform; // Set the target of the child NPC to the newly spawned NPC
        npcInGame++; // Increment the number of NPCs in the game
        currentNpcCount++; // Increment the current NPC count
    }
}