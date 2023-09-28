using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TentCtrl : MonoBehaviour
{
    [SerializeField] private enum SceneNames
    {
        Practice_01,
        Practice_02,
        Practice_Multiplayer,
    };
    [SerializeField] private SceneNames sceneName = SceneNames.Practice_01;
    [SerializeField] private GameObject player;
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
            Debug.Log("Entering " + sceneName.ToString());
            SceneManager.LoadScene(sceneName.ToString());
            
        }
    }
}
