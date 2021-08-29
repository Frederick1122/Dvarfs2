using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColumnScript : MonoBehaviour
{
    public float delay;

    public TextMesh hpColumn;

    //private float _timer = 0f;
    public int hp;



    void Start()
    {
        hp = 100;
    }
    private void Update()
    {
        hpColumn.text = hp.ToString();
        DataHolder.hpGates = hp;
        if (hp <= 0)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
    public void DamageColumn(int damage)
    {
        hp -= damage;
        Debug.Log(hp);
    }
    public void HealColumn()
    {
        hp += 10;
    }

}
