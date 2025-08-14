/*
Author: Elly
Date Created: 14 August 2025
Desc: Triggers a UI text and red particles when the player lets a shoplifter escape
*/
using UnityEngine;
using TMPro;

public class ThiefEscapeUI : MonoBehaviour
{
    public GameObject popupText;             // Assign the "ThiefEscapedText" UI
    public ParticleSystem redDustEffect;     // Assign your RedDustEffect
    public float displayTime = 3f;

    public void ShowThiefEscaped()
    {
        popupText.SetActive(true);

        if (redDustEffect != null)
        {
            redDustEffect.Play(); // Play the red dust
        }

        Invoke(nameof(HidePopup), displayTime);
    }

    private void HidePopup()
    {
        popupText.SetActive(false);
    }
}
