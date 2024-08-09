using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[SerializeField]
public enum RecordState
{
    NONE,
    IS_RECORDING,
    HAS_RECORDED
}
[SerializeField]
public struct Point
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public float PointTime { get; set; }
    public Point(Vector3 _position, Quaternion _rotation, float _pointTime)
    {
        Position = _position;
        Rotation = _rotation;
        PointTime = _pointTime;
    }
}

public class SF_Recorder : MonoBehaviour
{
    [SerializeField] SF_MovementComponent movementRef = null;
    [SerializeField] SF_TruckBehavior trucRef = null;
    [SerializeField] List<Point> allPoints = new List<Point>();
    [SerializeField] RecordState recordState = RecordState.NONE;

    [SerializeField] float time = 0;
    [SerializeField] float timeRewind = 0;
    [SerializeField] int index = 0;
    [SerializeField] float minTimeAllowed = 1;
    [SerializeField] float minDistAllowed = 0.1f;
    [SerializeField] float distanceBetweenPoints = 1f;

    public RecordState RecordingState => recordState;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Record();
        Rewind();
    }

    void Init()
    {
        movementRef = GetComponent<SF_MovementComponent>();
        trucRef = GetComponent<SF_TruckBehavior>();
    }

    void Record()
    {
        if(recordState != RecordState.IS_RECORDING)
        {
            if(recordState != RecordState.HAS_RECORDED && allPoints.Count > 0)
            {
                recordState = RecordState.HAS_RECORDED;
                allPoints.Add(new Point(transform.position, transform.rotation,time));
                time = 0;
            }
            return;
        }

        time = IncreaseTime(time);
        if(allPoints.Count < 1 || Vector3.Distance(transform.position, allPoints[allPoints.Count -1].Position) > distanceBetweenPoints)
        {
            allPoints.Add(new Point(transform.position, transform.rotation, time));
            //Point _point = new Point(transform.position, transform.rotation, time); //Si besoin de reutiliser la var
            //allPoints.Add(_point);
            time = 0;
        } 
    }

    private void Rewind()
    {
        if (allPoints.Count < 1) return;
        if(!trucRef || !movementRef || !trucRef.IsAutoPilot) return;
        timeRewind = IncreaseTime(timeRewind);
        if(timeRewind >= allPoints[index].PointTime || allPoints[index].PointTime < minTimeAllowed)
        {
            movementRef.MoveTo(Time.deltaTime, allPoints[index].Position);
            movementRef.RotateTo(Time.deltaTime, allPoints[index].Rotation);
            if (Vector3.Distance(transform.position, allPoints[index].Position) < minDistAllowed)
            {
                index++;
                timeRewind = 0;
                if(index > allPoints.Count - 1)
                {
                    index = 0;

                }
            }
        }
    }

    float IncreaseTime(float _time)
    {
        return _time += Time.deltaTime;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(recordState == RecordState.IS_RECORDING)
        {
            Gizmos.DrawSphere(transform.position, 1);
        }
        Gizmos.color = Color.white;
        int _size = allPoints.Count;
        for(int i = 0; i < _size; i++)
        {
            Gizmos.DrawSphere(allPoints[i].Position, 0.1f);
            if(i+1 < allPoints.Count)
            {
                Gizmos.DrawLine(allPoints[i].Position, allPoints[i+1].Position);
            }
        }
    }

    public void SetRecording(InputAction.CallbackContext context)
    {
        if (!trucRef) return;
        if(recordState == RecordState.HAS_RECORDED)
        {
            ResetRecordedPoint();
        }
        recordState = recordState != RecordState.IS_RECORDING ? RecordState.IS_RECORDING : RecordState.NONE;
    }

    void ResetRecordedPoint()
    {
        allPoints.Clear();
        index = 0;
    }
}
