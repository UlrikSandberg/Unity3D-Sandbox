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
    
    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!leftThruster.isPlaying)
            {
                leftThruster.Play();
                rightThruster.Play();   
            }
            
            var rocketThrust = rocketMainThrust * Time.deltaTime;
            rocketBody.AddRelativeForce(new Vector3(0, 1 * rocketThrust, 0));
            if (!thrustSound.isPlaying)
                thrustSound.PlayOneShot(thrustAudio);
        }
        else
        {
            if (leftThruster.isPlaying)
            {
                leftThruster.Stop();
                rightThruster.Stop(); 
            }
            
            if(thrustSound.isPlaying)
                thrustSound.Stop();
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if(!rightSideThruster.isPlaying)
                rightSideThruster.Play();
            
            
            var leftRotationalSpeed = rocketLeftRotationalVelocity * Time.deltaTime;
            var leftTorque = new Vector3(0, 0, -1 * leftRotationalSpeed);
            rocketBody.AddRelativeTorque(leftTorque); 
        }
        else
        {
            if (rightSideThruster.isPlaying)
                rightSideThruster.Stop();
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            if(!leftSideThruster.isPlaying)
                leftSideThruster.Play();
            var rightRotationalSpeed = rocketRightRotationalVelocity * Time.deltaTime;
            var rightTorque = new Vector3(0, 0, 1 * rightRotationalSpeed);
            
            rocketBody.AddRelativeTorque(rightTorque);
        }
        else
        {
            if (leftSideThruster.isPlaying)
                leftSideThruster.Stop();
        }
    }
}
