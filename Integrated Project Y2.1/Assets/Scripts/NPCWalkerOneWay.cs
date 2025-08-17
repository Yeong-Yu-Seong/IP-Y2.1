/*
Author: Elly
Date Created: 16 August 2025
Desc: controls speed of walking NPCs
*/
using System.Collections;
using UnityEngine;

public class NPCWalkerOneWay : MonoBehaviour
{
    [Header("Walk Settings")]
    public Transform destination;     // The point NPC walks to
    public float moveSpeed = 2f;      // Default speed (can be overridden)

    private void Start()
    {
        StartCoroutine(WalkToDestination());
    }

    IEnumerator WalkToDestination()
    {
        // Move until we're close enough to the destination
        while (Vector3.Distance(transform.position, destination.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destination.position,
                moveSpeed * Time.deltaTime
            );

            // Make NPC face the destination
            Vector3 direction = (destination.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            yield return null; // Wait until next frame
        }

        // Arrived — destroy NPC
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destination"))
        {
            Destroy(gameObject);
        }
    }
}

