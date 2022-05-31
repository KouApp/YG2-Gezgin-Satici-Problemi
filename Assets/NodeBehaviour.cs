using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class NodeBehaviour : MonoBehaviour
{
    private Vector2 difference = Vector2.zero;
    public  List<NeighbourNode> neightbours = new();

    private Camera mainCam;
    public TextMeshProUGUI tmpro;

    private void Awake()
    {
        mainCam = Camera.main;
        
    }
    
    private void Start()
    {
        var child = transform.GetChild(0);
        var child2 = child.transform.GetChild(0).gameObject;
        tmpro = child2.transform.GetComponent<TextMeshProUGUI>();
        tmpro.text = gameObject.name;
    }

    private void OnMouseDown()
    {
        difference = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }
    
    public void AddNeightbour(NeighbourNode node)
    {
        //node.CalculateDistance(gameObject);
        neightbours.Add(node);
        GeneralManager.instance.UpdateMatris();
    }
    public void RemoveNeightbour(NodeBehaviour node)
    {
        neightbours.Remove(neightbours.Find(x => x.node == node));
        GeneralManager.instance.UpdateMatris();
    }
}

public class NeighbourNode
{
    public NodeBehaviour node { get; private set; }
    public LineRendererCS line { get; private set; }

    public float Distance
    {
        get; private set;
    }

    public float GetDistance() => line.distance;
    public NeighbourNode(NodeBehaviour node, LineRendererCS line, float distance)
    {
        this.node = node;
        this.line = line;
        this.Distance = distance;
    }
    public void CalculateDistance(GameObject nodeGameObject)
    {
        Distance = Vector2.Distance(nodeGameObject.transform.position, node.transform.position);
    }

    public void SetDistance(float distance)
    {
        this.Distance = distance;
    }
}
