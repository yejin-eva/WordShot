using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class PhpConnection : MonoBehaviour
{
    //public int questionId = 2;
    public string jsonArray;

    public static PhpConnection instance = null;
    private void Awake()
    {
        instance = this;
    }

    public void GenerateQuestion(int questionId, Action<string> callback)
    {
        StartCoroutine(GetQuestion(questionId, callback));
        //return jsonArray;
    }
    void Start()
    {
        
        // A correct website page.
        //StartCoroutine(GetDate("http://localhost/WordShot/GetDate.php"));

        // A non-existing page.
        //StartCoroutine(GetUsers("http://localhost/WordShot/GetUsers.php"));

        //StartCoroutine(Login("eva", "evaline"));

        //StartCoroutine(GetQuestion(questionId));
    }

    IEnumerator GetQuestion(int wordId, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("wordId", wordId);
        
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/WordShot/GetGreQuestions.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
            jsonArray = www.downloadHandler.text;
            callback(jsonArray);
            //call callback function to pass results
        }
    }
    /*
    IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPassword", password);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/WordShot/Login.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
    IEnumerator GetDate(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            //Debug.Log("Attempting to access URL: " + uri); 

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    IEnumerator GetUsers(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }*/
}
