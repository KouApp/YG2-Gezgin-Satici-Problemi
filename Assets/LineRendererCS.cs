using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererCS : MonoBehaviour
{
    public GameObject lrStart;
    public GameObject lrEnd;
    LineRenderer lr;
    void Start()
    {
        if (lr == null)
        {
            lr = GetComponent<LineRenderer>();
        }
    }
    
    public void SetLine(GameObject lrStart, GameObject lrEnd)
    {
        if (lrStart != null && lrEnd != null)
        {
            lr.SetPosition(0, lrStart.transform.position);
            lr.SetPosition(1, lrEnd.transform.position);
        }
    }

    void Update()
    {
        SetLine(lrStart, lrEnd);
    }
}
