using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    private int Score = 0;
    
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        Score += 1;
        Debug.Log($"Bumped into {Score} walls");
    }
}
