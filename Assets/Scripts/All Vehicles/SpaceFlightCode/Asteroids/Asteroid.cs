using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    [SerializeField] float minScale = .8f;
    [SerializeField] float maxScale = 1.2f;
    [SerializeField] float rotationOffset = 50f;
    public float movementSpeed;
    Transform myT;
    Vector3 randomRotation;
    Vector3 direction;
    public float boundary;

	void Awake ()
    {
        myT = transform;
	}

    void Start()
    {
        movementSpeed = Random.Range(20, 120);

        direction = (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f))).normalized;

        //random size
        Vector3 scale = Vector3.one;
        scale.x = Random.Range(minScale, maxScale);
        scale.y = Random.Range(minScale, maxScale);
        scale.z = Random.Range(minScale, maxScale);

        myT.localScale = scale;
        //random rotation

        randomRotation.x= Random.Range(-rotationOffset, rotationOffset);
        randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.z = Random.Range(-rotationOffset, rotationOffset);
        

    }
	
	
	void Update ()
    {
        myT.Rotate(randomRotation * Time.deltaTime);
        

        if(transform.position.x > boundary)
        {
            direction = -direction;
        }

        if (transform.position.y > boundary)
        {
            direction = -direction;
        }

        if (transform.position.z > boundary)
        {
            direction = -direction;
        }
    }

    void Move()
    {
       transform.position += direction * movementSpeed * Time.deltaTime;
    }
}
