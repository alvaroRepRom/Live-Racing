using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsController : MonoBehaviour
{
    [SerializeField] private WheelCollider frontRightCol;
    [SerializeField] private WheelCollider frontLeftCol;
    [SerializeField] private WheelCollider rearRightCol;
    [SerializeField] private WheelCollider rearLeftCol; 
    
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform rearRightTransform;
    [SerializeField] private Transform rearLeftTransform;

    [Header("Car Settings")]
    public float acceleration = 500;
    public float breakingForce = 300;
    public float maxTurnAngle = 15;
    public float topSpeed = 20;

    [Header("Custom Settings")]
    [Range(0, 1)]
    public float frontBrakeFactor = 0.65f;

    private float currentAcceleration;
    private float currentBreakingForce;
    private float currentTurnAngle;


    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if ( vertical < 0)
        {
            currentBreakingForce = breakingForce;
        }
        else
        {
            currentAcceleration = acceleration * vertical;
            currentBreakingForce = 0;
        }

        rearRightCol.motorTorque = currentAcceleration;
        rearLeftCol.motorTorque  = currentAcceleration;
        
        frontRightCol.brakeTorque = currentBreakingForce * frontBrakeFactor;
        frontLeftCol.brakeTorque  = currentBreakingForce * frontBrakeFactor;
        rearRightCol.brakeTorque  = currentBreakingForce * ( 1 - frontBrakeFactor );
        rearLeftCol.brakeTorque   = currentBreakingForce * ( 1 - frontBrakeFactor );


        currentTurnAngle = horizontal * maxTurnAngle;

        frontLeftCol.steerAngle  = currentTurnAngle;
        frontRightCol.steerAngle = currentTurnAngle;

        //UpdateWheels( frontLeftCol , frontLeftTransform );
        //UpdateWheels( frontRightCol , frontRightTransform );
        //UpdateWheels( rearLeftCol , rearLeftTransform );
        //UpdateWheels( rearRightCol , rearRightTransform );
    }

    private void UpdateWheels( WheelCollider col , Transform trans )
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose( out position, out rotation );

        trans.position = position;
        trans.rotation = rotation;
    }

}
