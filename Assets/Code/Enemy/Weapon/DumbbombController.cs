using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbbombController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animatorDumbbomb;
    public GameObject destroyEffect;
    private float damage = 10f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animatorDumbbomb = GetComponent<Animator>();
    }

    void Update()
    {
        if(transform.position.y < -4f)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            rb.velocity = Vector2.zero;
            Destroy(gameObject);
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
        Vector2 lookdirection = direction.normalized;
        animatorDumbbomb.SetFloat("Look X", lookdirection.x);
        rb.AddForce(direction * force);
    }

    public void destroyBomb()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tank"))
        {
            other.gameObject.GetComponent<TankController>().changeHealth(damage);
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
