public class Tower_Gunner_T1 : BaseTower
{
    void Update()
    {
        GetNearestTarget();
        Aim();
        Shoot();
    }
}
