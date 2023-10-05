using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChattingBoxUI : MonoBehaviour
{
    private bool ChatOn;
    [SerializeField] private GameObject ChattingBox;

    public void OnChatButtonClicked()
    {
        if(ChatOn)
        {
            ChattingBox.SetActive(false);
            ChatOn = false;
        }
        else
        {
            ChattingBox.SetActive(true);
            ChatOn = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ChatOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
