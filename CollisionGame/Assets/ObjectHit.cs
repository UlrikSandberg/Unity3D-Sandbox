using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
