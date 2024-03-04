using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crates4Controller : MonoBehaviour
{
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(transform.position.y < -4f)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
    }

    public void Launch(Vector2 direction, float force, bool lookRight)
    {
        if (lookRight)
        {
            direction = new Vector2(1, 0);
        }
        else
        {
            direction = new Vector2(-1, 0);
        }
        rb.AddForce(direction * force);
    }
}
