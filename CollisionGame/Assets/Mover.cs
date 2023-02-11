using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float xSpeed = 0.1f;
    [SerializeField]
    private float ySpeed = 0.0f;
    [SerializeField]
    private float zSpeed = 0.1f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(1,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        var xInput = Input.GetAxis("Horizontal") * xSpeed * Time.deltaTime;
        var zInput = Input.GetAxis("Vertical") * zSpeed * Time.deltaTime;

        transform.Translate(xInput, 0.0f, zInput);
    }
}
