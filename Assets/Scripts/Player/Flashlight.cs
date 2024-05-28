using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3[] vertices;
    private int[] triangles;
    void Start()
    {
        
    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        transform.up = Vector2.MoveTowards(transform.up, direction, 0.2f);
    }
}
