using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayComponent : CustomComponent
{
    [SerializeField] float playbackSpeed = 10f;
    [SerializeField] List<Vector3> positions;
    [SerializeField] List<Quaternion> rotations ;
    [SerializeField] List<bool> isMovings;
    [SerializeField] List<float> stopTimeMaxs;
    [SerializeField] int currentIndex = 0;
    [SerializeField] float minDist = 0.5f;
    [SerializeField] float stopTime = 0f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        rideRoad();
    }

    public void SetRoute(List<Vector3> routePositions, List<Quaternion> routeRotations, List<bool> routeisMovings, List<float> TimeMaxs)
    {
        positions = routePositions;
        rotations = routeRotations;
        isMovings = routeisMovings;
        stopTimeMaxs = TimeMaxs;
    }

    public void rideRoad()
    {
        if (isMovings[currentIndex])
        {
            transform.position = Vector3.MoveTowards(transform.position, positions[currentIndex], playbackSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotations[currentIndex], playbackSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, positions[currentIndex]) <= minDist){
                currentIndex++;
            }
        }
        else
        {
            stopTime += Time.deltaTime;
            if(stopTime > stopTimeMaxs[currentIndex])
            {
                stopTime = 0f;
                currentIndex++;
            }
        }
    }

}
