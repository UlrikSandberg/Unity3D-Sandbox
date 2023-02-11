using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rocketBody;
    
    // Gravity is 9.8 m/s^2 factoring in delta T and this should give us enough force to counteract gravity.
    [SerializeField] private float rocketMainThrust = 9.8f;
    [SerializeField] private float rocketLeftRotationalVelocity = 1.0f;
    [SerializeField] private float rocketRightRotationalVelocity = 1.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Thrusting!!!!");

            var rocketThrust = rocketMainThrust * Time.deltaTime;
            Debug.Log(rocketThrust);
            rocketBody.AddRelativeForce(new Vector3(0, 1 * rocketThrust, 0));
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            var leftRotationalSpeed = rocketLeftRotationalVelocity * Time.deltaTime;
            var leftTorque = new Vector3(0, 0, -1 * leftRotationalSpeed);
            rocketBody.AddRelativeTorque(leftTorque); 
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            var rightRotationalSpeed = rocketRightRotationalVelocity * Time.deltaTime;
            var rightTorque = new Vector3(0, 0, 1 * rightRotationalSpeed);
            
            rocketBody.AddRelativeTorque(rightTorque);
        }
    }
}
