/*
 Author: Ellysha Adriana Puteri
 Date Created: 7 August 2025
 Description: Controls the effects that appear when a thief npc is present
 + the player succesfully stops a thief
*/

using UnityEngine;

public class ThiefAlert : MonoBehaviour
{
    [Header("Exclamation Effect")]
    public GameObject exclamationPrefab; // Assign prefab in Inspector
    public Transform alertPoint; // Position above NPC's head

    private GameObject activeMark;

    [Header("Star Effect")]
    public GameObject starEffectPrefab; // Assign particle system prefab in Inspector

    [Header("Thief State")]
    public bool isStealing = false;
    public bool isCaught = false;
    public bool isOutOfStore = false;


    void Update()
    {
        // Show exclamation when stealing
        if (isStealing && !isCaught && !isOutOfStore)
        {
            if (activeMark == null)
            {
                activeMark = Instantiate(exclamationPrefab, alertPoint.position, Quaternion.identity);
                activeMark.transform.SetParent(alertPoint, true); // Follows NPC
            }
        }
        else
        {
            // Remove exclamation when not stealing
            if (activeMark != null)
            {
                Destroy(activeMark);
            }
        }
    }

    // Call this when NPC starts stealing
    public void StartStealing()
    {
        isStealing = true;
        isCaught = false;
        isOutOfStore = false;
    }

    // Call this when NPC leaves the store
    public void LeaveStore()
    {
        isOutOfStore = true;
        isStealing = false;
    }

    // Call this when player catches NPC
    public void CaughtByPlayer()
    {
        isCaught = true;
        isStealing = false;

        // Play star effect at NPC's position
        if (starEffectPrefab != null)
        {
            GameObject starEffect = Instantiate(starEffectPrefab, transform.position + Vector3.up * 1.5f, transform.rotation);
            starEffect.GetComponent<ParticleSystem>().Play();
        }
    }
}
