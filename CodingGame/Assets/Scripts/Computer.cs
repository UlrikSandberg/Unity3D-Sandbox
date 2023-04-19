using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Computer : MonoBehaviour
{

    [SerializeField] private TMP_InputField _inputField;
    
    
    // Start is called before the first frame update
    public void StartComputer()
    {
        var text = System.IO.File.ReadAllText("./Assets/Scripts/script.ss");
        _inputField.text = text;
        
        Debug.Log("Starting computer");
    }

    public void CloseComputer()
    {
        System.IO.File.WriteAllText("./Assets/Scripts/script.ss", _inputField.text);
        
        Debug.Log("Closing computer");
    }
}
