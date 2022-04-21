using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager instance;

    [Header("OBJECTS")]
    [SerializeField] GameObject selectedObject;
    public GameObject node;
    public GameObject nodeS;
    public GameObject nodeText;
    [HideInInspector] public int i = 0;

    [Header("MATRIX")]
    public List<NodeBehaviour> nodes = new();
    private float[][] matris;
    public Text matrisText;

    [Header("PANELS")]
    public GameObject matrixPanel;
    public GameObject helpPanel;
    public GameObject settingsPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void UpdateMatris()
    {
        matris = new float[nodes.Count][];
        for (int i = 0; i < nodes.Count; i++)
        {
            matris[i] = new float[nodes.Count];
            for (int t = 0; t < nodes[i].neightbours.Count; t++)
            {
                //find nodes[i].neightbours[t] in nodes list
                int index = nodes.FindIndex(x => x == nodes[i].neightbours[t].node);
                matris[i][t] = nodes[i].neightbours[t].distance; ///matris[i][index] idi ben deðiþtirdim
            }
            matris[i][i] = 0;
        }

        matrisText.text = "";
        for (int i = 0; i < nodes.Count; i++)
        {
            for (int t = 0; t < nodes.Count; t++)
            {
                matrisText.text += Mathf.Round(matris[i][t]);
                if (t != nodes.Count-1)
                {
                    matrisText.text += ",";
                }
            }
            matrisText.text += "\n";
        }      
    }

    private void Update()
    {
        MouseInput();
        //if press esc, call ClassAll
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAll();
        }
    }

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);

            if (hit.collider.CompareTag("node"))
            {
                if (hit.collider != selectedObject)
                {
                    foreach (GameObject obj in GameObject.FindGameObjectsWithTag("node"))
                    {
                            obj.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
                selectedObject = hit.collider.gameObject;
                selectedObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
            else if(hit.transform != null && !hit.collider.gameObject.CompareTag("node"))
            {
                
            }
            else if (hit.transform == null)
            {
                selectedObject = null;
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("node"))
                {
                    obj.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }

    public void RemoveSelectedObject()
    {
        if (selectedObject != null)
        {
            Destroy(selectedObject);
            nodes.Remove(selectedObject.GetComponent<NodeBehaviour>());
            
            UpdateMatris();
            selectedObject = null;
        }
    }

    public void InstantiateNode()
    {
        if (nodes.Count == 0)
        {
            GameObject GO = Instantiate(nodeS, new Vector3(0, 0, 0), Quaternion.identity);
            i++;
            GO.name = i.ToString();
            //GameObject txtObj = Instantiate(nodeText, GO.transform);
            //txtObj.transform.parent = GO.transform;
            nodes.Add(GO.GetComponent<NodeBehaviour>());
            UpdateMatris();
        }
        else
        {
            GameObject GO = Instantiate(node, new Vector3(0, 0, 0), Quaternion.identity);
            i++;
            GO.name = i.ToString();
            //GameObject txtObj = Instantiate(nodeText, GO.transform);
            //txtObj.transform.parent = GO.transform;
            nodes.Add(GO.GetComponent<NodeBehaviour>());
            UpdateMatris();
        }
    }

    #region Panel Control
    public void OpenMatrixPanel()
    {
        matrixPanel.SetActive(true);
        helpPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    public void OpenHelpPanel()
    {
        helpPanel.SetActive(true);
        matrixPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
        matrixPanel.SetActive(false);
        helpPanel.SetActive(false);
    }
    public void CloseAll()
    {
        matrixPanel.SetActive(false);
        helpPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    #endregion
}
