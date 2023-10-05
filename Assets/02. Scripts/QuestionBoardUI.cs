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
    [SerializeField] private int startIndex = 1;
    [SerializeField] private int endIndex = 44;

    [SerializeField] private int minimumCoin = 5;
    [SerializeField] private int maximumCoin = 10;


    private List<string> questionList;
    private List<List<string>> questionListFinal;


    [SerializeField] private Text answer1;
    [SerializeField] private Text answer2;
    [SerializeField] private Text answer3;
    [SerializeField] private Text answer4;
    [SerializeField] private Text question;
    public class WordDefinition
    {
        public string word;
        public string definition;
    }


    private void Start()
    {
        //initialize question string list
        questionList = new List<string>
        {
            "", "", "", ""
        };
        questionListFinal = new List<List<string>>
        {
            new List<string> {"", ""},
            new List<string> {"", ""},
            new List<string> {"", ""},
            new List<string> {"", ""}
        };

        StartCoroutine(ResetQuestions()); //
    }
    private IEnumerator ResetQuestions()
    {
        answer = "One";
        Debug.Log("Started Reset Questions");
        //get four random numbers 
        List<int> randomIds = GenerateUniqueRandomNumbers(startIndex, endIndex, 4);

        //get generated question in json string format
        int completedRequests = 0;
        for (int i=0; i< randomIds.Count; i++)
        {
            int currentIndex = i;
            PhpConnection.instance.GenerateQuestion(randomIds[i], result =>
            {
                questionList[currentIndex] = result;
                //Debug.Log(result);

                completedRequests++;
                if(completedRequests == randomIds.Count)
                {
                    //Debug.Log("completed");
                    Debug.Log(questionList[0]);
                    //all requests complete, now parse Json
                    ProcessQuestions();
                    
                }
            });

            //Debug.Log(PhpConnection.instance.GenerateQuestion(randomIds[i]));
            
        }

        //wait until all requests are complete
        yield return new WaitUntil(() => completedRequests == randomIds.Count);



    }

    //turn json format into double list
    private void ProcessQuestions()
    {
        Debug.Log("Processing questions...");

        //get rid of first and last brackets to allow json parsing method
        for(int i=0; i<questionList.Count; i++)
        {
            string question = questionList[i].Substring(1, questionList[i].Length - 2);
            questionList[i] = question;
        }

        //parse json format and add to string 
        for (int i = 0; i < questionList.Count; i++)
        {
            WordDefinition wordDef = JsonUtility.FromJson<WordDefinition>(questionList[i]);
            
            List<string> pair = new List<string> { wordDef.word, wordDef.definition };
            questionListFinal[i] = pair;
        }
        Debug.Log("Here: " + questionListFinal[0][0]);


        //display definitions to ui
        ShowQuestions();
        
    }

    //show definitions of the questions on the question UI
    private void ShowQuestions()
    {
        //choose which will be the answer
        int answerIndex = Random.Range(0, 4);
        answer = questionListFinal[answerIndex][1];
        question.text = questionListFinal[answerIndex][0]; //will be changed
        Debug.Log("definition: " + answer);
        Debug.Log("question: " + questionListFinal[answerIndex][0]);
        //display in UI
        answer1.text = questionListFinal[0][1];
        answer2.text = questionListFinal[1][1];
        answer3.text = questionListFinal[2][1];
        answer4.text = questionListFinal[3][1];

    }

    public List<int> GenerateUniqueRandomNumbers(int min, int max, int count)
    {
        List<int> numbers = new List<int>();
        

        while (numbers.Count < count)
        {
            int randomNumber = Random.Range(min, max + 1);
            if(!numbers.Contains(randomNumber))
            {
                numbers.Add(randomNumber);
            }
        }

        return numbers;
    }
    public void OnItemBtnClicked()
    {
        Debug.Log("Pressed");

        //if problem can be solved
        if (UIManager.instance.itemAmount > 0)
        {
            questionBoard.SetActive(true);
            StartCoroutine(ResetQuestions());
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
            UIManager.instance.itemAmount -= 1;

            chosenAnswer = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text.ToString();
            //Debug.Log("chosen is " + chosenAnswer);
            
            //give 5-10coins randomly if correct
            if (chosenAnswer == answer)
            {
                UIManager.instance.coinAmount += Random.Range(minimumCoin, maximumCoin + 1);
                Database.Instance.coinScore = UIManager.instance.coinAmount; //update score to database
            }

            StartCoroutine(ResetQuestions());


            if(UIManager.instance.itemAmount <= 0)
            {
                questionBoard.SetActive(false);
            }

        }
        //if used up all items & number of items is 0 or lower
        else
        {
            Debug.Log("Cannot solve more");
            questionBoard.SetActive(false);
            //StartCoroutine(ResetQuestions());
            //UIManager.instance.itemAmount += 1;

        }
        //subtract used item
        //UIManager.instance.itemAmount -= 1;

    }
}
