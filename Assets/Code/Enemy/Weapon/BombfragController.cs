using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombfragController
 : MonoBehaviour
{
    Rigidbody2D rb;
    private float damage = 30f;
    public GameObject destroyEffect;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(transform.position.y < -4.5f)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            rb.velocity = Vector2.zero;
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    public void BossLaunch(Vector2 direction, float force)
    {

        float random = Random.Range(-20, 20);
        transform.Rotate(0, 0, random);
        Vector2 forceDirection = Quaternion.Euler(0, 0, random) * direction;
            
        rb.gravityScale = 1;
        rb.AddForce(forceDirection * force, ForceMode2D.Impulse);

    }

    public void destroyBomb()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tank"))
        {
            other.gameObject.GetComponent<TankController>().changeHealth(damage);
            Destroy(gameObject);
        }
    }
}
