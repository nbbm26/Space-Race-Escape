using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickupManager : MonoBehaviour {

    [SerializeField] GameObject shieldPickup;
    Vector3 position;
    public float spawnRange = 2000f;

    void Start()
    {
        position = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange));
        PlaceShieldPickup();
    }


    void PlaceShieldPickup()
    {
        Instantiate(shieldPickup, position, Quaternion.identity);
    }
}
