/*
Author: Elly
Date created: 14 August 2025
Desc: Triggers exclamation mark when an NPC attempts to steal
Disappears if NPC is caught or if they get away
*/
using UnityEngine;

public class ExclamationMark : MonoBehaviour
{
    [Header("Exclamation Effect")]
    public GameObject exclamationPrefab; // Assign prefab in Inspector
    public Transform alertPoint; // Position above NPC's head

    private GameObject activeMark;

    [Header("Thief State")]
    public bool isStealing = false;
    public bool isCaught = false;
    public bool isOutOfStore = false;

    void Update()
    {
        // If stealing and alert is not active → show it
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
            // Hide if they stop stealing, get caught, or leave
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
    }
}
