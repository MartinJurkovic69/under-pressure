using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Stats")]
    public float health = 50f;
    public float speed = 3f;
    public float detectionRadius = 10f;
    public float attackRange = 2.2f; 
    public float damage = 10f;
    public float attackCooldown = 1.0f;

    [Header("Raycast Settings")]
    public float heightOffset = 1.0f; 
    public LayerMask groundLayer;    

    protected Transform target;
    protected EnemyMovement movement;
    private float nextAttackTime;

    public virtual void Start()
    {
        movement = GetComponent<EnemyMovement>();
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    public virtual void Update()
    {
        FindPriorityTarget();
        HandleMovementAndAttack();
        ApplyRaycastGrounding();
    }

    void HandleMovementAndAttack()
{
    if (target == null) return;
    
    float dist = Vector3.Distance(transform.position, target.position);

    if (dist > attackRange + 0.2f)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;

        transform.position += direction * speed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }
    }
    else
    {
        TryAttack(target.gameObject);
    }
}
    void ApplyRaycastGrounding()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 2f, Vector3.down, out hit, 10f, groundLayer))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + heightOffset, transform.position.z);
        }
    }

    void FindPriorityTarget()
    {
        target = null;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRadius)
            target = player.transform;

        if (target == null)
        {
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
            float closest = detectionRadius;
            foreach (GameObject t in towers)
            {
                float d = Vector3.Distance(transform.position, t.transform.position);
                if (d < closest) { closest = d; target = t.transform; }
            }
        }
        if (movement != null) movement.isBlocked = (target != null);
    }

    void TryAttack(GameObject attackTarget)
    {
        if (Time.time >= nextAttackTime)
        {
            attackTarget.GetComponent<PlayerController>()?.TakeDamage(damage, gameObject);
            attackTarget.GetComponent<MainBase>()?.TakeDamage(damage, gameObject);
            attackTarget.GetComponent<TowerBase>()?.TakeDamage(damage, gameObject);
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public virtual void TakeDamage(float amount, GameObject source)
    {
        health -= amount;
        if (health <= 0) Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}