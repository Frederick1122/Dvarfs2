using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurUpgrade : MonoBehaviour
{
    public int level;
    public Collider col;
    public GameObject meshRend;

    public float _timer = 0f;
    void Start()
    {
        //_col = gameObject.GetComponent<Collider>();
        //_meshRend = gameObject.GetComponent<MeshRenderer>();
    }

    void Update()
    {

        if (level <= DataHolder.lvlBur)
        {
            Debug.Log(gameObject);
            _timer += Time.deltaTime;
            if (_timer > 1f)
            {
                if (DataHolder.quantityEnergy <= 100 - 0.1f)
                    DataHolder.quantityEnergy += 0.1f;
                else
                    DataHolder.quantityEnergy = 100f;
                _timer = 0f;
            }
            col.enabled = true;
            meshRend.SetActive(true);
        }
        else
        {
            
            col.enabled = false;
            meshRend.SetActive(false);
        }
    }
}
