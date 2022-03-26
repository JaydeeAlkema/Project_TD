using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Scriptable Enemy Wave", fileName = "Enemy Wave")]
public class ScriptableEnemyWave : ScriptableObject
{
    public GameObject enemyPrefab;
    public int enemyIndex;
    public int enemyCount;
    public float timeToSpawn;
}
