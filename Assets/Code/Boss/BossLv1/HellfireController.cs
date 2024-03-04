using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellfireController : MonoBehaviour
{
    public Transform target;
    public GameObject destroyEffect;
    private Rigidbody2D rb;
    private float speed = 10f;
    bool targetSet = false;
    private float damage = 20f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("SetTarget", 0.35f);
        InvokeRepeating("UpdateTargetPosition", 0.35f, 0.1f);
    }

    void SetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Tank").transform != null)
        {
            target = GameObject.FindGameObjectWithTag("Tank").transform;
            targetSet = true;
            rb.gravityScale = 0;
        }
    }

    void UpdateTargetPosition()
    {
        if (targetSet)
        {
            SetTarget();
        }
    }

    void Update()
    {
        if (target != null && targetSet)
        {
            Vector2 direction = target.position - transform.position;
            transform.up = direction.normalized;
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        if(transform.position.y < -4f)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            rb.velocity = Vector2.zero;
            Destroy(gameObject);
        }
    }

    public void destroyHellfire()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force, bool leftLaucher)
    {
        if (leftLaucher)
        {
            transform.Rotate(0, 0, 40);
            Vector2 forceDirection = Quaternion.Euler(0, 0, 40) * direction;
            rb.AddForce(forceDirection * force, ForceMode2D.Impulse);
        }
        else
        {
            transform.Rotate(0, 0, -40);
            Vector2 forceDirection = Quaternion.Euler(0, 0, -40) * direction;
            rb.AddForce(forceDirection * force, ForceMode2D.Impulse);
        }
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
