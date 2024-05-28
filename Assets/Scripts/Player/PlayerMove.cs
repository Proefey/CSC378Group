using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 0.25f;
    public float lookBoost = 2f;

    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on " + gameObject.name);
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        Vector2 movementDirection = movement.normalized;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerToMouseDirection = (mousePosition - rb.position).normalized;

        float speed = moveSpeed;

        Debug.Log(Vector2.Dot(movementDirection, playerToMouseDirection));
        if(Vector2.Dot(movementDirection, playerToMouseDirection) > 0.75f){
            speed *= lookBoost;
        }

        rb.velocity = movement * speed;
    }
}
