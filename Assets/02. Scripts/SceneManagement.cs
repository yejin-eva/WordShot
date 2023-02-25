using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    public void OnStartBtnPressed()
    {
        Debug.Log("Start Button Pressed");
        SceneManager.LoadScene("Practice_01");
    }

   
}
