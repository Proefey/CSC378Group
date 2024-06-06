using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float wanderSpeed = 2f;
    public float chaseSpeed = 1f;
    public float detectionRadius = 1.2f;
    public float attackRange = 0.5f;
    public int damage = 10;
    public float wanderTime = 1f;

    private Transform player;
    private Vector3 wanderDirection;
    private float wanderTimer;
    private bool isChasing = false;

    [SerializeField] AudioSource audioSource;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
        ChooseNewWanderDirection();
        audioSource.Play();
        audioSource.Pause();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRadius)
            {
                isChasing = true;
                ChasePlayer();
            }
            else
            {
                isChasing = false;
                Wander();
            }

            if (isChasing && distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            // Handle case when player is null (e.g., player destroyed)
            isChasing = false;
            Wander();
        }
    }

    void Wander()
    {
        transform.position += (Vector3)(wanderDirection * wanderSpeed * Time.deltaTime);
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0)
        {
            ChooseNewWanderDirection();
        }
    }

    void ChooseNewWanderDirection()
    {
        wanderDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        wanderTimer = wanderTime;
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            transform.position += (Vector3)(directionToPlayer * chaseSpeed * Time.deltaTime);
        }
    }

    void AttackPlayer()
    {
        if (player != null)
        {
            // Assuming the player has a method called TakeDamage(int damage)
            // player.GetComponent<PlayerHealth>().TakeDamage(damage);
            audioSource.Play();
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Draw the attack range in the editor
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
