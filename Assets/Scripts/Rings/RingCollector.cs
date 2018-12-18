using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCollector : MonoBehaviour {

    private int numOfRing = 4;

    private Rigidbody ringPrefab;
    private Rigidbody theCar;

    private float rLoc_X = 0.0f;
    private float rLoc_Y = 0.0f;
    private float rLoc_Z = 0.0f;
    private float rLoc_W = 0.0f;
    private float pLoc_X = 0.0f;
    private float pLoc_Y = 0.0f;
    private float pLoc_Z = 0.0f;

    void Start()
    {
        rLoc_X = transform.localRotation.x;
        rLoc_Y = transform.localRotation.y;
        rLoc_Z = transform.localRotation.z;
        rLoc_W = transform.localRotation.w;
        pLoc_X = transform.localPosition.x;
        pLoc_Y = transform.localPosition.y;
        pLoc_Z = transform.localPosition.z;

        theCar = this.GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Z))
        {
            transform.localPosition = new Vector3(pLoc_X, pLoc_Y, pLoc_Z);
            transform.localRotation= new Quaternion(rLoc_X, rLoc_Y, rLoc_Z, rLoc_W);

            //Stops the velocity so it does not continue to fly while respawned.
            theCar.velocity = Vector3.zero;


            print("This is the update" + transform.localPosition);
        }
    }

    void OnTriggerEnter(Collider coll)
    {

        Debug.Log("Item Entered");
    }

    void OnTriggerStay(Collider coll)
    {

        Debug.Log("Somthing is Inside the trigger");
    }

    void OnTriggerExit(Collider coll)
    {

        Debug.Log("Something Exited");

        rLoc_X = transform.localRotation.x;
        rLoc_Y = transform.localRotation.y;
        rLoc_Z = transform.localRotation.z;
        rLoc_W = transform.localRotation.w;
        pLoc_X = transform.localPosition.x;
        pLoc_Y = transform.localPosition.y;
        pLoc_Z = transform.localPosition.z;

        numOfRing--;
        print("Rings left: "+ numOfRing);
        Destroy(coll.gameObject);
    }
}
