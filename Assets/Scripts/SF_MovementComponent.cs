using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SF_MovementComponent : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float rotationSpeed = 50;
    [SerializeField] SF_InputComponent inputsRef = null;
    [SerializeField] SF_TruckBehavior trucRef = null;
    // Start is called before the first frame update
    void Start()
    {
        inputsRef = GetComponent<SF_InputComponent>();
        trucRef = GetComponent<SF_TruckBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        if (!inputsRef || !trucRef || trucRef.IsAutoPilot) return;
        Vector2 _movDir = inputsRef.Move.ReadValue<Vector2>();
        transform.position += transform.right * _movDir.x * moveSpeed * Time.deltaTime;
        transform.position += transform.forward * _movDir.y * moveSpeed * Time.deltaTime;
    }

    private void Rotate()
    {
        if(!inputsRef || !trucRef || trucRef.IsAutoPilot) return;
        Vector2 _rotDir = inputsRef.Rotate.ReadValue<Vector2>();
        transform.eulerAngles += transform.up * _rotDir.x * rotationSpeed * Time.deltaTime;
        //transform.eulerAngles += transform.right * _rotDir.y * rotationSpeed * Time.deltaTime;
    }

    public void MoveTo(float _deltaTime,Vector3 _pos)
    {
        if(!trucRef || !trucRef.IsAutoPilot) return;
        transform.position = Vector3.MoveTowards(transform.position, _pos, _deltaTime * moveSpeed);
    }
    
    public void RotateTo(float _deltaTime,Quaternion _rot)
    {
        if(!trucRef || !trucRef.IsAutoPilot) return;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, _deltaTime * rotationSpeed);
    }

    public void RotateTo(float _deltaTime,Vector3 _pos)
    {
        if(!trucRef ||!trucRef.IsAutoPilot) return;
        Vector3 _lookAtDirection = _pos  - transform.position;
        if (_lookAtDirection == Vector3.zero) return;
        Quaternion _lookAt = Quaternion.LookRotation(_lookAtDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,_lookAt, _deltaTime * rotationSpeed);
    }
}

