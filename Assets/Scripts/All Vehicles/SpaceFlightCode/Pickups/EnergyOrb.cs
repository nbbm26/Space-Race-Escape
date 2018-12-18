using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : MonoBehaviour {

    [SerializeField] float rotationOffset = 50f;
    Vector3 randomRotation;
    Vector3 position;
    public float spawnRange = 2000f;
    Transform myT;
    public GameObject energyOrb;
    public static int orbCounter = 0;

    void Awake()
    {
        myT = transform;
    }

    void Start()
    {
        position = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange));
        randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.z = Random.Range(-rotationOffset, rotationOffset);
    }

    void Update()
    {
        myT.Rotate(randomRotation * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerShip")
        {
            orbCounter += 1;
            Instantiate(energyOrb, position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.tag == "Forcefield")
        {
            orbCounter += 1;
            Instantiate(energyOrb, position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
