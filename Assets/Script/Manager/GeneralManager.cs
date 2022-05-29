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

    private void Awake()
    {
        //Singleton method
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

    private void Update()
    {
        MouseInput();
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
                matris[i][t] = nodes[i].neightbours[t].distance; ///matris[i][index] iken matris[i][t] olarak deðiþtirdim
            }
            matris[i][i] = 0;
        }

        //matrisler düzgün yazýlmýyor, hata üstteki kodda mý alttaki kodda mý bilmiyorum
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

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //sol mouse ile týklayýnca bütün nodelar beyaz oluyor. Sonra, raycasthite denk gelen node cyan oluyor.
            Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);

            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("node"))
            {
                obj.GetComponent<SpriteRenderer>().color = Color.white;
            }

            if (hit)
            {
                selectedObject = hit.collider.gameObject;
                selectedObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
        }
    }

    public void RemoveSelectedObject()
    {
        if (selectedObject != null)
        {
            NodeBehaviour node = selectedObject.GetComponent<NodeBehaviour>();
            nodes.Remove(node);
            foreach (var item in node.neightbours)
            {
                item.node.RemoveNeightbour(node);
            }
            UpdateMatris();
            Destroy(selectedObject);
        }
    }

    public void InstantiateNode()
    {
        //Sahnede square yoksa instantiate edilen node square oluyor. Her instantiate edilen node ile i artýyor ve i deðiþkeni nodeun üzerine yazdýrýlýyor.
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

    
}
