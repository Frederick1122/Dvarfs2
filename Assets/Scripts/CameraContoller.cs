using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraContoller : MonoBehaviour
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

        if (player.GetComponent<PlayerController>().number == 1)
        {
            
            DataHolder.firstCapsule = false;
            DataHolder.secondCapsule = false;
            DataHolder.thirdCapsule = false;
        }
    }


    void LateUpdate()
    {
        _position = target.TransformPoint(_localPosition);
        CameraRotation();
        ObstaclesReact();
        PlayerReact();
        Interface();
        _localPosition = target.InverseTransformPoint(_position);


    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pause = !_pause;
        }
        if(_pause)
        {

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
    void Interface()
    {
        if (player.GetComponent<PlayerController>().number == 1)
        {
            scrapInBack.text = _scrapInBack.ToString();
            rightUpperAngle.text = "Скрап в портфеле";
        }
        else if(player.GetComponent<PlayerController>().number == 2)
        {
            rightUpperAngle.text = "Энергия: " + DataHolder.quantityEnergy.ToString("0.0") + "%";
        }
        else if (player.GetComponent<PlayerController>().number == 3)
        {
            rightUpperAngle.text = "Живых сигнатур " + (DataHolder.bestNumberEnemy - DataHolder.bestNumberEnemyDead).ToString();
        }
        _curs.origin = gameObject.transform.position;
        _curs.direction = gameObject.transform.forward;


        if (_timer > 0 && _timer < 1)
        {
            _timer += Time.deltaTime;
            timer.text = _timer.ToString();
        }
        else if (_timer > 1)
        {
            _timer = 0;
            timer.text = "";
        }
        else if (timer != null)
        {
            timer.text = "";
        }

        if (man.GetBool("isMine"))
        {
            man.SetBool("isMine", false);
        }
        if (man.GetBool("isPush"))
        {
            man.SetBool("isPush", false);
        }
        if (Physics.Raycast(_curs, out _hit) && _timer == 0)
        {
            if (_hit.collider.gameObject.tag == "controller")
            {
                indexObj.text = "компьютер";
                if (_hit.distance < distanceInObj)
                {
                    refer.text = "1 - добытчик-дварф, 2 - дварф-инженер, 3 - дварф-туррельщик";
                    if (Input.GetKeyDown(KeyCode.Alpha1) && player.gameObject.GetComponent<PlayerController>().number != 1)
                    {
                        DataHolder.respawnScrap++;
                        Debug.Log("U down 1");
                        _hit.collider.gameObject.GetComponent<ComputerScript>().Mans[0].SetActive(true);
                        _hit.collider.gameObject.GetComponent<ComputerScript>().Mans[1].SetActive(false);
                        _hit.collider.gameObject.GetComponent<ComputerScript>().Mans[2].SetActive(false);

                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2) && player.gameObject.GetComponent<PlayerController>().number != 2)
                    {
                        DataHolder.respawnScrap++;
                        Debug.Log("U down 2");
                        _hit.collider.gameObject.GetComponent<ComputerScript>().Mans[0].SetActive(false);
                        _hit.collider.gameObject.GetComponent<ComputerScript>().Mans[1].SetActive(true);
                        _hit.collider.gameObject.GetComponent<ComputerScript>().Mans[2].SetActive(false);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3) && player.gameObject.GetComponent<PlayerController>().number != 3)
                    {
                        DataHolder.respawnScrap++;
                        Debug.Log("U down 3");
                        _hit.collider.gameObject.GetComponent<ComputerScript>().Mans[0].SetActive(false);
                        _hit.collider.gameObject.GetComponent<ComputerScript>().Mans[1].SetActive(false);
                        _hit.collider.gameObject.GetComponent<ComputerScript>().Mans[2].SetActive(true);
                    }
                }
            }
            else if (_hit.collider.gameObject.tag == "scrap" && player.GetComponent<PlayerController>().number == 1)
            {
                indexObj.text = "скрап";

                if (_hit.distance < distanceInObj)
                {
                    refer.text = "нажми 'E'";

                    if (Input.GetKeyDown(KeyCode.E) && _scrapInBack < 10)
                    {
                        if (_timer == 0)
                        {
                            gameObject.GetComponent<AudioSource>().PlayOneShot(mining[_miningSchet]);
                            if(_miningSchet+1 < mining.Length)
                            {
                                _miningSchet++;
                            } else
                            {
                                _miningSchet = 0;
                            }
                            _timer += Time.deltaTime;
                            man.SetBool("isMine", true);
                            _scrapInBack++;
                            player.GetComponent<PlayerController>().MoveHero(player.GetComponent<PlayerController>().moveCam, player.GetComponent<PlayerController>().movePlayer);
                            _hit.collider.gameObject.GetComponent<ScrapScript>().hp--;
                        }
                    }
                }

            }
            else if (_hit.collider.gameObject.tag == "turret" && player.GetComponent<PlayerController>().number == 3)
            {
                indexObj.text = "туррель";
                if (_hit.distance < distanceInObj)
                {
                    refer.text = "нажми 'E'";
                    if (Input.GetKeyDown(KeyCode.E) && mainCamera.enabled == true)
                    {
                        _hit.collider.gameObject.GetComponent<AudioSource>().PlayOneShot(turretPlay);

                        indexObj.text = "";
                        refer.text = "B - вернуться";
                        _hit.collider.gameObject.GetComponent<shiftCamera>().shiftCameras();


                    }
                }
            }
            else if (_hit.collider.gameObject.tag == "scrapManager" && player.GetComponent<PlayerController>().number == 1)
            {
                indexObj.text = "Пункт приема скрапа";
                if (_hit.distance < distanceInObj)
                {
                    refer.text = "Нажми 'E', чтобы переложить скрап в хранилище";
                    if (Input.GetKeyDown(KeyCode.E) && mainCamera.enabled == true)
                    {
                        man.SetBool("isPush", true);
                        DataHolder.respawnScrap++;
                        player.GetComponent<PlayerController>().MoveHero(player.GetComponent<PlayerController>().moveCam, player.GetComponent<PlayerController>().movePlayer);
                        DataHolder.quantityScrap += _scrapInBack;
                        _scrapInBack = 0;
                        au.PlayOneShot(clickAu);
                    }
                }
                
            }
            else if (_hit.collider.gameObject.tag == "wallController" && player.GetComponent<PlayerController>().number == 1)
            {
                indexObj.text = "Контроль состояния ворот";
                if (_hit.distance < distanceInObj)
                {
                    refer.text = "заплати 10 скрапа и почини 10% двери (нажми 'E')";
                    if (Input.GetKeyDown(KeyCode.E) && mainCamera.enabled == true)
                    {
                        man.SetBool("isPush", true);
                        au.PlayOneShot(clickAu);
                        player.GetComponent<PlayerController>().MoveHero(player.GetComponent<PlayerController>().moveCam, player.GetComponent<PlayerController>().movePlayer);
                        if (DataHolder.quantityScrap >= 10 && column.GetComponent<ColumnScript>().hp <= 90)
                        {
                            DataHolder.quantityScrap -= 10;
                            column.GetComponent<ColumnScript>().HealColumn();
                        }
                         
                        _scrapInBack = 0;

                    }
                }
            }
            else if (_hit.collider.gameObject.tag == "crystal" && player.GetComponent<PlayerController>().number == 2)
            {
                indexObj.text = "кристалл";
                if (_hit.distance < distanceInObj)
                {
                    refer.text ="нажми 'e'";
                    if (Input.GetKeyDown(KeyCode.E) && mainCamera.enabled == true)
                    {
                        player.GetComponent<PlayerController>().MoveHero(player.GetComponent<PlayerController>().moveCam, player.GetComponent<PlayerController>().movePlayer);
                        man.SetBool("isMine", true);
                        gameObject.GetComponent<AudioSource>().PlayOneShot(mining[_miningSchet]);
                        if (_miningSchet + 1 < mining.Length)
                        {
                            _miningSchet++;
                        }
                        else
                        {
                            _miningSchet = 0;
                        }
                        DataHolder.quantityEnergy += 0.1f;
                        _timer += Time.deltaTime;

                        

                    }
                }
            }
            else if (_hit.collider.gameObject.tag == "upCrystal" && player.GetComponent<PlayerController>().number == 2)
            {
                indexObj.text = "Установка буров";
                if (_hit.distance < distanceInObj)
                {
                    if (DataHolder.lvlBur < 4)
                    {
                        refer.text = "За " + (10 * (DataHolder.lvlBur + 1)).ToString() + " Скрапа можно установить бур(нажми 'e')";
                        if (Input.GetKeyDown(KeyCode.E) && mainCamera.enabled == true && DataHolder.quantityScrap >= 10 * (DataHolder.lvlBur + 1))
                        {
                            au.PlayOneShot(clickAu);
                            player.GetComponent<PlayerController>().MoveHero(player.GetComponent<PlayerController>().moveCam, player.GetComponent<PlayerController>().movePlayer);
                            man.SetBool("isPush", true);
                            DataHolder.quantityScrap -= 10 * (DataHolder.lvlBur + 1);
                            DataHolder.lvlBur += 1;
                        }
                    }
                    else
                    {
                        refer.text = "Максимальное количество буров";
                    }


                }
                }
            
            else if (_hit.collider.gameObject.tag == "upTurret" && player.GetComponent<PlayerController>().number == 3)
            {
                indexObj.text = "Апгрейд туррелей";
                if (_hit.distance < distanceInObj)
                {
                    if (DataHolder.lvlTur < 5)
                    {
                        refer.text = "За " + (10 * (DataHolder.lvlTur + 1)).ToString() + " скрапа можно улучшить туррель(нажми 'e')";
                        if (Input.GetKeyDown(KeyCode.E) && mainCamera.enabled == true && DataHolder.quantityScrap >= 10 * (DataHolder.lvlTur + 1))
                        {
                            au.PlayOneShot(clickAu);
                            player.GetComponent<PlayerController>().MoveHero(player.GetComponent<PlayerController>().moveCam, player.GetComponent<PlayerController>().movePlayer);
                            man.SetBool("isPush", true);
                            DataHolder.quantityScrap -= 10 * (DataHolder.lvlTur + 1);
                            DataHolder.lvlTur += 1;

                        }


                    }
                    else
                    {
                        refer.text = "У вас лучшая туррель";
                    }
                    
                }
            }
            else if (_hit.collider.gameObject.tag == "safeCapsule")
            {
                indexObj.text = "Спасательные капсулы";
                if (_hit.distance < distanceInObj)
                {
                    refer.text = "Нажмите 'е', когда батарея зарядится на полную";
                    if (Input.GetKeyDown(KeyCode.E) && mainCamera.enabled == true && DataHolder.quantityEnergy == 100f)
                    {
                        au.PlayOneShot(clickAu);
                        player.GetComponent<PlayerController>().MoveHero(player.GetComponent<PlayerController>().moveCam, player.GetComponent<PlayerController>().movePlayer);
                        man.SetBool("isPush", true);
                       if(!DataHolder.firstCapsule && player.GetComponent<PlayerController>().number == 1)
                        {
                            if (!DataHolder.firstCapsule && DataHolder.secondCapsule && DataHolder.thirdCapsule)
                            {
                                SceneManager.LoadScene("Wining");
                            }
                            else if(!DataHolder.secondCapsule)
                            {
                                DataHolder.firstCapsule = true;
                                Debug.Log(DataHolder.firstCapsule);
                                Debug.Log(DataHolder.secondCapsule);
                                Debug.Log(DataHolder.thirdCapsule);
                                comp.gameObject.GetComponent<ComputerScript>().Mans[0].SetActive(false);
                                comp.GetComponent<ComputerScript>().Mans[1].SetActive(true);
                                comp.GetComponent<ComputerScript>().Mans[2].SetActive(false);

                            }  else if(!DataHolder.thirdCapsule)
                            {

                                
                                DataHolder.firstCapsule = true;

                                Debug.Log(DataHolder.firstCapsule);
                                Debug.Log(DataHolder.secondCapsule);
                                Debug.Log(DataHolder.thirdCapsule);
                                comp.gameObject.GetComponent<ComputerScript>().Mans[0].SetActive(false);
                                comp.GetComponent<ComputerScript>().Mans[1].SetActive(false);
                                comp.GetComponent<ComputerScript>().Mans[2].SetActive(true);
                            }
                            
                        }
                        if (!DataHolder.secondCapsule && player.GetComponent<PlayerController>().number == 2)
                        {
                            if (DataHolder.firstCapsule && !DataHolder.secondCapsule && DataHolder.thirdCapsule)
                            {
                                SceneManager.LoadScene("Wining");
                            }
                            else if(!DataHolder.firstCapsule)
                            {
                                DataHolder.secondCapsule = true;

                                Debug.Log(DataHolder.firstCapsule);
                                Debug.Log(DataHolder.secondCapsule);
                                Debug.Log(DataHolder.thirdCapsule);
                                comp.GetComponent<ComputerScript>().Mans[0].SetActive(true);
                                comp.GetComponent<ComputerScript>().Mans[1].SetActive(false);
                                comp.GetComponent<ComputerScript>().Mans[2].SetActive(false);
                            }
                            else if (!DataHolder.thirdCapsule){

                                

                                DataHolder.secondCapsule = true;
                                Debug.Log(DataHolder.firstCapsule);
                                Debug.Log(DataHolder.secondCapsule);
                                Debug.Log(DataHolder.thirdCapsule);
                                comp.GetComponent<ComputerScript>().Mans[0].SetActive(false);
                                comp.GetComponent<ComputerScript>().Mans[1].SetActive(false);
                                comp.GetComponent<ComputerScript>().Mans[2].SetActive(true);
                            }
                            
                        }
                        if (!DataHolder.thirdCapsule && player.GetComponent<PlayerController>().number == 3)
                        {
                            if (DataHolder.firstCapsule && DataHolder.secondCapsule && !DataHolder.thirdCapsule)
                            {
                                SceneManager.LoadScene("Wining");
                            }
                            else if (!DataHolder.secondCapsule)
                            {

                               
                                DataHolder.thirdCapsule = true;
                                Debug.Log(DataHolder.firstCapsule);
                                Debug.Log(DataHolder.secondCapsule);
                                Debug.Log(DataHolder.thirdCapsule);
                                comp.GetComponent<ComputerScript>().Mans[0].SetActive(false);
                                comp.GetComponent<ComputerScript>().Mans[1].SetActive(true);
                                comp.GetComponent<ComputerScript>().Mans[2].SetActive(false);
                            }
                            else if (!DataHolder.firstCapsule)
                            {
                                DataHolder.thirdCapsule = true;

                                Debug.Log(DataHolder.firstCapsule);
                                Debug.Log(DataHolder.secondCapsule);
                                Debug.Log(DataHolder.thirdCapsule);
                                comp.GetComponent<ComputerScript>().Mans[0].SetActive(true);
                                comp.GetComponent<ComputerScript>().Mans[1].SetActive(false);
                                comp.GetComponent<ComputerScript>().Mans[2].SetActive(false);
                            }
                            
                        }




                    }
                }
            }
            else if (_hit.collider.gameObject.tag == "computerPanel")
            {

                indexObj.text = "Памятка";
                if (_hit.distance < distanceInObj)
                {
                    refer.text = _hit.collider.gameObject.GetComponent<ComputerPanelScript>().panelText;
                }
            }
            else if (_hit.collider.gameObject.tag == "playGame")
            {

                indexObj.text = "Ручник";
                if (_hit.distance < distanceInObj)
                {
                    refer.text = "Вы готовы? Тогда жмите E";
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        _hit.collider.gameObject.GetComponent<Animator>().SetBool("isPlay", true);
                        SceneManager.LoadScene("MainScene");
                    }
                }
            }
            else
            {
                indexObj.text = "";
                if (refer.text != "*I can't go too far from the ship*")
                    refer.text = "";
                
            }
        }
        else
        {
            indexObj.text = "";
            
        }
    }
}
