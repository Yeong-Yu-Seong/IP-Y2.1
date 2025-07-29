using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    public bool reachedShopPoint = false;
    public Transform[] locations;
    // This is a list of locations the npc will walk to
    private int currentLocationIndex = 0;

    [SerializeField]
    Transform currentLocation;
    NavMeshAgent myAgent;
    public string currentState;
    public bool stolen = false; // Flag to indicate if the item has been stolen

    void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();
        currentLocationIndex = 0; // Initialize the current location index
    }

    void Start()
    {
        StartCoroutine(SwitchState("Idle"));
    }

    IEnumerator SwitchState(string newState)
    {
        if (currentState == newState)
        {
            yield break; // Exit if the state is already the same
        }
        StopCoroutine(currentState); // Stop the current state coroutine if it exists

        currentState = newState;

        StartCoroutine(currentState);
    }

    IEnumerator Walking()
    {
        reachedShopPoint = false; // Reset the flag at the start of walking
        currentLocation = locations[currentLocationIndex]; // Set the initial walking location
        myAgent.SetDestination(currentLocation.position); // Set the agent's destination to the current walking point
        Debug.Log("Starting walking to " + currentLocationIndex);
        while (currentState == "Walking")
        {
            if (reachedShopPoint)
            {
                // Go to next location in the route
                if (currentLocationIndex == locations.Length - 1)
                {
                    currentLocationIndex = 0; // Reset to the first location if at the end
                }
                else
                {
                    currentLocationIndex += 1;
                }
                StartCoroutine(SwitchState("Stolen")); // Switch to stolen state if reached route point
                yield break; // Exit the Walking coroutine
            }
            yield return null; // Wait for the next frame
        }
    }

    IEnumerator Idle()
    {
        float idleTime = 0f; // Timer for idle state
        while (currentState == "Idle")
        {
            // Perform idle behavior here
            idleTime += Time.deltaTime; // Increment idle time
            if (idleTime >= 5f) // If idle for 5 seconds, switch to walking
            {
                StartCoroutine(SwitchState("Walking"));
                yield break; // Exit the idle coroutine
            }
            yield return null; // Wait for the next frame
        }
    }
    IEnumerator Stolen()
    {
        // Generate a random number to determine if the item is stolen
        float randomChance = Random.Range(0f, 1f);
        if (randomChance < .05f) // 5% chance to steal
        {
            stolen = true; // Set the stolen flag to true
            myAgent.isStopped = true; // Stop the agent's movement
            Debug.Log("Item has been stolen!");
            yield return new WaitForSeconds(10f); // Wait for 10 seconds before switching state
        }
        else
        {
            Debug.Log("Item not stolen, continuing patrol.");
        }
        StartCoroutine(SwitchState("Idle")); // Switch back to idle state
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Location"))
        {
            if (other.transform == locations[currentLocationIndex])
            {
                reachedShopPoint = true;
            }
        }
    }
}