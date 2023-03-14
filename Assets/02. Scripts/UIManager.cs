using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image imgHpbar;
    
    public PlayerCtrl playerCtrl;
    
    public int itemAmount;
    public TMP_Text itemAmountShow;

    public GameObject questionBoard;
    public int coinAmount;
    public Text coinAmountShow;

    private string answer;
    private string chosenAnswer;

    public GameObject miniMapObj;
    // Start is called before the first frame update
    void Start()
    {
        itemAmount = 0;
        answer = "One";

    }

    // Update is called once per frame
    void Update()
    {
        imgHpbar.fillAmount = (float)playerCtrl.hp / (float)playerCtrl.initHp; //show hp
        itemAmountShow.text = itemAmount.ToString(); //show item
        coinAmountShow.text = coinAmount.ToString(); //show coin
    }

    public void OnItemBtnClicked()
    {
        Debug.Log("Pressed");
        
        //if problem can be solved
        if (itemAmount > 0)
        {
            questionBoard.SetActive(true);
        }
    }
    public void OnExitBtnClicked()
    {
        Debug.Log("Exit clicked");
        questionBoard.SetActive(false);
    }

    public void OnAnswerChoiceBtnClicked()
    {
        
        //if there is item available for solving
        if (itemAmount > 0)
        {
            Debug.Log("Hello there");
            chosenAnswer = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text.ToString();
            Debug.Log("chosen is " + chosenAnswer);

            //give 5-10coins randomly if correct
            if (chosenAnswer == answer)
            {
                coinAmount += Random.Range(5, 11);
            }

            

        }
        //if used up all items & number of items is 0 or lower
        else
        {
            Debug.Log("Cannot solve more");
            questionBoard.SetActive(false);
            itemAmount += 1;

        }
        //subtract used item
        itemAmount -= 1;

    }

    public void OnMinimapBtnClicked()
    {
        miniMapObj.SetActive(true);
    }
    public void OnMinimapBtnCloseClicked()
    {
        miniMapObj.SetActive(false);
    }
}
