using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordComponent : MonoBehaviour
{
    public float intervalTime = 0.1f;
    [SerializeField] List<Vector3> positions = new List<Vector3>();
    [SerializeField] List<Quaternion> rotations = new List<Quaternion>();
    [SerializeField] List<bool> isMovings = new List<bool>();
    [SerializeField] List<float> stopTimers = new List<float>();
    [SerializeField] float timer = 0f;
    [SerializeField] InputComponent input = null;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputComponent>();
        positions.Clear();
        rotations.Clear();
        isMovings.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= intervalTime)
        {
            RecordPosition();
            timer = 0f;
        }
    }

    public void RecordPosition()
    {
        
        positions.Add(transform.position);
        rotations.Add(transform.rotation);
        bool _moving = input.Move.inProgress;
        if (!_moving) {
            stopTimers.Add(input.GetLastTimeMovement());
        }
        else
        {
            stopTimers.Add(-1f);
        }
        isMovings.Add(_moving);
    }

    public List<Vector3> GetPositions()
    {
        return positions;
    }

    public List<Quaternion> GetRotations()
    {
        return rotations;
    }

    public List<bool> GetMovement()
    {
        return isMovings;
    }
    
    public List<float> GetTimeStop()
    {
        return stopTimers;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        List<Vector3> _positions = GetPositions();
        foreach (Vector3 _position in _positions) 
        {
            Gizmos.DrawWireSphere(_position,.5f);
        }
    }
}
