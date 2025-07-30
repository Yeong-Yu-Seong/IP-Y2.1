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
    public GameObject player;
    /// <summary>
    /// TextMeshProUGUI component to display the score
    /// </summary>
    public TextMeshProUGUI scoreText;
    /// <summary>
    /// Prefab for the NPC to spawn
    /// </summary>
    public GameObject npcPrefab;
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
    public TextMeshProUGUI thievesCaughtText;
    public TextMeshProUGUI thievesNotCaughtText;
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
            StopCoroutine(SpawnNPCCoroutine()); // Stop the NPC spawning coroutine if in menu
        }
        else
        {
            if (!isSpawning)
            {
                isSpawning = true; // Set the flag to true to prevent multiple coroutines
                StartCoroutine(SpawnNPCCoroutine()); // Start the NPC spawning coroutine
            }
            else if (isGameOver && npcInGame == 0)
            {
                uiManager.GameOverCanvas.enabled = true; // Show the game over canvas if no NPCs are in the game
                thievesCaughtText.text = "Thieves Caught: " + currentScore.ToString(); // Update the caught thieves text
                thievesNotCaughtText.text = "Thieves Not Caught: " + thievesNotCaught.ToString(); // Update the not caught thieves text
                Cursor.visible = true; // Show the cursor when game is over
                Cursor.lockState = CursorLockMode.None; // Unlock the cursor when game is over
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
            StopCoroutine(SpawnNPCCoroutine()); // Stop the NPC spawning coroutine
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
    /// </summary>
    void SpawnNPC()
    {
        GameObject newNpc = Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation); // Instantiate a new NPC at the spawn point
        newNpc.GetComponent<Npc>().locations = locations; // Assign the locations to the NPC
    }

    /// <summary>
    /// This coroutine handles the spawning of NPCs in the game.
    /// It checks the number of NPCs in the game and spawns new ones at regular intervals.
    /// It stops spawning when the total number of NPCs has been reached or if the limit of 10 NPCs is reached.
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnNPCCoroutine()
    {
        while (true)
        {
            if (currentNpcCount >= totalNpcToSpawn) // Stop spawning if the total number of NPCs has been reached
            {
                isGameOver = true; // Set the game over flag to true
                yield break; // Exit the coroutine
            }
            SpawnNPC(); // Call the method to spawn a new NPC
            npcInGame++; // Increment the number of NPCs in the game
            currentNpcCount++; // Increment the current NPC count
            yield return new WaitForSeconds(10f); // Wait for 10 seconds before spawning the next NPC
        }
    }
}