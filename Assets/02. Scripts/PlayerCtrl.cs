using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public int hp = 300;
    public int initHp; //initial hp

    private CameraCtrl cameraCtrl;

    private float h = 0.0f;
    private float v = 0.0f;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float runSpeed;

    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;

    Animator animator;

    public event EventHandler OnPlayerHit;
    private enum Movements
    {
        Horizontal,
        Vertical,
    };
    private enum AnimMovement
    {
        Idle,
        Forward,
        Backward,
        Left,
        Right,
        Run,
        IsFire,
    };

    // Start is called before the first frame update
    void Start()
    {
        initHp = hp;

        moveSpeed = 2f;
        rotateSpeed = 0.5f;
        runSpeed = 5f;
        animator = player.GetComponent<Animator>();
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

    
    private void animMovement()
    {
        //forward
        if (Input.GetKeyDown(KeyCode.W))
            animator.SetTrigger(AnimMovement.Forward.ToString());
        else if (Input.GetKeyUp(KeyCode.W))
            animator.SetTrigger(AnimMovement.Idle.ToString());
        //backward
        if (Input.GetKeyDown(KeyCode.S))
            animator.SetTrigger(AnimMovement.Backward.ToString());
        else if (Input.GetKeyUp(KeyCode.S))
            animator.SetTrigger(AnimMovement.Idle.ToString());
        //left
        if (Input.GetKeyDown(KeyCode.A))
            animator.SetTrigger(AnimMovement.Left.ToString());
        else if (Input.GetKeyUp(KeyCode.A))
            animator.SetTrigger(AnimMovement.Idle.ToString());
        //right
        if (Input.GetKeyDown(KeyCode.D))
            animator.SetTrigger(AnimMovement.Right.ToString());
        else if (Input.GetKeyUp(KeyCode.D))
            animator.SetTrigger(AnimMovement.Idle.ToString());


        //run
        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.W))
        {
            animator.SetTrigger(AnimMovement.Run.ToString());
        }
        else if (Input.GetMouseButtonUp(1) && Input.GetKey(KeyCode.W))
        {
            animator.SetTrigger(AnimMovement.Forward.ToString());
        }

        else if (Input.GetKeyUp(KeyCode.W)) //from D
            animator.SetTrigger(AnimMovement.Idle.ToString());

        //fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger(AnimMovement.IsFire.ToString());
        }

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


        //fire animation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger(AnimMovement.IsFire.ToString());
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (cameraCtrl.isScope == -1)
        {
            movement();
            animMovement();
        }
        else if (cameraCtrl.isScope == 1)
        {
            movementLimit();
        }
        if(hp <= 0)
        {
            GameManager.instance.GameOver();
        }
        
    }
}
