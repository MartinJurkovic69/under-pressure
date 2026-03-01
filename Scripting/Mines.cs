using UnityEngine;

public class TacticalMine : MonoBehaviour
{
    public float damage = 50f;
    public float explosionRadius = 3f;
    public GameObject mineExplosionEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Detonate();
        }
    }

    void Detonate()
    {
        if (mineExplosionEffect) Instantiate(mineExplosionEffect, transform.position, Quaternion.identity);

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in enemies)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyBase>().TakeDamage(damage, gameObject);
            }
        }

        Destroy(gameObject);
    }
}