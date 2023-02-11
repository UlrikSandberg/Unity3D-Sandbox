using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerScript : MonoBehaviour
{
    [SerializeField] private float xAngle = 0.0f;
    [SerializeField] private float yAngle = 0.0f;
    [SerializeField] private float zAngle = 0.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xAngle, yAngle, zAngle);
    }
}
