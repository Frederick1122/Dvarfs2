using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour
{
    public TextMesh _quantityScrap;
    public TextMesh _quantityEnergy;
    public TextMesh _hpGates;
    public TextMesh _lvlTur;
    public TextMesh _lvlBur;

    private void Start()
    {
        DataHolder.lvlBur = 0;
        DataHolder.lvlTur = 1;
        DataHolder.quantityEnergy = 0;
        DataHolder.quantityScrap = 0;

    }
    void Update()
    {
        _quantityScrap.text = DataHolder.quantityScrap.ToString();        
        _quantityEnergy.text = DataHolder.quantityEnergy.ToString("0.0") + "%";        
        _hpGates.text = DataHolder.hpGates.ToString();        
        _lvlTur.text = DataHolder.lvlTur.ToString();        
        _lvlBur.text = DataHolder.lvlBur.ToString();
        Cursor.visible = false;
    }
}
