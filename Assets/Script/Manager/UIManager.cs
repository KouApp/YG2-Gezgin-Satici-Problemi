using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("PANELS")]
    public GameObject matrixPanel;
    public GameObject helpPanel;
    public GameObject settingsPanel;

    [Header("SETTINGS")]
    public bool isDistanceEnable = true;

    public List<NodeBehaviour> nodes;

    private void Start()
    {
        nodes = GeneralManager.instance.nodes;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAll();
        }
    }
    //A�a��da UI panelleri a��l�p kapat�l�yor
    public void OpenMatrixPanel()
    {
        matrixPanel.SetActive(true);
        helpPanel.SetActive(false);
    }
    public void OpenHelpPanel()
    {
        helpPanel.SetActive(true);
        matrixPanel.SetActive(false);
    }
    public void CloseAll()
    {
        matrixPanel.SetActive(false);
        helpPanel.SetActive(false);
    }

    //Nodelar aras�ndaki mesafeyi g�steren text a��l�p kapat�l�yor.
    public void DistanceTextShow()
    {
        isDistanceEnable = !isDistanceEnable;
        foreach (var obj in nodes)
        {
            foreach (var item in obj.neightbours)
            {
                item.line.child2.SetActive(isDistanceEnable);
            }
        }


    }
}
