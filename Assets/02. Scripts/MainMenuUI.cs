using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject tutorialView;
    private void Start()
    {
        tutorialView.SetActive(false);
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
}
