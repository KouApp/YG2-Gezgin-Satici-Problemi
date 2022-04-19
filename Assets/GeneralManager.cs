using UnityEngine;
using UnityEngine.UI;

public class GeneralManager : MonoBehaviour
{
    [SerializeField] GameObject selectedObject;
    public GameObject node;
    public GameObject cursorObject;
    int i = 0;

    private void Update()
    {
        MouseInput();
        //cursorObject position equals to cursor position
        cursorObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            }
            else
            {
                selectedObject = null;
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("node"))
                {
                    obj.GetComponent<SpriteRenderer>().color = Color.white;
                }
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
