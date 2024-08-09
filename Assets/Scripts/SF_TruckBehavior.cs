using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SF_TruckBehavior : MonoBehaviour
{
    [SerializeField] SF_MovementComponent movement = null;
    [SerializeField] SF_InputComponent input = null;
    [SerializeField] SF_Recorder recorder = null;
    [SerializeField] bool isAutoPilot = false;

    public SF_MovementComponent Movement => movement;
    public SF_InputComponent Input => input;
    public SF_Recorder Recorder => recorder;
    public bool IsAutoPilot => isAutoPilot;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        movement = GetComponent<SF_MovementComponent>();
        input = GetComponent<SF_InputComponent>();
        recorder = GetComponent<SF_Recorder>();

        input.Record.performed += recorder.SetRecording;
        input.Rewind.performed += SetAutoPilot;
    }

    private void SetAutoPilot(InputAction.CallbackContext _context)
    {
        if (recorder.RecordingState == RecordState.IS_RECORDING) return;
        isAutoPilot = !isAutoPilot;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
