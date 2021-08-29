using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimScript : MonoBehaviour
{
    //public float speed;
    private Image _aim;
    void Start()
    {
        _aim = gameObject.GetComponent<Image>();
    }

    
    void Update()
    {
        transform.Rotate(new Vector3(0,0,1f)); 
    }
}
