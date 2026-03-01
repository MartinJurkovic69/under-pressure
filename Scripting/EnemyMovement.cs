using UnityEngine;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    public MapPath path;
    public float speed = 3f;
    public bool isInitialized = false;
    public bool isBlocked = false;
    
    [SerializeField] private int currentWaypointIndex = 0;

    void Update()
    {
        if (!isInitialized || path == null || path.waypoints.Count == 0 || isBlocked)
            return;

        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        Vector3 targetPos = path.waypoints[currentWaypointIndex].position;
        targetPos.y = transform.position.y; 

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.LookAt(targetPos);

        if (Vector3.Distance(transform.position, targetPos) < 0.3f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= path.waypoints.Count)
            {
                OnReachedBase();
            }
        }
    }

    void OnReachedBase()
    {
        MainBase shelter = Object.FindFirstObjectByType<MainBase>();
        if (shelter != null)
        {
            shelter.TakeDamage(10f, gameObject);
        }
        Destroy(gameObject);
    }
}