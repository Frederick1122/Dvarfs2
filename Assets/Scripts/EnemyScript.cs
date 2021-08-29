using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public float speed;
    public GameObject target; // должно передаваться из спавна. Федя не забудь!!
    public GameObject targetRoad; // должно передаваться из спавна. Федя не забудь!!
    public int hp;
    public int number;
    public int prov;
    public Animator animEnemy;
    public AudioSource drive;
    public AudioSource drell;

    private Transform tr;
    private float _timer = 0f;
    private bool _flag = false;
    private bool _flag2 = false;
    private float _speed;
    private bool _driveOrNot;
    private bool _drellOrNot;
    
    public void damaged(int damage)
    {
        hp -= damage;
    }

    void Start()
    {
        
        tr = gameObject.transform;
        _speed = speed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "column")
        {
            speed = 0;
            _flag = true;
            animEnemy.SetBool("IsAttack", true);
            if (_drellOrNot)
            {
                drell.Play();
                drive.Stop();
            }
                
        }
        if (other.gameObject.tag == "centerRoad")
        {
            _flag2 = true;
        }
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    void Update()
    {

      
            
     
            if (DataHolder.bestNumberEnemyDead > prov)
            {
                gameObject.GetComponent<Rigidbody>().drag = 0;
                speed = _speed;
                prov++;
                animEnemy.SetBool("isStop", false);
          
            }
            if (hp <= 0)
            {
                DataHolder.bestNumberEnemyDead++;
                Destroy(gameObject);
            }
            if (speed == 0 && _flag)
            {
                gameObject.GetComponent<Rigidbody>().drag = 1000;
                _timer += Time.deltaTime;
                //Debug.Log(_timer);
                if (_timer >= 1f)
                {
                    _timer = 0;
                    target.GetComponent<ColumnScript>().DamageColumn(2);
                }
            }
        }


    private void FixedUpdate()
    {
        if (!_flag2)
        {
            tr.position = Vector3.MoveTowards(tr.position, targetRoad.transform.position, speed * Time.deltaTime);
            Vector3 dir = targetRoad.transform.position - transform.position - new Vector3(0, targetRoad.transform.position.y - transform.position.y, 0);
            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = rotation;
            transform.Rotate(0, transform.rotation.y, 0);
        }
        else
        {
            tr.position = Vector3.MoveTowards(tr.position, target.transform.position, speed * Time.deltaTime);
            Vector3 dir = target.transform.position - transform.position - new Vector3(0, target.transform.position.y - transform.position.y, 0);
            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = rotation;
            transform.Rotate(0, transform.rotation.y, 0);
        }
    }
}
