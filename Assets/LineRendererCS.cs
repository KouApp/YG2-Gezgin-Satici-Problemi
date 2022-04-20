using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererCS : MonoBehaviour
{
    public GameObject lrStart;
    public GameObject lrEnd;
    [SerializeField]LineRenderer lr;
    void Awake()
    {
        if (lr == null)
        {
            lr = GetComponent<LineRenderer>();
        }
    }
    public void SetLines(GameObject lrStart, GameObject lrEnd)
    {
        this.lrStart = lrStart;
        this.lrEnd = lrEnd;
    }
    private void SetLine(GameObject lrStart, GameObject lrEnd)
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
