public class Projectile_StandardBullet : Projectile_Base
{
    void Update()
    {
        MoveTowardsTarget();
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        CollisionBehaviour(collision);
    }
}
