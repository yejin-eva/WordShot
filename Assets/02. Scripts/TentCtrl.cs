using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TentCtrl : MonoBehaviour
{
    public string sceneName;
    public GameObject player;
    float dist;
    
    // Start is called before the first frame update
    void Start()
    {
        dist = Vector3.Distance(this.transform.position, player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(this.transform.position, player.transform.position);
        if (dist < 1.0f)
        {
            Debug.Log("Entering " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }
}
