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
    public TextMeshPro tmpro;
    GeneralManager gm;

    private void Awake()
    {
        mainCam = Camera.main;
        
    }
    
    private void Start()
    {
        gm = GetComponent<GeneralManager>();
        Transform child = transform.GetChild(0);
        tmpro = child.transform.GetChild(0).GetComponent<TextMeshPro>();              
        //aha bu anasýný siktimin koduyla tam 3 saat uðraþtým.
        Debug.Log(tmpro.name);
    }

    private void OnMouseDown()
    {
        difference = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }
    
    public void AddNeightbour(NeighbourNode node)
    {
        node.CalculateDistance(gameObject);
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
