using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class ChatManager : NetworkBehaviour
{

    public static ChatManager instance = null;

    [SerializeField] ChatMessage chatMessagePrefab;
    [SerializeField] CanvasGroup chatContent;
    [SerializeField] InputField chatInput;

    public string playerName = PlayerData.instance.PlayerName;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage(chatInput.text, playerName);
            chatInput.text = "";
        }
    }

    public void SendChatMessage(string _message, string _fromWho = null)
    {
        //check if the string is empty
        if (string.IsNullOrEmpty(_message)) return;

        string s = _fromWho + " : " + _message;
        SendChatMessageServerRpc(s);

    }
    private void AddMessage(string msg)
    {
        ChatMessage chatMessage = Instantiate(chatMessagePrefab, chatContent.transform);
        chatMessage.SetText(msg);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SendChatMessageServerRpc(string message)
    {
        ReceiveChatMessageClientRpc(message);
    }

    [ClientRpc]
    private void ReceiveChatMessageClientRpc(string message)
    {
        ChatManager.instance.AddMessage(message);
    }
}
