using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Mover : MonoBehaviour
{
    private Transform transform;
    [SerializeField]
    private Transform computerTransform;

    [SerializeField] private GameObject computerScreen;
    [SerializeField] private GameObject computer;
    private Computer _computer;
    
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        computerScreen.SetActive(false);
        _computer = computer.GetComponent<Computer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, computerTransform.position) < 1 && Input.GetKeyDown(KeyCode.I) && !computerScreen.activeSelf)
        {
            if (computerScreen != null)
            {
                _computer.StartComputer();
                computerScreen.SetActive(true);
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (computerScreen != null)
            {
                _computer.CloseComputer();
                computerScreen.SetActive(false);
            }

            return;
        }
        
        if (computerScreen.activeSelf)
            return;
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -100f * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, 100f * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime);
        }
    }
}
