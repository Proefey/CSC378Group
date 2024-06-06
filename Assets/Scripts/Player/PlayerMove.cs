using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float moveSpeed = 1f;
    private float lookBoost = 3f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on " + gameObject.name);
        }
        audioSource.Play();
        audioSource.Pause();
    }

    void Update()
    {
        Move();
        
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if(moveHorizontal + moveVertical > 0.01f) {audioSource.Play();}
        else {audioSource.Pause();}

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        Vector2 movementDirection = movement.normalized;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerToMouseDirection = (mousePosition - rb.position).normalized;

        float speed = moveSpeed;

        if(Vector2.Dot(movementDirection, playerToMouseDirection) > 0.75f){
            speed *= lookBoost;
        }

        rb.velocity = movement * speed;
    }
}
