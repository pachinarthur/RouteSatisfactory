using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : CustomComponent
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float rotateSpeed = 30;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        if (!playerRef) return;
        Vector2 _moveDir = playerRef.Input.Move.ReadValue<Vector2>();
        transform.position += transform.forward * _moveDir.y * moveSpeed * Time.deltaTime;
        transform.position += transform.right * _moveDir.x * moveSpeed * Time.deltaTime;
    }

    private void Rotate()
    {
        if (!playerRef) return;
        float _value = playerRef.Input.Rotate.ReadValue<float>();
        transform.eulerAngles += transform.up * _value * rotateSpeed * Time.deltaTime;
    }
}
