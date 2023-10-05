using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image imgHpbar;

    public PlayerCtrl playerCtrl;
    
    //for question
    public int itemAmount;
    public TMP_Text itemAmountShow;

    //for coin
    public GameObject questionBoard; //
    public int coinAmount;
    public Text coinAmountShow;


    public GameObject miniMapObject;

    //create singleton
    public static UIManager instance = null;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        itemAmount = 0;
        //answer = "One";
        coinAmount = Database.Instance.coinScore; //get initial score

    }

    
    void Update()
    {
        imgHpbar.fillAmount = (float)PlayerData.instance.PlayerHp / (float)PlayerData.instance.PlayerInitialHp; //show hp
        itemAmountShow.text = itemAmount.ToString(); //show item
        coinAmountShow.text = coinAmount.ToString(); //show coin
    }

    

    public void OnMinimapBtnClicked()
    {
        miniMapObject.SetActive(true);
    }
    public void OnMinimapBtnCloseClicked()
    {
        miniMapObject.SetActive(false);
    }
}
