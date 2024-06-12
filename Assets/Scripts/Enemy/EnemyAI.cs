using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public float wanderSpeed = 2f;
    public float chaseSpeed = 1f;
    //Slow down enemy while player looks at them
    private float chaseMultiplier = 1f;
    public float detectionRadius = 1.2f;
    public float attackRange = .5f;
    public int damage = 10;
    public float wanderTime = 1f;

    private Transform player;
    private Transform enemy;
    private Vector3 wanderDirection;
    private float wanderTimer;
    private bool isChasing = false;
    private float timer;
    private float angertimer;
    private bool angry = false;

    //Scale Difficulty
    private int itemcount = 0;
    private int prevcount = 1;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource flashlightDistortion;
    [SerializeField] AudioSource angryScream;
    public PlayerController itemcounter;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        ChooseNewWanderDirection();
        audioSource.Play();
        audioSource.Pause();
    }

    void Update()
    {  
        Debug.Log(flashlightDistortion.isPlaying);
        //Handle Anger
        if(angry){
            angertimer += Time.deltaTime;
            if (angertimer >= 10){
                angryScream.Pause();
                angertimer = 0;
                angry = false;
            }
        }
        //Reset Enemy To Middle
        itemcount = itemcounter.getItemCount();
        if (itemcount > 0 && itemcount != prevcount){
            enemy.transform.position = new Vector2(2f,4f);
            prevcount = itemcount;
            return;
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (IsPlayerLookingAtEnemy() && distanceToPlayer <= 5){
            timer += Time.deltaTime;
            switch(itemcount){
                case 0:
                case 1:
                    chaseMultiplier = 0f;
                    break;
                case 2:
                    chaseMultiplier = 0.25f;
                    break;
                case 3:
                    chaseMultiplier = 0.5f;
                    break;
                default:
                    chaseMultiplier = 0.75f;
                    break;
            }
            if (timer >= 5){
                if (!angryScream.isPlaying) angryScream.Play();
                angry = true;
            }
        }
        else {
            timer = 0;
            chaseMultiplier = 1f;
        }

        if (distanceToPlayer <= 5){
            float volume = distanceToPlayer / 10;
            flashlightDistortion.volume = volume;
            if (!flashlightDistortion.isPlaying) flashlightDistortion.Play();
        }
        else{
            flashlightDistortion.Pause();
        }
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
        if(angry) chaseMultiplier = 1.0f;
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        transform.position += (Vector3)(directionToPlayer * chaseSpeed * chaseMultiplier * Time.deltaTime);
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

    private bool IsPlayerLookingAtEnemy(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(player.position);
        Vector3 enemyScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 playerToMouse = (mousePos - playerScreenPos).normalized;
        Vector2 playerToEnemy = (enemyScreenPos - playerScreenPos).normalized;

        float angle = Vector2.Angle(playerToMouse, playerToEnemy);

        float lookAngleThreshold = 30f;
        return angle < lookAngleThreshold;
    }

    private void handleItemCounter(){

    }

}