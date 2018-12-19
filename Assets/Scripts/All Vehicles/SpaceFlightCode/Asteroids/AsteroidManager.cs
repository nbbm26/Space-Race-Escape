using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {
    [SerializeField] Asteroid asteroid;
    Vector3 position;
    public float spawnRange = 2000f;
    public int numberOfAsteroids;


    void Start()
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            position = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange));
            PlaceAsteroid();
        }
       
    }


    void PlaceAsteroid()
    {
        Instantiate(asteroid, position, Quaternion.identity);
    }

}
