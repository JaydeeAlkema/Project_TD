using NaughtyAttributes;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public abstract class BaseTower : MonoBehaviour
{
    [BoxGroup("Prefabs")] public GameObject projectilePrefab = default;
    [Space]
    [BoxGroup("References")] public GameObject target = default;
    [BoxGroup("References")] public Transform headPivot = default;
    [BoxGroup("References")] public Transform Barrel = default;
    [Space]
    [BoxGroup("Settings")] public LayerMask detectionLayer = default;
    [BoxGroup("Settings")] public float detectionRange = 5f;
    [BoxGroup("Settings")] public float attackSpeed = 1f;
    [Space]
    [BoxGroup("Debugging")] public bool showDetectionRange = false;

    protected internal float attackSpeedTimer = 0f;

    /// <summary>
    /// Aim tower towards target.
    /// </summary>
    public virtual void Aim()
    {
        if (target == null) return;

        headPivot.right = target.transform.position - transform.position;
    }

    /// <summary>
    /// Shoot at Target.
    /// </summary>
    public virtual void Shoot()
    {
        if (target == null) return;

        attackSpeedTimer -= Time.deltaTime;
        if (attackSpeedTimer < 0f)
        {
            GameObject projectileGO = projectilePrefab;
            Projectile_Base projectile_Base = projectileGO.GetComponent<Projectile_Base>();

            projectile_Base.target = target;
            projectileGO.transform.up = projectile_Base.target.transform.position - projectileGO.transform.position;

            Instantiate(projectilePrefab, Barrel.transform.position, Quaternion.identity);
            attackSpeedTimer = attackSpeed;
        }
    }

    /// <summary>
    /// Get the nearest target to the tower.
    /// </summary>
    /// <returns> Nearest target (As GameObject). </returns>
    public virtual void GetNearestTarget()
    {
        GameObject nearestTarget = null;
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, detectionRange, detectionLayer);
        float dist = Mathf.Infinity;

        foreach (Collider2D collider in collidersInRange)
        {
            float distToTarget = Vector2.Distance(transform.position, collider.transform.position);
            if (distToTarget < dist)
            {
                dist = distToTarget;
                nearestTarget = collider.gameObject;
            }
        }

        if (nearestTarget == null) return;

        target = nearestTarget;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (showDetectionRange)
        {
            Handles.color = Color.cyan;
            Handles.DrawWireDisc(transform.position, Vector3.forward, detectionRange);
        }
    }
#endif
}
