using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapScript : MonoBehaviour
{
    public int hp;
    private int _respawn;
    void Start()
    {
        _respawn = 0;
        RespawnScrap();
    }


    void Update()
    {
        transform.localScale = Vector3.one * hp * 3f;
        if (_respawn < DataHolder.respawnScrap)
        {
            RespawnScrap();
            _respawn++;
        }
    }

    public void RespawnScrap()
    {
        hp = Random.Range(1, 5);
    }
}
