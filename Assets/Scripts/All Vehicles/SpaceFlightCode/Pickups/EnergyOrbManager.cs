using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrbManager : MonoBehaviour {

    [SerializeField] GameObject energyOrb;
    Vector3 position;
    public float spawnRange = 100f;

    void Start()
    {
        position = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange));
        PlaceOrb();
    }

    void PlaceOrb()
    {
        Instantiate(energyOrb, position, Quaternion.identity);
    }
}
