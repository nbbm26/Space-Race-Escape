using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

    [SerializeField] float rotationOffset = 50f;
    Vector3 position;
    Transform myT;
    public GameObject healthPack;
    Vector3 randomRotation;
    public float spawnRange = 2000f;

    void Start()
    {
        position = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange));
        randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.z = Random.Range(-rotationOffset, rotationOffset);
    }


    void Awake()
    {
        myT = transform;
    }


	void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime*20,0,0));
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerShip")
        {
            Destroy(healthPack);
            Instantiate(healthPack, position, Quaternion.identity);
        }
    }

    
}
