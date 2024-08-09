using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class SF_InputComponent : MonoBehaviour
{
    [SerializeField] SF_Inputs controls = null;
    [SerializeField] InputAction move = null;
    [SerializeField] InputAction rotate = null;
    [SerializeField] InputAction record = null;
    [SerializeField] InputAction rewind = null;
    [SerializeField] const string  PATH = "Test/Save file Path";

    [SerializeField] List<InputAction> allActions = new(); //similaire a new List<InputAction>

    public InputAction Move => move;
    public InputAction Rotate => rotate;
    public InputAction Record => record;
    public InputAction Rewind => rewind;

    private void Awake()
    {
        controls = new SF_Inputs();
        if (!File.Exists(PATH)) return; //reprend les inputs du fichier qu'on a save plus bas
        string _controlsJson = File.ReadAllText(PATH);
        controls.LoadBindingOverridesFromJson(_controlsJson);
    }

    private void OnEnable()
    {
        allActions.Clear();
        move = controls.Truck.Move;
        allActions.Add(move);

        rotate = controls.Truck.Rotate;
        allActions.Add(rotate);

        record = controls.Truck.Record;
        allActions.Add(record);

        rewind = controls.Truck.Rewind;
        allActions.Add(rewind);

        ManageInputActivation(true);

        rewind.Disable(); //Disable de disable car on ne meut pas unbind un input en utilisatio,
        InputActionRebindingExtensions.RebindingOperation _rebinOps = rewind.PerformInteractiveRebinding();//Recupere l'objectqui s'occupe du rebind
        _rebinOps.WithControlsExcluding("Mouse");//Je peux rebind en excluant certain controle
        string _inputs = "";
        _rebinOps.OnComplete((callback) => //Oncomplete accepte le callback
        //Detruire le callback sinono on risque la fuite memoire.Réactiver l'inputAction 
          {
              callback.Dispose();
              rewind.Enable();
              _inputs = controls.SaveBindingOverridesAsJson();
              //File.WriteAllText(PATH, _inputs);
              string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
              Directory.CreateDirectory(Path.Combine(_path, "Test"));
              _path = Path.Combine(_path, PATH);
              File.WriteAllText(_path, _inputs);

          });
        Debug.Log("Press any key as new bindings");
        _rebinOps.Start(); //Active l'ecoute du prochain input
    }

    private void OnDisable()
    {
        ManageInputActivation(false);
    }
   
    void ManageInputActivation(bool _value)
    {
        foreach(InputAction input in  allActions)
        {
            if(_value)
            {
                input.Enable();
            }
            else
            {
                input.Disable();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
