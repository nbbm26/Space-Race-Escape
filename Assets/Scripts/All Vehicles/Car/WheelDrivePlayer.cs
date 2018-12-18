using UnityEngine;
using UnityEngine.AI;
using System;

public class WheelDrivePlayer : MonoBehaviour
{
    [Tooltip("Gets the car's actual body")]
    Rigidbody rb;

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

    // Find all the WheelColliders down in the hierarchy.
    void Start()
    {

        rb = this.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, vehicleCenterOfMass, 0);

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
        Speedometer.ShowSpeed(rb.velocity.magnitude, 0, criticalSpeed);

        m_Wheels[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);

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
        string wheelName = wheel.name;

        //Determines left turn or right turn
        if (Input.GetAxis("Horizontal") > 0.3)
        {
            if (wheelName == "a0l" || wheelName == "a0r")
            {
                print("turn right");
                SteerCarRight(wheel);
            }
        }
        else if (Input.GetAxis("Horizontal") < -0.3)
        {
            if (wheelName == "a0l" || wheelName == "a0r")
            {
                print("turn left");
                SteerCarLeft(wheel);
            }
        }
        else
        {
            SteerCarNeutral(wheel);
            print("Neutral Steering Wheel");
        }


        //Determines backward or forward
        if (Input.GetAxis("Vertical") > 0.3)
        {
            print("Forward");
            SteerCarForward(wheel);
        }
        else if (Input.GetAxis("Vertical") < -0.3)
        {
            SteerCarBackward(wheel);
            print("Backward");
        }
        else
        {
            SteerCarStop(wheel);
            print("Car Stopped");
        }
    }


    //Turn Left
    void SteerCarLeft(WheelCollider wheel)
    {
        wheel.steerAngle = (maxAngle * Input.GetAxis("Horizontal"));

        //Determines backward or forward
        if (Input.GetAxis("Vertical") > 0.3)
        {
            //All this is for forward position
            rb.AddRelativeForce(wheel.transform.forward * -wheel.steerAngle);
            rb.AddRelativeForce(wheel.transform.right * wheel.steerAngle);
            rb.AddTorque(transform.up * wheel.steerAngle * maxAngleTorque);

        }
        else if (Input.GetAxis("Vertical") < -0.3)
        {
            //All this is for Backward position
            rb.AddRelativeForce(wheel.transform.forward * wheel.steerAngle);
            rb.AddRelativeForce(wheel.transform.right * -wheel.steerAngle);
            rb.AddTorque(transform.up * -wheel.steerAngle * maxAngleTorque);
        }
    }

    //Turn Right
    void SteerCarRight(WheelCollider wheel)
    {
        wheel.steerAngle = maxAngle * Input.GetAxis("Horizontal");


        //Determines backward or forward
        if (Input.GetAxis("Vertical") > 0.3)
        {
            //All this is for forward position
            rb.AddRelativeForce(wheel.transform.forward * wheel.steerAngle);
            rb.AddRelativeForce(wheel.transform.right * wheel.steerAngle);
            rb.AddTorque(transform.up * wheel.steerAngle * maxAngleTorque);
        }
        else if (Input.GetAxis("Vertical") < -0.3)
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
        rb.velocity.Set(rb.velocity.x, rb.velocity.z, maxTorque);
        wheel.brakeTorque = 0;
    }

    //Stop the Car Movement
    void SteerCarStop(WheelCollider wheel)
    {
        wheel.motorTorque = 0;
        rb.velocity.Set(0, 0, 0);
        wheel.brakeTorque = brakeTorque * 100000000;
    }

}