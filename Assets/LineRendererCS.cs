using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LineRendererCS : MonoBehaviour
{
    public GameObject lrStart;
    public GameObject lrEnd;
    [SerializeField]LineRenderer lr;

    [Header("INPUT FIELD")]
    InputField inputField;
    GameObject child2;

    void Awake()
    {
        this.name = "line" + this.GetInstanceID();
        if (lr == null)
        {
            lr = GetComponent<LineRenderer>();
        }
    }
    private void Start()
    {
        var child = transform.GetChild(0);
        child2 = child.transform.GetChild(0).gameObject;
        inputField = child2.GetComponent<InputField>();
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
        if (lrStart != null && lrEnd != null)
        {
            SetLine(lrStart, lrEnd);
            child2.transform.position = (lrStart.transform.position + lrEnd.transform.position) / 2;
        }
        else
        {
            Destroy(this.gameObject);
            Destroy(inputField);
        }
        
        //if (gm.isDistanceText)
        //    child2.SetActive(true);
        //else
        //    child2.SetActive(false);
    }
}
