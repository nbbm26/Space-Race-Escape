using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour {

    private Rigidbody theVehicle;
    private GameObject levelStatusObject;
    private LevelStatus levelStatusScript;
    private PlayerStatus playerStatusScript;
    private EnemyStatus enemyStatusScript;

    WheelDrivePlayer SpeedometerScript;
    private float topSpeed;


    //Respawn Variables
    private float rLoc_X = 0.0f;
    private float rLoc_Y = 0.0f;
    private float rLoc_Z = 0.0f;
    private float rLoc_W = 0.0f;
    private float pLoc_X = 0.0f;
    private float pLoc_Y = 0.0f;
    private float pLoc_Z = 0.0f;

    void Start()
    {
        //Sets the Respawn Location at start
        rLoc_X = transform.localRotation.x;
        rLoc_Y = transform.localRotation.y;
        rLoc_Z = transform.localRotation.z;
        rLoc_W = transform.localRotation.w;
        pLoc_X = transform.localPosition.x;
        pLoc_Y = transform.localPosition.y;
        pLoc_Z = transform.localPosition.z;

        //Variable holds the player vehicle's rigidbody
        theVehicle = this.GetComponent<Rigidbody>();

        //This holds the levels script criterias
        levelStatusObject = GameObject.Find("LevelStatusObject");
        levelStatusScript = levelStatusObject.GetComponent<LevelStatus>();
        playerStatusScript = theVehicle.gameObject.GetComponent<PlayerStatus>();

        SpeedometerScript = theVehicle.GetComponent<WheelDrivePlayer>();
        topSpeed = SpeedometerScript.criticalSpeed;
    }

    void Update()
    {
        Speedometer.ShowSpeed(theVehicle.velocity.magnitude, 0, topSpeed);
        //Respawn to last ring location when 'Z' key is pressed
        if (Input.GetKey(KeyCode.Z))
        {
            Respawn();
        }

        //Respawn to last ring location when 'LeftShift' key is pressed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ResetCar(transform.rotation.y);
        }

        Debug.Log("lOCAL" + transform.localRotation.w
            
            + " Normal" + transform.rotation.w);
    }

    //Detects when this GameObject enters a trigger
    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Item Entered");
    }

    //Detects when this GameObject is inside a trigger
    void OnTriggerStay(Collider coll)
    {

        Debug.Log("Somthing is Inside the trigger");
    }

    //Detects when this GameObject exits a trigger
    void OnTriggerExit(Collider coll)
    {

        //Looks for rings
        if (coll.CompareTag("Rings"))
        {
            //Rotation floats
            rLoc_X = transform.localRotation.x;
            rLoc_Y = transform.localRotation.y;
            rLoc_Z = transform.localRotation.z;
            rLoc_W = transform.localRotation.w;

            //Positions floats
            pLoc_X = transform.localPosition.x;
            pLoc_Y = transform.localPosition.y;
            pLoc_Z = transform.localPosition.z;

            if (levelStatusScript.ringsToCollect >= 1)
            {
                levelStatusScript.ringsToCollect--;
                Destroy(coll.gameObject);
                print("Rings left: " + levelStatusScript.ringsToCollect);

                if(levelStatusScript.ringsToCollect == 0)
                {
                    //Rewards the player 150 currency
                    playerStatusScript.currency += 150;
                }
            }
            else
            {
                print("You collected all needed rings");
            }
        }

        Debug.Log("Something Exited");
    }

    //Detects when this GameObject made a collision
    void OnCollisionEnter(Collision coll)
    {
        //Looks for rings
        if (coll.gameObject.CompareTag("Enemy"))
        {
            enemyStatusScript = coll.gameObject.GetComponent<EnemyStatus>();
            enemyStatusScript.health -= 5; 
            playerStatusScript.health -= 1;

            //Destroys the game object if the game object health is less than 1
            if (enemyStatusScript.health < 1)
            {
                //If there's no cars left to destroy
                if (levelStatusScript.carsToDestroyed < 1)
                {
                    print("You destroyed all the cars");
                }
                else
                {
                    levelStatusScript.carsToDestroyed -= 1;
                    print("You have " + levelStatusScript.carsToDestroyed + " left to destroy");
                }

                Destroy(coll.gameObject);
            }
        }

        Debug.Log("Collision occured");
    }

    void Respawn()
    {

        transform.localPosition = new Vector3(pLoc_X, pLoc_Y, pLoc_Z);
        transform.localRotation = new Quaternion(rLoc_X, rLoc_Y, rLoc_Z, rLoc_W);

        //Stops the velocity so it does not continue to fly while respawned.
        theVehicle.velocity = Vector3.zero;

        print("You just respawned to the previous location");

    }

    void ResetCar(float theLocY)
    {
            transform.localRotation = new Quaternion(0, transform.localRotation.y, 0, 0);

            //Stops the velocity so it does not continue to fly while respawned.
            theVehicle.velocity.Normalize();

        print("You just Flipped your car back up");
    }

}
