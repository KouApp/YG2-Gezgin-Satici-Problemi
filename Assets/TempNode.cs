using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempNode : MonoBehaviour
{
    [HideInInspector]public GameObject node;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Node entered");
        if (other.CompareTag("node"))
        {
            node = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Node exited");
        if (other.CompareTag("node"))
        {
            node = null;
        }
    }
}
