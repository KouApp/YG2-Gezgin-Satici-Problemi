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
    public GameObject square;
    [HideInInspector] public int i = 0;

    [Header("MATRIX")]
    public List<NodeBehaviour> nodes = new();
    private float[][] matris;
    public Text matrisText;

    [Header("PANELS")]
    public GameObject matrixPanel;
    public GameObject helpPanel;
    public GameObject settingsPanel;

    [Header("CAMERA")]
    public Camera mainCamera;
    public float camMoveSpeed = 5f;

    [Header("SETTINGS")]
    public bool isDistanceText = true;


    private void Awake()
    {
        mainCamera = Camera.main;
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
        CameraZoom();
        CameraMovement();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAll();
        }
    }

    void CameraZoom()
    {
        //make zoom in and out with mouse scroll
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            mainCamera.orthographicSize -= 0.2f;
            if (mainCamera.orthographicSize <= 0.4f)
            {
                mainCamera.orthographicSize = 0.4f;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            mainCamera.orthographicSize += 0.2f;
            if (mainCamera.orthographicSize >= 9f)
            {
                mainCamera.orthographicSize = 9f;
            }            
        }
    }

    void CameraMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            mainCamera.transform.position += new Vector3(camMoveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mainCamera.transform.position -= new Vector3(camMoveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            mainCamera.transform.position -= new Vector3(0, camMoveSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            mainCamera.transform.position += new Vector3(0, camMoveSpeed * Time.deltaTime, 0);
        }
    }

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);
            Debug.Log(hit.collider.name);

            if (hit.collider != null && hit.collider.CompareTag("node"))
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
            else if (!hit.collider.CompareTag("node"))
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
        square = GameObject.Find("Square");
        if (square == null)
        {
            GameObject GO = Instantiate(nodeS, new Vector3(0, 0, 0), Quaternion.identity);
            i++;
            GO.name = i.ToString();
            nodes.Add(GO.GetComponent<NodeBehaviour>());
            UpdateMatris();
        }
        else
        {
            GameObject GO = Instantiate(node, new Vector3(0, 0, 0), Quaternion.identity);
            i++;
            GO.name = i.ToString();
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
    #region Settings Control
    public void DistanceTextShow()
    {
        if (isDistanceText)
            isDistanceText = false;
        else
            isDistanceText = true;
    }
    #endregion
}
