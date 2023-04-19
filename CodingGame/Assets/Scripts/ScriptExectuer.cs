using SandScript.Interpreter;
using SandScript.Interpreter.Interop;
using SandScript.Interpreter.Modules;
using UnityEngine;

public class ScriptExectuer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canon;
    [SerializeField] private GameObject boxTarget;

    private CanonController canonController;
    
    // Start is called before the first frame update
    void Start()
    {
        canonController = canon.GetComponent<CanonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 1 && Input.GetKeyDown(KeyCode.X))
        {
            ExecuteScript();
        }
    }

    private void ExecuteScript()
    {
        Debug.Log("Executing script");
        
        var script = System.IO.File.ReadAllText("./Assets/Scripts/script.ss");

        // Initialize a SandScript engine
        SandScriptEngine engine = new SandScriptEngine();
        engine.LoadModule(ModulesExtensions.MathModule());

        engine.SetReference("canonController", new ClrObject(canonController));
        engine.SetReference("canonPosition", new ClrObject(canonController.GetComponent<Transform>().position));
        engine.SetReference("targetPosition", new ClrObject(boxTarget.GetComponent<Transform>().position));
        
        // Set environment specific objects
        var completion = engine.Execute(script);
        Debug.Log(completion.Value.ToString());
    }
}
