using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeBehaviour : MonoBehaviour
{
    private Vector2 difference = Vector2.zero;
    private  List<NeighbourNode> neightbours = new();

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void OnMouseDown()
    {
        difference = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - difference;
    }
    
    
    
    public void AddNeightbour(NeighbourNode node)
    {
        node.CalculateDistance(gameObject);
        neightbours.Add(node);
    }
    public void RemoveNeightbour(NodeBehaviour node)
    {
        neightbours.Remove(neightbours.Find(x => x.node == node));
    }
}

public class NeighbourNode
{
    public NodeBehaviour node { get; private set; }
    public LineRendererCS line { get; private set; }
    public float distance { get; private set; }

    public NeighbourNode(NodeBehaviour node, LineRendererCS line, float distance)
    {
        this.node = node;
        this.line = line;
        this.distance = distance;
    }
    public void CalculateDistance(GameObject nodeGameObject)
    {
        distance = Vector2.Distance(nodeGameObject.transform.position, node.transform.position);
    }

    public void SetDistance(float distance)
    {
        this.distance = distance;
    }
}
