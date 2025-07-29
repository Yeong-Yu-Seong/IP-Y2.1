using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Canvas Menu;
    public Button StartButton;
    public Button SettingsButton;
    public Image SettingsPanel;
    bool isSettingsOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Menu.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Play()
    {
        Menu.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Settings()
    {
        if (isSettingsOpen)
        {
            SettingsPanel.gameObject.SetActive(false);
        }
        else
        {
            SettingsPanel.gameObject.SetActive(true);
        }
        isSettingsOpen = !isSettingsOpen;
    }
}
