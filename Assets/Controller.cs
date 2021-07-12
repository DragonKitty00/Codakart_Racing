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

//Controller class that moves the kart wheels
public class Controller : MonoBehaviour
{
    //list of the axles and motor/steering for the kart wheels
    public List<Axle> axles;
    public float maxMoterTorque;
    public float maxSteeringAngle;
    
    //fixed update that runs the pysics and input
    public void FixedUpdate()
    {
        //get the motor and steering input WASD
        float motor = maxMoterTorque * Input.GetAxis("Vertical"); //W-Forwards, S-Backwards
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");//A-Left, D-right

        //foreach loop that runs through all the axles ing the list
        foreach (Axle axle in axles)
        {
            //if motor is valid, move the wheels
            if (axle.motor)
            {
                //if steering is valid, turn the wheels
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
