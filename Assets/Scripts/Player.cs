using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Profiling;
using UnityEngine.UIElements;

[RequireComponent(typeof(MovementComponent), typeof(InputComponent), typeof(ReplayComponent))]
[RequireComponent (typeof(RecordComponent))]

public class Player : MonoBehaviour
{
    [SerializeField] MovementComponent movement = null;
    [SerializeField] InputComponent input = null;
    [SerializeField] RecordComponent record = null;
    [SerializeField] ReplayComponent replay = null;

    public MovementComponent Movement => movement;
    public InputComponent Input => input;
    public RecordComponent Record => record;
    public ReplayComponent Replay => replay;
    // Start is called before the first frame update
    void Start()
    {
        Init();

    }
    void Init()
    {
        movement = GetComponent<MovementComponent>();
        input = GetComponent<InputComponent>();
        record = GetComponent<RecordComponent>();
        replay = GetComponent<ReplayComponent>();

        input.Record.performed += StartRecording;
        input.StopRecord.performed += StartPlayback;
        record.enabled = false;
        replay.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    void StartRecording(InputAction.CallbackContext context)
    {
        record.enabled = true;
        replay.enabled = false;
    }

    void StartPlayback(InputAction.CallbackContext context)
    {
        replay.SetRoute(record.GetPositions(), record.GetRotations(), record.GetMovement(),record.GetTimeStop());
        record.enabled = false;
        replay.enabled = true;
    }

    private void OnDrawGizmos()
    {
     
    }
}
