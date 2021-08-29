using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualTurretScript : MonoBehaviour
{
    public MeshRenderer[] turretMesh;

    private int _schetchick;

    private void Start()
    {
        _schetchick = 0;
        for(int i = 0; i < 5; i++)
        {
            if (i != _schetchick)
                turretMesh[i].enabled = false;
            else
                turretMesh[i].enabled = true;

        }
    }
    void Update()
    {
        if (_schetchick+1 != DataHolder.lvlTur)
        {
            _schetchick++;
            for (int i = 0; i < 5; i++)
            {
                if (i != _schetchick)
                    turretMesh[i].enabled = false;
                else
                    turretMesh[i].enabled = true;

            }
        }    
    }
}
