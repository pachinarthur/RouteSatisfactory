using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : CustomComponent
{
    [SerializeField] InputsPlayer controls = null;

    [SerializeField] InputAction move = null;
    [SerializeField] InputAction rotate = null;
    [SerializeField] InputAction record = null;
    [SerializeField] InputAction stopRecord = null;
    [SerializeField] float lastInputTime;
    [SerializeField] bool inputReceived = false;

    public InputAction Move => move;
    public InputAction Rotate => rotate;
    public InputAction Record => record;
    public InputAction StopRecord => stopRecord;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    private void Awake()
    {
        controls = new InputsPlayer();
    }

    private void OnEnable()
    {
        move = controls.Player.Movement;
        rotate = controls.Player.Rotate;
        record = controls.Player.Record;
        stopRecord = controls.Player.StopRecord;

        move.Enable();
        rotate.Enable();
        record.Enable();
        stopRecord.Enable();

        move.performed += OnInputTouch;
    }

    private void OnDisable()
    {
        move.Disable();
        rotate.Disable();
        record.Disable();
        stopRecord.Disable();

        move.performed -= OnInputTouch;
    }

    public void OnInputTouch(InputAction.CallbackContext context)
    {
        lastInputTime = Time.time;
        inputReceived = true;
    }

    public float GetLastTimeMovement()
    {
        if (inputReceived)
        {
        return -1f;
        }
            lastInputTime = 0f;
            return Time.time - lastInputTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
