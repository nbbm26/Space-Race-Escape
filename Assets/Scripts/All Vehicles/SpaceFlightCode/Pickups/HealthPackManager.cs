using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackManager : MonoBehaviour {

    [SerializeField] GameObject healthPack;
    Vector3 position;
    public float spawnRange = 2000f;
    
    void Start()
    {
        position = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange));
        PlaceHealthPack();
    }


    void PlaceHealthPack()
    {
        Instantiate(healthPack, position, Quaternion.identity);
    }
}
