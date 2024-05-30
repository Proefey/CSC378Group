using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float wanderSpeed = 2f;
    public float chaseSpeed = 1f;
    public float detectionRadius = 1.2f;
    public float attackRange = .5f;
    public int damage = 10;
    public float wanderTime = 1f;

    private Transform player;
    private Vector3 wanderDirection;
    private float wanderTimer;
    private bool isChasing = false;

    [SerializeField] AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ChooseNewWanderDirection();
        audioSource.Play();
        audioSource.Pause();
    }

    void Update()
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

        Debug.Log(isChasing);
        if (isChasing && distanceToPlayer <= attackRange)
        {
            AttackPlayer();
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
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        transform.position += (Vector3)(directionToPlayer * chaseSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        // Assuming the player has a method called TakeDamage(int damage)
       // player.GetComponent<PlayerHealth>().TakeDamage(damage);
       audioSource.Play();
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