using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComPer : MonoBehaviour
{

    public Transform player;
    private void Start()
    {
        
    }
    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
    }
}
