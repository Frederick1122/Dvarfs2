using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class camWin : MonoBehaviour
{

    public float speedY;
    public int limitY;
    public Transform target;
    public float speedX;
    public LayerMask noPlayer;
    public Ray ray;
    public float hideDistance;
    public Camera mainCamera;
    public LayerMask obstacles;
    public Text indexObj;
    public Text refer;
    public GameObject player;
    public AudioSource au;


    public AudioClip turretPlay;
    public Animator man;
    public float distanceInObj;
    public Text timer;
    public Text scrapInBack;
    public GameObject column;
    public Text rightUpperAngle;
    public AudioClip[] mining;
    public GameObject comp;
    public AudioClip clickAu;



    private int _miningSchet;
    private int _scrapInBack;
    private Vector3 _localPosition;
    private float _maxDistance;
    private LayerMask _camOrigin;
    private float _currentYRotation;
    private Ray _curs;
    private RaycastHit _hit;
    private float _timer;
    private bool _pause;

    private Vector3 _position

    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    void Start()
    {
        _miningSchet = 0;
        _localPosition = target.InverseTransformPoint(_position);
        _maxDistance = Vector3.Distance(_position, target.position);
        _camOrigin = mainCamera.cullingMask;
        ray.origin = gameObject.transform.position;
        ray.direction = target.position;

        
    }


    void LateUpdate()
    {
        _position = target.TransformPoint(_localPosition);
        CameraRotation();
        ObstaclesReact();
        PlayerReact();
        
        _localPosition = target.InverseTransformPoint(_position);


    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }

    void CameraRotation()
    {
        var ax = Input.GetAxis("Mouse X");
        var ay = Input.GetAxis("Mouse Y");

        if (ay != 0)
        {

            var tap = Mathf.Clamp(_currentYRotation + ay * speedY * Time.deltaTime, -limitY, limitY);
            if (tap != _currentYRotation)
            {
                var rot = tap - _currentYRotation;
                transform.RotateAround(target.position, transform.right, -rot);
                _currentYRotation = tap;
            }
        }
        if (ax != 0)
        {

            transform.RotateAround(target.position, Vector3.up, ax * speedX * Time.deltaTime);
        }
        transform.LookAt(target);
    }

    void ObstaclesReact()
    {
        var distance = Vector3.Distance(_position, target.position);
        RaycastHit hit;
        if (Physics.Raycast(target.position, transform.position - target.position, out hit, _maxDistance, obstacles))
        {
            _position = hit.point;
        }
        else if (distance < _maxDistance && !Physics.Raycast(_position, -transform.forward, .1f, obstacles))
        {
            _position -= transform.forward * .05f;
        }
    }

    void PlayerReact()
    {
        var distance = Vector3.Distance(_position, target.position);
        if (distance < hideDistance)
        {
            mainCamera.cullingMask = noPlayer;
        }
        else
        {
            mainCamera.cullingMask = _camOrigin;
        }

    }
    
}
