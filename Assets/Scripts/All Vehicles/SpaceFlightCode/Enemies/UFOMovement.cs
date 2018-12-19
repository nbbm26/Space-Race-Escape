using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMovement : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float movementSpeed = 15f;
    [SerializeField] float rotationalDamp = .5f;
    [SerializeField] float rayCastOffset = 10f;
    [SerializeField] float detectionDistance = 20f;
    

    void Update()
    {
        Move();
        Pathfinding();
        transform.Rotate(Vector3.right * Time.deltaTime);
    }

    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }


	void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void Pathfinding()
    {
        RaycastHit hit;
        Vector3 raycastOffset = Vector3.zero;

        Vector3 left = transform.position - transform.right*rayCastOffset;
        Vector3 right = transform.position + transform.right * rayCastOffset;
        Vector3 up = transform.position + transform.up * rayCastOffset;
        Vector3 down = transform.position - transform.up * rayCastOffset;

        if (Physics.Raycast(left, transform.forward, out hit, detectionDistance))
            raycastOffset += Vector3.right;
        else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance))
            raycastOffset -= Vector3.right;

        if (Physics.Raycast(up, transform.forward, out hit, detectionDistance))
            raycastOffset -= Vector3.up;
        else if (Physics.Raycast(down, transform.forward, out hit, detectionDistance))
            raycastOffset += Vector3.up;

        if (raycastOffset != Vector3.zero)
            transform.Rotate(raycastOffset * 5f * Time.deltaTime);
        else
            Turn();
        

    }
}
