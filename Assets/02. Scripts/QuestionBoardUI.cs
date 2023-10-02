using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class QuestionBoardUI : MonoBehaviour
{
    [SerializeField] private GameObject questionBoard;
    private string answer;
    private string chosenAnswer;

    private void ResetQuestions()
    {
        answer = "One";
    }
    public void OnItemBtnClicked()
    {
        Debug.Log("Pressed");

        //if problem can be solved
        if (UIManager.instance.itemAmount > 0)
        {
            questionBoard.SetActive(true);
            ResetQuestions();
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
        if (UIManager.instance.itemAmount > 0)
        {
            
            chosenAnswer = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text.ToString();
            Debug.Log("chosen is " + chosenAnswer);

            //give 5-10coins randomly if correct
            if (chosenAnswer == answer)
            {
                UIManager.instance.coinAmount += Random.Range(5, 11);
                Database.Instance.coinScore = UIManager.instance.coinAmount; //update score to database
            }
            ResetQuestions();


        }
        //if used up all items & number of items is 0 or lower
        else
        {
            Debug.Log("Cannot solve more");
            questionBoard.SetActive(false);
            ResetQuestions();
            UIManager.instance.itemAmount += 1;

        }
        //subtract used item
        UIManager.instance.itemAmount -= 1;

    }
}
