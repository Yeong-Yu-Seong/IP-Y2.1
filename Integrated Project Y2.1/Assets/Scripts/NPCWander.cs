/*
Author: Elly
Date created: 12 August 2025
Description: handles the movement of NPCs outside the store
*/

using UnityEngine;
using System.Collections;

public class NPCWander : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 2f;
    public float minIdleTime = 1f;
    public float maxIdleTime = 3f;
    public float minWalkTime = 2f;
    public float maxWalkTime = 4f;

    private Vector3 moveDirection;

    void Start()
    {
        StartCoroutine(WanderRoutine());
    }

    IEnumerator WanderRoutine()
    {
        while (true)
        {
            // Idle
            animator.SetBool("isWalking", false);
            yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));

            // Walk
            animator.SetBool("isWalking", true);
            moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

            float walkDuration = Random.Range(minWalkTime, maxWalkTime);
            float timer = 0f;

            while (timer < walkDuration)
            {
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
