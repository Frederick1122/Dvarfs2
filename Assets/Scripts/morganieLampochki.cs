using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morganieLampochki : MonoBehaviour
{
    public Light point;
    public Light point2;
    private float _timer = 0f;
    private bool _flag = true;
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= 1f)
        {
            Debug.Log(_flag.ToString());
            _flag = !_flag;
            _timer = 0;
        }

        if(_flag)
        {
            point.enabled = true;
            point2.enabled = false;
        } else
        {
            point.enabled = false;
            point2.enabled = true;
        }
    }
}
