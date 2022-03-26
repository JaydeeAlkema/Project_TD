using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsManager : MonoBehaviour
{
    private static WaypointsManager instance;

    [BoxGroup("References")][SerializeField] private Transform waypointsParent = default;
    [BoxGroup("References")][SerializeField] private List<Transform> waypoints = new List<Transform>();
    [Space]
    [BoxGroup("Debugging")][SerializeField] private bool drawWaypoints = false;

    public static WaypointsManager Instance { get => instance; private set => instance = value; }

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }

    private void Start()
    {
        GetAllWaypointsFromParentTransform();
    }

    /// <summary>
    /// Get's all waypionts from the waypoints parent.
    /// </summary>
    [Button("Get Waypoints")]
    private void GetAllWaypointsFromParentTransform()
    {
        waypoints.Clear();

        Transform[] children = waypointsParent.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (waypoints.Contains(child) == false && child != waypointsParent)
            {
                waypoints.Add(child);
            }
        }
    }

    /// <summary>
    /// Get list of waypoints.
    /// </summary>
    /// <returns> Waypoints List<>. </returns>
    public List<Transform> GetWaypoints()
    {
        if (waypoints == null || waypoints.Count == 0) return null;

        return waypoints;
    }

    /// <summary>
    /// Get's Waypoint transform at index.
    /// </summary>
    /// <param name="index"> Index. </param>
    /// <returns> Waypoint Transform. </returns>
    //public Transform GetWaypointAtIndex(int index)
    //{
    //    if (waypoints[index] == null) return null;

    //    return waypoints[index];
    //}

    private void OnDrawGizmosSelected()
    {
        if (drawWaypoints)
        {
            if (waypoints.Count == 0) return;

            for (int i = 0; i < waypoints.Count; i++)
            {
                Transform waypoint = waypoints[i];
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(waypoint.position, 0.1f);

                if (i + 1 == waypoints.Count) return;
                Transform nextWaypoint = waypoints[i + 1];
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(waypoint.position, nextWaypoint.position);
            }
        }
    }
}
