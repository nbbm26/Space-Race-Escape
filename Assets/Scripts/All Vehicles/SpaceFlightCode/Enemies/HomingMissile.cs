using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{

    [SerializeField] float movementSpeed = 50f;
    [SerializeField] float rotationalDamp = .5f;
    public GameObject homingMissile;
   
    void Update()
    {
        Move();
    }


    void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

}
