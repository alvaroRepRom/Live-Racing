using UnityEngine;

public class RaceCar : MonoBehaviour
{
    [SerializeField] private float acceleration = 1.2f;
    [SerializeField] private float turnAcceleration = 10;

    private Rigidbody rb;
    private Transform centerMass;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        centerMass = transform.Find( "Center of Mass" );
        rb.centerOfMass = centerMass.localPosition;
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        rb.AddRelativeForce( vertical * acceleration * Vector3.forward , ForceMode.Force );
        
        rb.AddRelativeTorque( horizontal * turnAcceleration * Vector3.up , ForceMode.Force );

    }
}
