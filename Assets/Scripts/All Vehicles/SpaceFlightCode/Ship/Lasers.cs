using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lasers : MonoBehaviour
{

    LineRenderer lr;
    bool canFire;
    [SerializeField] float laserOffTime = 0.5f;
    [SerializeField] float maxDistance = 300f;
    [SerializeField] float fireDelay = 2f;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        lr.enabled = false;
        canFire = true;
    }

 //   void Update()
 //   {
 //       Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.yellow);
 //   }


    Vector3 CastRay()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxDistance;

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            Debug.Log("We hit: " + hit.transform.name);
            return hit.point;
        }
       
        Debug.Log("We missed...");
        return transform.position + (transform.forward * maxDistance);

    }
    public void FireLaser()
    {
        if (canFire)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, CastRay());
            lr.enabled = true;
            canFire = false;
            Invoke("TurnOffLaser", laserOffTime);
            Invoke("CanFire", fireDelay);
        }
    }

    void TurnOffLaser()
    {
        lr.enabled = false;
        canFire = true;
    }

    public float Distance
    {
        get { return maxDistance; }
    }

    void CanFire()
    {
        canFire = true;
    }


   
}