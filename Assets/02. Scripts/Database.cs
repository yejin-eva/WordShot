using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scores that need to be kept until after
public class Database : MonoBehaviour
{
    //created as singleton
    public static Database Instance = null;

    public int coinScore;
    private int coinScorePrivate;
    private void Awake()
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
