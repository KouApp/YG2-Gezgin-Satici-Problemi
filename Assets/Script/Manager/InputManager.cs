using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //set ins
    public static InputManager instance;
    
    [HideInInspector]public GameObject currentLeftSelected, currentRightSelected, tempRightSelected;
    [SerializeField] private GameObject tempNode, lineRender;
    
    private LineRendererCS tempLine;

    private Camera mainCamera;
    Vector2 difference;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        mainCamera = Camera.main;
    }

    private void Update()
    {
        MouseDown();

        MouseUpdate();

        MouseUp();
    }

    private void MouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 raycastPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.transform.CompareTag("node"))
                {
                    currentLeftSelected = hit.transform.gameObject;
                    //difference = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 raycastPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.transform.CompareTag("node"))
                {
                    currentRightSelected = hit.transform.gameObject;
                    tempRightSelected = Instantiate(tempNode, currentRightSelected.transform.position, Quaternion.identity);
                    tempLine = Instantiate(lineRender, currentRightSelected.transform.position, Quaternion.identity).GetComponent<LineRendererCS>();
                    tempLine.SetLines(currentRightSelected, tempRightSelected);
                    //difference = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
                }
            }
        }
    }

    private void MouseUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if(currentLeftSelected != null)
            {
                Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition) - difference;
                currentLeftSelected.transform.position = mousePos;
            }
        }
        if (Input.GetMouseButton(1))
        {
            if(tempRightSelected != null)
            {
                Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition) - difference;
                tempRightSelected.transform.position = mousePos;
            }
        }
    }

    private void MouseUp()
    {
        if(Input.GetMouseButtonUp(0))
        {
            currentLeftSelected = null;
        }
        if(Input.GetMouseButtonUp(1))
        {
            if (currentRightSelected != null)
            {
                GameObject temp = tempRightSelected;
                tempRightSelected = tempRightSelected.GetComponent<TempNode>().node;
                
                if (tempRightSelected)
                {
                    if(currentRightSelected.name == tempRightSelected.name
                       || (currentRightSelected.GetComponent<NodeBehaviour>().neightbours.FindIndex(x => x.node.name == tempRightSelected.name) >=0)
                       || (tempRightSelected.GetComponent<NodeBehaviour>().neightbours.FindIndex(x => x.node.name == currentRightSelected.name) >=0))
                    {
                        Destroy(tempLine.gameObject);
                        Destroy(temp.gameObject);
                    }
                    currentRightSelected.GetComponent<NodeBehaviour>().AddNeightbour(new NeighbourNode(tempRightSelected.GetComponent<NodeBehaviour>(), tempLine, 0));
                    tempRightSelected.GetComponent<NodeBehaviour>().AddNeightbour(new NeighbourNode(currentRightSelected.GetComponent<NodeBehaviour>(), tempLine, 0));
                    tempLine.SetLines(currentRightSelected, tempRightSelected);
                    
                }
                else
                {
                    Destroy(tempLine.gameObject);
                    Destroy(temp.gameObject);
                }
                currentRightSelected = null;
                tempRightSelected = null;
                Destroy(temp);
            }
            
        }
    }
}
