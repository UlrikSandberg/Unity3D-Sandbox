using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocks : MonoBehaviour
{
    [SerializeField] public int ImpendingDoom = 1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > ImpendingDoom)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
