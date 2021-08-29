using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform mainCamera;
    public float speed;
    public int angle;
    public bool firstMan;
    public Text refer;
    public GameObject[] otherMan;
    public int number;
    public Animator miningAnim;
    public Animation pushAnim;
    public Animation mineAnim;
    
    public float moveCam;
    public float movePlayer;


    public AudioSource StepsInGround;
    public AudioSource StepsInMetal;
    public bool walkOrNot;


    private bool _pause;

    private Rigidbody _rb;
    void Start()
    {
        _pause = false;
        moveCam = mainCamera.transform.rotation.eulerAngles.y;
        movePlayer = transform.rotation.eulerAngles.y;
        _rb = gameObject.GetComponent<Rigidbody>();
        //transform.Rotate(new Vector3(0, moveCam - movePlayer, 0));
        if (firstMan)
        {
            for(int i = 0; i < otherMan.Length; i++)
            {
                otherMan[i].SetActive(false);
            }
        }
        Cursor.visible = false;
    }


    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        var ax = Input.GetAxisRaw("Horizontal");
        var ay = Input.GetAxisRaw("Vertical");

        moveCam = mainCamera.transform.rotation.eulerAngles.y;
        movePlayer = transform.rotation.eulerAngles.y;


        if (ax != 0 || ay != 0)
        {
            _rb.drag = 0.5f;
            Walking(ax, ay);
            walkOrNot = true;
            if (moveCam != movePlayer)
            {

                if (moveCam > 180)
                {
                    moveCam = moveCam - 360;
                }
                if (movePlayer > 180)
                {
                    movePlayer = movePlayer - 360;
                }
                //Debug.Log("cam: " + moveCam + " pla: " + movePlayer + " ang: " + (movePlayer - moveCam));
                if (ax == 0)
                {
                    if (moveCam - movePlayer > 0.5f || moveCam - movePlayer < -0.5f)
                        MoveHero(moveCam + angle, movePlayer);

                }
                else if (moveCam - movePlayer > 0.5f || moveCam - movePlayer < -0.5f)
                    MoveHero(moveCam, movePlayer);

            }
            if (!miningAnim.GetBool("IsWalk"))
                miningAnim.SetBool("IsWalk", true);
        }
        else if (miningAnim.GetBool("IsWalk"))
        {
            walkOrNot = false;
            StepsInMetal.enabled = false;
            StepsInGround.enabled = false;
            miningAnim.SetBool("IsWalk", false);
            _rb.drag = 7;
        }   
        

        _rb.freezeRotation = true;

        
    }
    void Walking(float ax, float ay)
    {
        if (ay != 0)
            _rb.AddForce(transform.forward * ay * speed);
        if (ax != 0)
            _rb.AddForce(transform.right * ax * speed);
    }
    public void MoveHero(float moveCam, float movePlayer)
    {
        
       transform.Rotate(new Vector3(0, moveCam - movePlayer, 0));
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Border")
        {


            refer.text = "*I can't go too far from the ship*";

        }
        if (walkOrNot)
        {

            if (other.gameObject.tag == "ground")
            {
                StepsInMetal.enabled = false;
                StepsInGround.enabled = true;
            }   else
            {

                StepsInMetal.enabled = true;
                StepsInGround.enabled = false;
            }                             
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Border")
        {
            refer.text = "";

        }
    }

}
