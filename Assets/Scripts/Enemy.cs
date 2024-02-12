using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float chaseSpeed = 5f;
    public float detectionRange = 5f;

    private Transform target;
    private bool isChasing = false;

    private void Start() {}

    private void Update()
    {
            if (isChasing)
            {
                ChasePlayer();
            }
    }

    private void ChasePlayer()
    {
        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);

        // Check if player is out of detection range
        if (Vector2.Distance(transform.position, player.transform.position) > detectionRange)
        {
            isChasing = false;
        }
    }
}
