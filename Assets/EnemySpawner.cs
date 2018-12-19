using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject ufo;
    Vector3 position;
    public int ufoSpawn=2;
    public float spawnRange = 2000f;

    void Start()
    {
        SpawnUFO();
    }

    void SpawnUFO()
    {
        for(int i=0; i<ufoSpawn; i++)
        {
            position = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange));
            GameObject go = GameObject.Instantiate(ufo, position, Quaternion.identity) as GameObject;
        }
    }
}
