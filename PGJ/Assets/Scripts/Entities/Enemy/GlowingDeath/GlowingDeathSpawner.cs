using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingDeathSpawner : MonoBehaviour
{
    [SerializeField] private GameObject glowingDeathPrefab;

    [SerializeField] private Transform[] possibleSpawnPoints;

    public void SpawnGlowingDeath() 
    {
        print("You fucked boy");

        if (possibleSpawnPoints.Length == 0)
            return;

        int index = Random.Range(0, possibleSpawnPoints.Length);
        Transform randomTransform = possibleSpawnPoints[index];

        Instantiate(glowingDeathPrefab, possibleSpawnPoints[index].position, Quaternion.identity);
    }
}
