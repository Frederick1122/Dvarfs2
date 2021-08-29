using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretController : MonoBehaviour
{
    public int angle;
    public Text indexObj;
    public Text rightUpperAngle;

    public Camera mainCamera;
    public Camera turrelCamera;

    public float MouseSenseInTurrel;
    public Transform turrelBody;

    private float xRotation = 0f;
    private Ray ray;
    private RaycastHit hit;

    public int damage;
    // public GameObject bullet;
    // public GameObject spawnBullet;

    private bool _flag = true;
    public AudioClip turrelStop;
    public AudioClip shot;
    private int _damage;
    public AudioSource au;

    void shoot()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(shot);
        if (Physics.Raycast(ray, out hit)){
            if(hit.transform.gameObject.tag == "enemy")
            hit.collider.gameObject.GetComponent<EnemyScript>().damaged(_damage);
        }
    }


    void Start()
    {
        turrelCamera.enabled = false;

    }

    void Update()
    {
        rightUpperAngle.text = "Живых сигнатур " + (DataHolder.bestNumberEnemy - DataHolder.bestNumberEnemyDead).ToString();
        _damage = damage * DataHolder.lvlTur;
        if (turrelCamera.enabled == true)

        {
            if (_flag)
            {
                
                _flag = false;
            }
            float mouseX = Input.GetAxis("Mouse X") * MouseSenseInTurrel * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSenseInTurrel * Time.deltaTime;


            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            Debug.Log(transform.localPosition);
            transform.localRotation = Quaternion.Euler(xRotation+90, 90f * angle, 0f);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "enemy")
                {
                    indexObj.text = "Enemy(hp:" + hit.collider.gameObject.GetComponent<EnemyScript>().hp + ")";

                }
                else
                {

                    indexObj.text = "";
                }
            }
            else
            {

                indexObj.text = "";
            }
            //turrelBody.Rotate(Vector3.right * mouseX);
            turrelBody.Rotate(Vector3.forward * mouseX);
            ray.origin = gameObject.transform.position;
            ray.direction = gameObject.transform.forward;
            if (Input.GetKeyDown(KeyCode.B))
            {
                Debug.Log("turrel say B");
                mainCamera.gameObject.SetActive(true);
                mainCamera.enabled = true;
                au.PlayOneShot(turrelStop);
                turrelCamera.enabled = false;
                turrelCamera.gameObject.SetActive(false);
                
                
            }
            if (Input.GetButtonDown("Fire1"))
            {
                shoot();
            }
        }

    }
}
