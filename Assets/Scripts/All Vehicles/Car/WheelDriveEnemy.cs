using UnityEngine;
using UnityEngine.AI;
using System;

public class WheelDriveEnemy : MonoBehaviour
{
    [Tooltip("Gets the car's actual body")]
    Rigidbody rb;
    GameObject Front, Back, Left, Right;
    public Transform target;
    private NavMeshAgent agent;


    [Tooltip("Maximum steering angle of the wheels")]
    public float maxAngle = 50f;
    [Tooltip("Maximum steering angle of the wheels")]
    public float maxAngleTorque = 50f;
    [Tooltip("Maximum torque applied to the driving wheels")]
    public float maxTorque = 1500f;
    [Tooltip("Maximum brake torque applied to the driving wheels")]
    public float brakeTorque = 1500000f;
    [Tooltip("Normal brake torque applied to the driving wheels")]
    public float normalBrakeTorque = 1000000f;
    [Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
    public GameObject wheelShape;

    [Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
    public float criticalSpeed = 240f;
    [Tooltip("Simulation sub-steps when the speed is above critical.")]
    public int stepsBelow = 5000;
    [Tooltip("Simulation sub-steps when the speed is below critical.")]
    public int stepsAbove = 40;

    public float vehicleCenterOfMass = -1f;

    private WheelCollider[] m_Wheels;

    private float angle;
    private float torque;
    private float angleTurn = 0;
    private float torqueDirection = 0;

    // Find all the WheelColliders down in the hierarchy.
    void Start()
    {

        Front = GameObject.Find("FrontSide");
        Back  = GameObject.Find("BackSide");
        Left  = GameObject.Find("LeftSide");
        Right = GameObject.Find("RightSide");

        rb = this.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, vehicleCenterOfMass, 0);
        //agent = GetComponentInChildren<NavMeshAgent>();
        agent = GetComponent<NavMeshAgent>();

        m_Wheels = GetComponentsInChildren<WheelCollider>();

        for (int i = 0; i < m_Wheels.Length; ++i)
        {
            var wheel = m_Wheels[i];

            // Create wheel shapes only when needed.
            if (wheelShape != null)
            {
                var ws = Instantiate(wheelShape);
                ws.transform.parent = wheel.transform;
            }
        }
    }

    // This is a really simple approach to updating wheels.
    // We simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero.
    // This helps us to figure our which wheels are front ones and which are rear.
    void Update()
    {
        //Works with the Rigid body of this GameObject
        rb = this.GetComponent<Rigidbody>();

        m_Wheels[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);

        //Sets the destination for the nav agent
        agent.SetDestination(target.position);

        float handBrake = Input.GetKey(KeyCode.Space) ? brakeTorque : 0;

        foreach (WheelCollider wheel in m_Wheels)
        {
            SteerCar(wheel);

            // Update visual wheels if any.
            if (wheelShape)
            {
                Quaternion q;
                Vector3 p;
                wheel.GetWorldPose(out p, out q);

                // Assume that the only child of the wheelcollider is the wheel shape.
                Transform shapeTransform = wheel.transform.GetChild(0);
                shapeTransform.position = p;
                shapeTransform.rotation = q;
            }
        }

    }


    void SteerCar(WheelCollider wheel)
    {
        //Steering target holds the next position from the navMesh Path
        Vector3 targetDir = agent.steeringTarget - transform.position;
        Vector3 forward = transform.forward;

        string wheelName = wheel.name;
        float offSetAngle = 7.0f;
        float backingOffSet = 45.0f;
        float offSetAcc = 0.5f;
        float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);

        float frontDis = Vector3.Distance(Front.transform.position, agent.steeringTarget);  //Mathf.Abs( Front.transform.position.z - agent.steeringTarget.z);
        float backDis = Vector3.Distance(Back.transform.position, agent.steeringTarget);   //Mathf.Abs( Back.transform.position.z - agent.steeringTarget.z);

        float acceleration = 0;// transform.position.z - agent.steeringTarget.z;

        //This means the front object is closer than the back object
        if (frontDis < backDis)
        {
            acceleration = 1.0f;
        }
        //This means the back object is closer than the front object
        else if (backDis < frontDis)
        {
            acceleration = -1.0f;
        }


        //Going Forward Steering
        if (acceleration > offSetAcc && agent.remainingDistance > agent.stoppingDistance)
        {
            

            //Determines left turn or right turn
            if (angle < -offSetAngle && (-180 + offSetAngle) < angle) //(-180 + offSetAcc > angle && angle < -offSetAngle)
            {
                if (wheelName == "a0l" || wheelName == "a0r")
                {
                    print("turn right");
                    SteerCarRight(wheel, acceleration);
                }
            }
            else if (angle > offSetAngle && (180 - offSetAngle) > angle) //(180 - offSetAcc < angle && angle > offSetAngle)
            {
                if (wheelName == "a0l" || wheelName == "a0r")
                {
                    print("turn left");
                    SteerCarLeft(wheel, acceleration);
                }
            }
            else
            {
                SteerCarNeutral(wheel);
                print("Neutral Steering Wheel");
            }
        }

        //Car gets stuck steering, going backward steering
        if (acceleration < -offSetAcc && agent.remainingDistance > agent.stoppingDistance)
        {

            //Determines left turn or right turn
            if (angle < -backingOffSet) //(-180 + offSetAcc > angle && angle < -offSetAngle)
            {
                if (wheelName == "a0l" || wheelName == "a0r")
                {
                    print("turn left");
                    SteerCarLeft(wheel, acceleration);
                    
                }
            }
            else if (angle > backingOffSet) //(180 - offSetAcc < angle && angle > offSetAngle)
            {
                if (wheelName == "a0l" || wheelName == "a0r")
                {
                    print("turn right");
                    SteerCarRight(wheel, acceleration);
                }
            }
            else
            {
                SteerCarNeutral(wheel);
                print("Neutral Steering Wheel");
            }
        }

        //Determines backward or forward
        if (acceleration > offSetAcc && agent.remainingDistance > agent.stoppingDistance)
        {
            print("Forward");
            SteerCarForward(wheel);
        }
        else if (acceleration < -offSetAcc && agent.remainingDistance > agent.stoppingDistance)
        {
            SteerCarBackward(wheel);
            print("Backward");
        }
        else
        {
            SteerCarStop(wheel);
            print("Car Stopped");
        }


        Debug.Log(
            "   Target Angle:" + angle +
            "   Target Direction:" + acceleration +
            "   Current Position:" + transform.position +
            "   Remaining Distance:" + agent.remainingDistance +
            "   Steering Target: " + agent.steeringTarget +
            " Done"
            );
    }

    //Turn Left
    void SteerCarLeft(WheelCollider wheel, float forward)
    {

        //Turns the steering wheel as much as it can until max angle
        if( 180 - Mathf.Abs(angle) <= maxAngle)
        {
            wheel.steerAngle = 180 - Mathf.Abs(angle) * -1;
        }
        else
        {
            wheel.steerAngle = maxAngle * -1;
        }

        //Determines backward or forward
        if (forward == 1.0f)
        {
            //All this is for forward position
            rb.AddRelativeForce(wheel.transform.forward * -wheel.steerAngle);
            rb.AddRelativeForce(wheel.transform.right * wheel.steerAngle);
            rb.AddTorque(transform.up * wheel.steerAngle * maxAngleTorque);

        }
        else if (forward == -1.0f)
        {
            //All this is for Backward position
            rb.AddRelativeForce(wheel.transform.forward * wheel.steerAngle);
            rb.AddRelativeForce(wheel.transform.right * -wheel.steerAngle);
            rb.AddTorque(transform.up * -wheel.steerAngle * maxAngleTorque);
        }
    }

    //Turn Right
    void SteerCarRight(WheelCollider wheel, float forward)
    {
        //Turns the steering wheel as much as it can until max angle
        if (180 - Mathf.Abs(angle) <= maxAngle)
        {
            wheel.steerAngle = 180 - Mathf.Abs(angle);
        }
        else
        {
            wheel.steerAngle = maxAngle * 1;
        }


        //Determines backward or forward
        if (forward == 1.0f)
        {
            //All this is for forward position
            rb.AddRelativeForce(wheel.transform.forward * wheel.steerAngle);
            rb.AddRelativeForce(wheel.transform.right * wheel.steerAngle);
            rb.AddTorque(transform.up * wheel.steerAngle * maxAngleTorque);
        }
        else if (forward == -1.0f)
        {
            //All this is for Backward position
            rb.AddRelativeForce(wheel.transform.forward * -wheel.steerAngle);
            rb.AddRelativeForce(wheel.transform.right * -wheel.steerAngle);
            rb.AddTorque(transform.up * -wheel.steerAngle * maxAngleTorque);
        }
    }

    //Straighten the wheel
    void SteerCarNeutral(WheelCollider wheel)
    {
        wheel.steerAngle = 0;
    }

    //Gets Car moving Forward
    void SteerCarForward(WheelCollider wheel)
    {
        wheel.motorTorque = maxTorque * 10000000;
        rb.velocity.Set(rb.velocity.x, rb.velocity.z, maxTorque);
        wheel.brakeTorque = 0;
    }

    //Gets car backward
    void SteerCarBackward(WheelCollider wheel)
    {
        wheel.motorTorque = maxTorque * -10000000;
        //.velocity.Set(rb.velocity.x, rb.velocity.z, maxTorque);
        wheel.brakeTorque = 0;
    }

    //Stop the Car Movement
    void SteerCarStop(WheelCollider wheel)
    {
        wheel.motorTorque = 0;
        rb.velocity.Set(0, 0, 0); 
        wheel.brakeTorque = brakeTorque * 100000;
    }

}
