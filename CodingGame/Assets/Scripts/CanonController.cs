using System;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    private bool isPressing;

    [SerializeField] public GameObject cannonball;
    [SerializeField] public Transform firepoint;

    private Vector3 initialVelocity;

    private Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireCanon(float firingAngle, float firingVectorMagnitude)
    {
        GameObject cannonBall = Instantiate(cannonball, firepoint.position, Quaternion.identity);
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
        //this.transform.Rotate(new Vector3(1, 0, 0), -firingAngle);

        var verticalComponent = (float)Math.Sin(firingAngle) * firingVectorMagnitude;
        var horizontalComponent = (float)Math.Cos(firingAngle) * firingVectorMagnitude;
        
        rb.AddForce(new Vector3(horizontalComponent,verticalComponent,0), ForceMode.Impulse);
        //this.transform.Rotate(new Vector3(1, 0, 0), firingAngle);
    }
}
