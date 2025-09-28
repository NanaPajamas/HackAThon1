using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds2 = new(2f);
    public List<Transform> enemySpawnPoints;
    public GameObject enemyPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return _waitForSeconds2;

        foreach (Transform enemyPos in enemySpawnPoints)
        {
            Instantiate(enemyPrefab, enemyPos);
        }
    }
}
