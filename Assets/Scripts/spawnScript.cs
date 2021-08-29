using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour
{
    public GameObject enemy;
    public float Delay;
    public GameObject targetTurrel;
    public GameObject centerRoad;
    private Collider col;
    private float timer;


    private void spawnEnemy()
    {
       var a = Instantiate(enemy, new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x), transform.position.y, transform.position.z), Quaternion.identity);
       a.GetComponent<EnemyScript>().target = targetTurrel;
       a.GetComponent<EnemyScript>().targetRoad = centerRoad;
       a.GetComponent<EnemyScript>().number = DataHolder.enemyNumber;
        DataHolder.enemyNumber++;
        DataHolder.bestNumberEnemy++;
    }

    private void Start()
    {
        timer = 0;
        col = gameObject.GetComponent<Collider>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5 + Random.Range(1, Delay)) 
        {
            spawnEnemy();
            timer = 0;
        }
    }
}
