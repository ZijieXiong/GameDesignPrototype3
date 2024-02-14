using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 1f;
    public float chaseSpeed = 2f;
    public float detectionRange = 3f;

    public EnemySpawner roomSpawner;

    private Transform target;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;

    public AudioClip detectionSound; // Assign your detection sound in the Unity Editor
    private AudioSource audioSource;
    private bool hasPlayedSound = false;

    private void Start()
    {
        target = patrolPoints[currentPatrolIndex];
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        if (!hasPlayedSound && Vector3.Distance(transform.position, Player.instance.transform.position) < detectionRange)
        {
            PlayDetectionSound();
        }
    }

    private void Patrol()
    {
        // Move towards the current patrol point
        transform.position = Vector2.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);

        // Check if reached the current patrol point
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            // Move to the next patrol point
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            target = patrolPoints[currentPatrolIndex];
        }

        // Check for player within detection range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                isChasing = true;
                break;
            }
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

    private void PlayDetectionSound()
    {
        if (detectionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(detectionSound);
            hasPlayedSound = true;
            StartCoroutine(waiter());
            hasPlayedSound = false;
        }
    }

    IEnumerator waiter()
    {
        //Wait for 15 seconds
        yield return new WaitForSeconds(15);
    }
