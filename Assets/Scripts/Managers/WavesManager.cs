using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [BoxGroup("References")] public List<GameObject> enemiesInScene = new List<GameObject>();
    [BoxGroup("References")] public List<ScriptableEnemyWave> waves = new List<ScriptableEnemyWave>();
    [Space]
    [BoxGroup("References")] public Transform enemiesParentTransform = null;
    [BoxGroup("References")] public Transform spawnPoint = null;
    [BoxGroup("References")] public int currentWaveIndex = 0;

    [Button("Start Wave")]
    public void StartNextWave()
    {
        StartCoroutine(StartNextWaveCoroutine());
    }

    public IEnumerator StartNextWaveCoroutine()
    {
        if (currentWaveIndex >= waves.Count) yield return null;

        ScriptableEnemyWave currentWave = waves[currentWaveIndex];

        if (currentWaveIndex >= waves.Count)
        {
            // End the game, the player has won!
            Debug.Log("YOU WON!");
            yield return null;
        }
        else
        {
            int enemyIndex = currentWave.enemyIndex;
            GameObject enemyPrefab = currentWave.enemyPrefab;
            currentWaveIndex = Mathf.Clamp(currentWaveIndex += 1, 0, waves.Count);

            while (enemyIndex < currentWave.enemyCount)
            {
                GameObject newEnemyGO = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, enemiesParentTransform);
                enemiesInScene.Add(newEnemyGO);
                enemyIndex++;
                yield return new WaitForSeconds(currentWave.timeToSpawn);
            }
        }
    }
}