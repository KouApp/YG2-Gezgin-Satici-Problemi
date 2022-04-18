using UnityEngine;
using UnityEngine.UI;

public class GeneralManager : MonoBehaviour
{
    [SerializeField] GameObject selectedObject;
    public GameObject node;
    int i = 0;

    LineRendererCS[] lines;
    GameObject[] nodes;

    private void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("node");
    }
    //call a function that will call the SetLine function in the LineRendererCS with the parameter of two nodes
    public void SetLine()
    {
        for (int i = 0; i < nodes.Length-1; i++)
        {
            lines[i].SetLine(nodes[i], nodes[++i]);
        }    
    }




    private void Update()
    {
        MouseInput();
        SetLine();
    }
    
    void MouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);

            if (hit.collider != null)
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
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }

    public void InstantiateNode()
    {
        GameObject GO = Instantiate(node, new Vector3(0,0,0), Quaternion.identity);
        i++;
        GO.name = "Node" + i;
    }
}
