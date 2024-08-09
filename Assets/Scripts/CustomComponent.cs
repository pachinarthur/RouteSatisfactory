using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomComponent : MonoBehaviour
{
    [SerializeField] protected Player playerRef = null;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        playerRef = GetComponent<Player>();
    }
}
