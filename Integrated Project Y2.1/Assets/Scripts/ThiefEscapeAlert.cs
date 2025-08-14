/*
 Author: Ellysha Adriana Puteri
 Date Created: 7 August 2025
 Description: Controls the effects that appear when a thief NPC escapes
*/

using UnityEngine;
using TMPro;

public class ThiefEscapeAlert : MonoBehaviour
{
    public GameObject popupText;
    public ParticleSystem redDustEffect;     // RedDustEffect
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
