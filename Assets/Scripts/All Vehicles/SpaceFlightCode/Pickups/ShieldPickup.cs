using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour {

    [SerializeField] float rotationOffset = 50f;
    Transform myT;
    public GameObject shieldPickup;
    GameObject forcefield;
    private Light light; 
    Vector3 randomRotation;
    Vector3 position;
    public float spawnRange = 2000f;



    void Start()
    {
        position = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange));
        randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.z = Random.Range(-rotationOffset, rotationOffset);
        forcefield = GameObject.Find("Forcefield");
        myT = transform;
        light = GetComponent<Light>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * 20, 0, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerShip")
        {
            other.gameObject.transform.parent.parent.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject);
            Instantiate(shieldPickup, position, Quaternion.identity);
            Invoke("ShieldOff", 10);
        }
    }

    void ActivateShield()
    {
        forcefield.SetActive(true);
    }

    void ShieldOff()
    {
        GameObject.Find("PlayerShip").transform.parent.parent.transform.GetChild(0).gameObject.SetActive(false);
    }
}
