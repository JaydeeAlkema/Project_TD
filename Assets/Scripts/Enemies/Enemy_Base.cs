using NaughtyAttributes;
using UnityEngine;

public class Enemy_Base : MonoBehaviour, IDamageable
{
    [BoxGroup("Prefabs")] public GameObject OnDeathPrefab = default;
    [Space]
    //[BoxGroup("References")] public GameObject target = default;
    [Space]
    [BoxGroup("Settings")] public int health = 10;
    [BoxGroup("Settings")] public float moveSpeed = 1f;

    protected internal int currentWaypointIndex = 0;

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
}
