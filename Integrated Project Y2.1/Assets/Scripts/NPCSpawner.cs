/*
Author: Elly
Date Created: 16 August 2025
Desc: Spawns NPCs that walk from one end to another
*/
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public Transform spawnPoint;         //Where NPCs spawn
    public Transform destinationPoint;   //Where NPCs walk to

    [Header("NPC Speed Settings")]
    public float minSpeed = 1.5f;        //Minimum random speed
    public float maxSpeed = 3.5f;        //Maximum random speed

    private float timer;
    private float spawnInterval = 7f;    //spawn time (7 seconds)
    [SerializeField]
    GameObject[] roamingNPCs;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnNPC();
            timer = 0f;
        }
    }

    void SpawnNPC()
    {
        int randomIndex = Random.Range(0, roamingNPCs.Length);
        GameObject npc = Instantiate(roamingNPCs[randomIndex], spawnPoint.position, spawnPoint.rotation);

        // Set the NPC's destination and random speed
        NPCWalkerOneWay walker = npc.GetComponent<NPCWalkerOneWay>();
        if (walker != null)
        {
            walker.destination = destinationPoint;
            walker.moveSpeed = Random.Range(minSpeed, maxSpeed); // Randomize speed
        }
    }
}
