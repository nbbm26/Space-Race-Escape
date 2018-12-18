using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour {

    public Transform missileSpawn;
    public GameObject missile;
    public float fireDelay = .5f;
    public bool canFire;


    void Start()
    {
        canFire = true;
    }

	void Update()
    {
        if (canFire)
        {
            FireMissile();
        }
        canFire = false;
        Invoke("CanFire", fireDelay);
    }

    void FireMissile()
    {
        GameObject go = GameObject.Instantiate(missile, missileSpawn.position, missileSpawn.rotation) as GameObject;
        GameObject.Destroy(go, 5f);
    }

    void CanFire()
    {
        canFire = true;
    }

    
}
