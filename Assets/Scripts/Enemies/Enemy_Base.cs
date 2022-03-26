using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Base : MonoBehaviour, IDamageable
{
    [BoxGroup("Prefabs")] public GameObject OnDeathPrefab = default;
    [Space]
    [BoxGroup("References")] public List<Transform> waypoints = new List<Transform>();
    [BoxGroup("References")] public Transform target = default;
    [Space]
    [BoxGroup("Settings")] public int health = 10;
    [BoxGroup("Settings")] public float moveSpeed = 1f;

    protected internal int currentWaypointIndex = 0;

    public void Start()
    {
        waypoints = WaypointsManager.Instance.GetWaypoints();
        target = waypoints[currentWaypointIndex];
        transform.position = target.position;
    }

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if (OnDeathPrefab)
            {
                GameObject onDeathGO = Instantiate(OnDeathPrefab, transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Move towards target position.
    /// </summary>
    public virtual void Move()
    {
        if (Vector3.Distance(transform.position, target.position) <= 0.01f)
        {
            transform.position = target.position;
            currentWaypointIndex++;
            if (currentWaypointIndex < waypoints.Count)
            {
                target = waypoints[currentWaypointIndex];
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            transform.right = target.transform.position - transform.position;
        }
    }
}
