using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rocketBody;
    private AudioSource thrustSound;
    
    // Gravity is 9.8 m/s^2 factoring in delta T and this should give us enough force to counteract gravity.
    [SerializeField] private float rocketMainThrust = 9.8f;
    [SerializeField] private float rocketLeftRotationalVelocity = 1.0f;
    [SerializeField] private float rocketRightRotationalVelocity = 1.0f;
    [SerializeField] private AudioClip thrustAudio;


    [SerializeField] private ParticleSystem leftSideThruster;
    [SerializeField] private ParticleSystem rightSideThruster;
    [SerializeField] private ParticleSystem leftThruster;
    [SerializeField] private ParticleSystem rightThruster;
    
    // Start is called before the first frame update
    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();
        thrustSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void StopThruster()
    {
        leftThruster.Stop();
        rightThruster.Stop();
    }
    
    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            leftThruster.Play();
            rightThruster.Play();
            
            var rocketThrust = rocketMainThrust * Time.deltaTime;
            rocketBody.AddRelativeForce(new Vector3(0, 1 * rocketThrust, 0));
            if (!thrustSound.isPlaying)
                thrustSound.PlayOneShot(thrustAudio);
        }
        else
        {
            Invoke("StopThruster", 0.5f);
            if(thrustSound.isPlaying)
                thrustSound.Stop();
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rightSideThruster.Play();
            var leftRotationalSpeed = rocketLeftRotationalVelocity * Time.deltaTime;
            var leftTorque = new Vector3(0, 0, -1 * leftRotationalSpeed);
            rocketBody.AddRelativeTorque(leftTorque); 
        }
        else
        {
            rightSideThruster.Stop();
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            leftSideThruster.Play();
            var rightRotationalSpeed = rocketRightRotationalVelocity * Time.deltaTime;
            var rightTorque = new Vector3(0, 0, 1 * rightRotationalSpeed);
            
            rocketBody.AddRelativeTorque(rightTorque);
        }
        else
        {
            leftSideThruster.Stop();
        }
    }
}
