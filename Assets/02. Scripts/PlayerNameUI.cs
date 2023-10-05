using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameUI : MonoBehaviour
{

    [SerializeField] private InputField inputField;
    private void Awake()
    {
    }
    private void Start()
    {

        inputField.text = "Evaline";
        inputField.onValueChanged.AddListener(OnInputValueChanged);

    }

    private void OnInputValueChanged(string newValue)
    {
        //Debug.Log("Value Changed");
        PlayerData.instance.PlayerName = newValue;
        ChatManager.instance.playerName = PlayerData.instance.PlayerName; //change to new name
    }
}
