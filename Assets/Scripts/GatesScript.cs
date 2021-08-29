using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesScript : MonoBehaviour
{
    public float speed;

    public GameObject leftGate;
    public GameObject rightGate;

    public Transform leftBorder;
    public Transform rightBorder;
    public GameObject centerLeft;
    public GameObject centerRight;

    private bool _flag;
    private bool _flag2;
    private AudioSource _au;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _flag = true;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _flag = false;
        }
    }

    void Start()
    {
        _au = gameObject.GetComponent<AudioSource>();
        centerLeft.transform.position = leftGate.transform.position;
        centerRight.transform.position = rightGate.transform.position;
        _flag2 = true;
    }

    void Update()
    {
        if(_flag)
        {
            Audio();
            OpenGates();
        }
        else
        {
            Audio();
            CloseGates();
        }
    }

    void OpenGates()//new Vector3(leftBorder.position.x, leftGate.transform.position.y, leftGate.transform.position.z)
    {
        leftGate.transform.position = Vector3.MoveTowards(leftGate.transform.position, new Vector3(leftBorder.position.x, leftGate.transform.position.y, leftGate.transform.position.z), speed * Time.deltaTime);
        rightGate.transform.position = Vector3.MoveTowards(rightGate.transform.position, new Vector3(rightBorder.position.x, leftGate.transform.position.y, leftGate.transform.position.z), speed * Time.deltaTime);
    
    }
    void CloseGates()
    {
        leftGate.transform.position = Vector3.MoveTowards(leftGate.transform.position, new Vector3(centerLeft.transform.position.x, leftGate.transform.position.y, leftGate.transform.position.z), speed * Time.deltaTime);
        rightGate.transform.position = Vector3.MoveTowards(rightGate.transform.position, new Vector3(centerRight.transform.position.x, leftGate.transform.position.y, leftGate.transform.position.z), speed * Time.deltaTime);
    }
    void Audio()
    {
        if(leftGate.transform.position == new Vector3(leftBorder.position.x, leftGate.transform.position.y, leftGate.transform.position.z) || leftGate.transform.position == new Vector3(centerLeft.transform.position.x, leftGate.transform.position.y, leftGate.transform.position.z))
        {
            _flag2 = true;
            _au.Stop();
        }
        else if(_flag2)
        {
            _au.Play();
            _flag2 = false;
        }
    }
}
