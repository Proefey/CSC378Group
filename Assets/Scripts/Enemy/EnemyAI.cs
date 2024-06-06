using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float timer;

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
        if (IsPlayerLookingAtEnemy() && distanceToPlayer <= 5){
            timer += Time.deltaTime;
            return;
        }
        else timer = 0;
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

        //Debug.Log(isChasing);
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
       SceneManager.LoadScene(0);
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

    private bool IsPlayerLookingAtEnemy()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(player.position);
        Vector3 enemyScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 playerToMouse = (mousePos - playerScreenPos).normalized;
        Vector2 playerToEnemy = (enemyScreenPos - playerScreenPos).normalized;

        float angle = Vector2.Angle(playerToMouse, playerToEnemy);

        // Assume the player is looking at the enemy if the angle is less than a certain threshold
        float lookAngleThreshold = 30f;
        return angle < lookAngleThreshold;
    }
}