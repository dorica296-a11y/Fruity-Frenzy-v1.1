using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float speed = 2f; 
    int dir = 1;

    public Transform rightCheck;
    public Transform leftCheck;
    
    public LayerMask groundLayer; 

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * dir * Time.fixedDeltaTime);

        
        if (dir > 0) 
        {
            RaycastHit2D hit = Physics2D.Raycast(rightCheck.position, Vector2.down, 2.2f, groundLayer);
            if (hit.collider == null) 
            {
                dir = -1;
            }
        }
        else if (dir < 0) 
        {
            RaycastHit2D hit = Physics2D.Raycast(leftCheck.position, Vector2.down, 2.2f, groundLayer);
            if (hit.collider == null) 
            {
                dir = 1;
            }
        }
        
        Debug.DrawRay(rightCheck.position, Vector2.down * 2.2f, Color.red);
        Debug.DrawRay(leftCheck.position, Vector2.down * 2.2f, Color.blue);
    }
}
