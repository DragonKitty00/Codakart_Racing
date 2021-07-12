using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Axle class that defines what goes into our  axles
[System.Serializable]
public class Axle
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class Controller : MonoBehaviour
{
    public List<Axle> axles;
    public float maxMoterTorque;
    public float maxSteeringAngle;

    public void FixedUpdate()
    {
        float motor = maxMoterTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (Axle axle in axles)
        {
            if (axle.motor)
            {
                axle.leftWheel.motorTorque = motor;
                axle.rightWheel.motorTorque = motor;
            }
            if (axle.steering)
            {
                axle.leftWheel.steerAngle = steering;
                axle.rightWheel.steerAngle = steering;
            }
        }
    }
}
