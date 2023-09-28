using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private enum Scenes 
    {
        Practice_01,
        MainMenu,
    };
    public void OnStartBtnPressed()
    {
        Debug.Log("Start Button Pressed");
        SceneManager.LoadScene(Scenes.Practice_01.ToString());
    }

    public void OnReturnBtnPressed()
    {
        Debug.Log("Return Button Pressed");
        SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }

   
}
