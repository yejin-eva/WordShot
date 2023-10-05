using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private GameObject player;

    //public int hp = 300;
    //public int initHp; //initial hp

    private CameraCtrl cameraCtrl;

    private float h = 0.0f;
    private float v = 0.0f;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 mPrevPos = Vector3.zero;
    private Vector3 mPosDelta = Vector3.zero;


    //public event EventHandler OnPlayerHit;
    private enum Movements
    {
        Horizontal,
        Vertical,
    };

    void Start()
    {
        //initHp = PlayerData.instance.PlayerHp;

        moveSpeed = 2f;
        rotateSpeed = 0.5f;
        runSpeed = 5f;
        
        cameraCtrl = Camera.main.GetComponent<CameraCtrl>();


    }

    private void movement()
    {
        h = Input.GetAxis(Movements.Horizontal.ToString());
        v = Input.GetAxis(Movements.Vertical.ToString());

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        
        if (Input.GetKey(KeyCode.W)) //forward
        {
            player.transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.S)) //backward
        {
            player.transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self); 
        }
        //run
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.W))
        {
            player.transform.Translate(moveDir.normalized * runSpeed * Time.deltaTime, Space.Self);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 touchDeltaPosition = Input.mousePosition - mPrevPos;
            player.transform.Rotate(0, touchDeltaPosition.x * rotateSpeed, 0);
        }
        mPrevPos = Input.mousePosition;
        

        
    }


    //스코프 활성화 시 움직임 제한 
    void movementLimit()
    {
        h = Input.GetAxis(Movements.Horizontal.ToString());
        v = Input.GetAxis(Movements.Vertical.ToString());

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        if (Input.GetKey(KeyCode.W)) //forward
        {
            player.transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.S)) //backward
        {
            player.transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        }


        
    }


    // Update is called once per frame
    void Update()
    {
        if (cameraCtrl.isScope == -1)
        {
            movement();
            //animMovement();
        }
        else if (cameraCtrl.isScope == 1)
        {
            movementLimit();
        }
        if(PlayerData.instance.PlayerHp <= 0)
        {
            GameManager.instance.GameOver();
        }
        
    }
}
