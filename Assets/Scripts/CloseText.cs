using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseText : MonoBehaviour
{
    public GameObject miningMan;
    private Text _text;
    private string _dopText;
    void Start()
    {
        _text = gameObject.GetComponent<Text>();
        _dopText = _text.text;
    }

    void Update()
    {
        if(!miningMan.activeInHierarchy && _text.text != "" || miningMan == null)
        {
            _dopText = _text.text;
            _text.text = "";

        } else if(miningMan.activeInHierarchy && _text.text == "")
        {
            _text.text = _dopText;
            
        }
    }
}
