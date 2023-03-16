using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject tutorialView;
    public GameObject conversationView;

    //show coin amount
    public Text coinAmountShow;
    private void Start()
    {
        tutorialView.SetActive(false);
        //load coin amount
        coinAmountShow.text = Database.Instance.coinScore.ToString();
    }
    public void OnTutorialBtnPressed()
    {
        Debug.Log("ButtonPressed");
        tutorialView.SetActive(true);
    }

    public void OnCancelBtnPressed()
    {
        tutorialView.SetActive(false);
    }

    //clicked conversation btn
    public void OnConversationBtnPressed()
    {
        Debug.Log("Clicked conversation Btn");
        conversationView.SetActive(true);
    }

    public void OnConversationEndBtnPressed()
    {
        Debug.Log("End Conversation");
        conversationView.SetActive(false);
    }
}
