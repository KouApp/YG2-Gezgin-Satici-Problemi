using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera mainCamera;
    public float camMoveSpeed = 5f;

    private void Start()
    {
        mainCamera = Camera.main;
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

    void Update()
    {
        CameraZoom();
        CameraMovement();
    }
}
