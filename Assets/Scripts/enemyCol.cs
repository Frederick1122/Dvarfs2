using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCol : MonoBehaviour
{
    public GameObject enemy;
    public Animator enemyAnim;
    
    public void Start()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            enemyAnim.SetBool("isStop", true);
            enemy.GetComponent<EnemyScript>().speed = 0;
            enemy.GetComponent<Rigidbody>().drag = 1000;
            enemy.GetComponent<EnemyScript>().prov = DataHolder.bestNumberEnemyDead;
            /*if (other.gameObject.GetComponent<EnemyScript>().number < number)
            {
                Destroy(other.gameObject);
            }*/
        }
    }
}
