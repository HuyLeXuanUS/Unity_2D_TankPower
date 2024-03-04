using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rb;
    private float damage = 10f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(transform.position.magnitude > 300.0f || transform.position.y > 10 || transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force, float damage)
    {
        this.damage = damage;
        rb.AddForce(direction * force);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Enemy level
        EnemyLv1Controller enemyLv1Controller = other.GetComponent<EnemyLv1Controller>();
        EnemyLv2Controller enemyLv2Controller = other.GetComponent<EnemyLv2Controller>();
        EnemyLv3Controller enemyLv3Controller = other.GetComponent<EnemyLv3Controller>();

        // Orther enemy
        SmallcopterController smallcopterController = other.GetComponent<SmallcopterController>();
        DeltabomberController deltabomberController = other.GetComponent<DeltabomberController>();

        // Enemy weapon
        DumbbombController dumbbombController = other.GetComponent<DumbbombController>();
        HellfireController hellfireController = other.GetComponent<HellfireController>();

        // Boss level 1
        BossController bossController = other.GetComponent<BossController>();

        // Boss level 2
        GunBossController gunBossController = other.GetComponent<GunBossController>();
        MainHullController mainHullController = other.GetComponent<MainHullController>();

        // Boss level 3
        BossLv3Controller bossLv3Controller = other.GetComponent<BossLv3Controller>();

        if (enemyLv1Controller != null)
        {
            enemyLv1Controller.changeHealth(-damage);
        }
        else if (enemyLv2Controller != null)
        {
            enemyLv2Controller.changeHealth(-damage);
        }
        else if (enemyLv3Controller != null)
        {
            enemyLv3Controller.changeHealth(-damage);
        }

        if (smallcopterController != null)
        {
            smallcopterController.changeHealth(-damage);
        }
        else if (deltabomberController != null)
        {
            deltabomberController.changeHealth(-damage);
        }

        if (dumbbombController != null)
        {
            dumbbombController.destroyBomb();
        }
        else if (hellfireController != null)
        {
            hellfireController.destroyHellfire();
        }
        
        if (bossController != null)
        {
            bossController.changeHealth(-damage);
        }

        if (gunBossController != null)
        {
            gunBossController.changeHealth(-damage);
        }
        else if (mainHullController != null)
        {
            mainHullController.changeHealth(-damage);
        }

        if (bossLv3Controller != null)
        {
            bossLv3Controller.changeHealth(-damage);
        }

        Destroy(gameObject);
    }
}
