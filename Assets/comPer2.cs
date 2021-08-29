using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comPer2 : MonoBehaviour
{

    public Transform player;
    private void Start()
    {

    }
    void Update()
    {
        Cursor.visible = false;
        transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
    }
}
