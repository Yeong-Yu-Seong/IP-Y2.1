/* Author: Yeong Yu Seong
Date Created: 28 July 2025
Description: Manages the UI elements and transitions between menus
*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Canvas for the main menu
    /// </summary>
    public Canvas Menu;
    /// <summary>
    /// Panel for settings
    /// </summary>
    [SerializeField]
    Image SettingsPanel;
    /// <summary>
    /// Flag to check if settings are open
    /// </summary>
    bool isSettingsOpen = false;
    /// <summary>
    /// Canvas for the game UI
    /// </summary>
    [SerializeField]
    Canvas GameCanvas;
    /// <summary>
    /// Canvas for the game over screen
    /// </summary>
    public Canvas GameOverCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Menu.enabled = true; // Enable the main menu canvas
        GameCanvas.enabled = false; // Disable the game canvas initially
        GameOverCanvas.enabled = false; // Disable the game over canvas initially
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// This method is called when the Play button is clicked.
    /// It hides the menu and shows the game canvas.
    /// It also sets the cursor visibility and lock state for gameplay.
    /// </summary>
    public void Play()
    {
        Menu.enabled = false; // Hide the main menu canvas
        Cursor.visible = false; // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the game window
        GameCanvas.enabled = true; // Show the game canvas
    }
    /// <summary>
    /// This method toggles the settings panel visibility.
    /// If the settings panel is open, it closes it; otherwise, it opens it.
    /// </summary>
    public void Settings()
    {
        //Checks if the settings panel is currently open or closed
        if (isSettingsOpen)
        {
            SettingsPanel.gameObject.SetActive(false); // If Settings are open, hide the settings panel
        }
        else
        {
            SettingsPanel.gameObject.SetActive(true); // If Settings are closed, show the settings panel
        }
        isSettingsOpen = !isSettingsOpen; // Toggle the settings open state
    }
    /// <summary>
    /// This method is called to return to the main menu.
    /// It enables the main menu canvas and disables the game canvas.
    /// </summary>
    public void BackToMenu()
    {
        Menu.enabled = true; // Show the main menu canvas
        GameCanvas.enabled = false; // Hide the game canvas
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
    }

    /// <summary>
    /// This method is called when the Restart button is clicked.
    /// It reloads the current scene to restart the game.
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(0); // Reload the current scene to restart the game
    }
}
