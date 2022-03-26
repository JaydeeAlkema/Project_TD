using NaughtyAttributes;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile_Base : MonoBehaviour
{
    [BoxGroup("Prefabs")] public GameObject trailPrefab = default;
    [BoxGroup("Prefabs")] public GameObject onHitPrefab = default;
    [Space]
    [BoxGroup("References")] public GameObject target = default;
    [BoxGroup("References")] public Rigidbody2D rb2d = default;
    [Space]
    [BoxGroup("Settings")] public RotationPivotVector RotationPivotVector = RotationPivotVector.UP;
    [BoxGroup("Settings")] public CollisionType collisionType = CollisionType.SpecificTarget;
    [BoxGroup("Settings")] public int damageOnHit = 1;
    [BoxGroup("Settings")] public float moveSpeed = 20f;
    [Space]
    [BoxGroup("Debugging")] public bool showTarget = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    /// <summary>
    /// Move towards the target gameobject.
    /// </summary>
    public virtual void MoveTowardsTarget()
    {
        if (target == null) return;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        switch (RotationPivotVector)
        {
            case RotationPivotVector.UP:
                transform.up = target.transform.position - transform.position;
                break;

            case RotationPivotVector.RIGHT:
                transform.right = target.transform.position - transform.position;
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Handle different types of collision types.
    /// </summary>
    /// <param name="collision"> Reference to the Collision Component. </param>
    public void CollisionBehaviour(Collider2D collision)
    {
        switch (collisionType)
        {
            case CollisionType.FireAndForget:
                if (collision == null) break;

                collision.GetComponent<IDamageable>()?.Damage(damageOnHit);
                break;

            case CollisionType.SpecificTarget:
                if (collision == null) break;
                if (collision.gameObject != target) break;

                collision.GetComponent<IDamageable>()?.Damage(damageOnHit);
                break;

            default:
                break;
        }

        if (onHitPrefab != null)
        {
            Instantiate(onHitPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
