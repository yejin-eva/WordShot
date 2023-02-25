using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image imgHpbar;
    
    public PlayerCtrl playerCtrl;
    
    public int itemAmount;
    public TMP_Text itemAmountShow;
    // Start is called before the first frame update
    void Start()
    {
        itemAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        imgHpbar.fillAmount = (float)playerCtrl.hp / (float)playerCtrl.initHp;
        itemAmountShow.text = itemAmount.ToString();
    }
}
