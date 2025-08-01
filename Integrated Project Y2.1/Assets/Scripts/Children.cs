/*
Author: Mo Xuan'en Shannon
Date Created: 30 July 2025
Description: Controls the child's NPC behavior
*/

using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the behavior of a child NPC that can either idle or follow a target based on distance.
/// </summary>
public class Children : MonoBehaviour
{
    /// <summary>
    /// Possible behavior states for the child NPC.
    /// </summary>
    public enum State { Idle, Follow }

    /// <summary>
    /// The current state of the NPC (Idle or Follow).
    /// </summary>
    public State currentState = State.Idle;

    /// <summary>
    /// The target that the child NPC will follow.
    /// </summary>
    public Transform target;

    /// <summary>
    /// The distance at which the NPC will start following the target.
    /// </summary>
    public float followDistance = 3f;

    /// <summary>
    /// The distance at which the NPC will stop following and return to idle.
    /// </summary>
    public float stopDistance = 2f;

    /// <summary>
    /// The NavMeshAgent component used for navigation.
    /// </summary>
    private NavMeshAgent agent;

    /// <summary>
    /// Initializes the NavMeshAgent component.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Updates the NPC's state and movement based on the distance to the target.
    /// </summary>
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroy the child NPC if the target is null
            return;
        }
        float distanceFromTarget = Vector3.Distance(transform.position, target.position);

        switch (currentState)
        {
            case State.Idle:
                // Switch to Follow state if the target is far enough
                if (distanceFromTarget > followDistance)
                    currentState = State.Follow;
                break;

            case State.Follow:
                // Switch to Idle state if close enough to the target
                if (distanceFromTarget <= stopDistance)
                    currentState = State.Idle;
                else
                    // Move towards the target
                    agent.SetDestination(target.position);
                break;
        }
    }
}
