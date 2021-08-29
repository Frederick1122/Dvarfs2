using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiftCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Camera turrelCamera;
    public void shiftCameras()
    {
        turrelCamera.gameObject.SetActive(true);

        turrelCamera.enabled = true;
        mainCamera.enabled = false;

        mainCamera.gameObject.SetActive(false);

    }
}
