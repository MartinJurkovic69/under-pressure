using UnityEngine;
using System.Collections.Generic;

public class MapPath : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();

    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count == 0) return;

        for (int i = 0; i < waypoints.Count; i++)
        {
            if (waypoints[i] == null) continue;

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(waypoints[i].position, 0.3f);

            if (i < waypoints.Count - 1 && waypoints[i + 1] != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }
}