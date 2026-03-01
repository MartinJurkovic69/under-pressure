using UnityEngine;

public class Barricade : TowerBase
{
    public GameObject minePrefab;
    public float spawnRate = 5f;
    public float explosionRadius = 5f;
    public float explosionDamage = 50f; // Added this to fix the error
    public GameObject explosionEffect; 

    void Start() 
    { 
        InvokeRepeating("SpawnMine", spawnRate, spawnRate); 
    }

    void SpawnMine() 
    { 
        if (minePrefab == null) return;

        Vector3 spawnPos = transform.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
        Instantiate(minePrefab, spawnPos, Quaternion.identity); 
    }

    // Use OnDestroy carefully; it triggers when the game stops too.
    // The isLoaded check you added is good practice!
    void OnDestroy() 
    { 
        if (gameObject.scene.isLoaded)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (explosionEffect) Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in enemies)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyBase enemyScript = hit.GetComponent<EnemyBase>();
                if (enemyScript != null)
                {
                    // Changed 'damage' to 'explosionDamage'
                    enemyScript.TakeDamage(explosionDamage * 3f, gameObject);
                }
            }
        }
    }
}