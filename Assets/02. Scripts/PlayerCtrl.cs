using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject player;

    public int hp = 300;
    public int initHp; //initial hp

    CameraCtrl cameraCtrl;

    private float h = 0.0f;
    private float v = 0.0f;

    public float moveSpeed;
    public float rotateSpeed;
    public float runSpeed;

    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;

    Animator animator;

    
    
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
        
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

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
            animator.SetTrigger("Forward");
        else if (Input.GetKeyUp(KeyCode.W))
            animator.SetTrigger("Idle");
        //backward
        if (Input.GetKeyDown(KeyCode.S))
            animator.SetTrigger("Backward");
        else if (Input.GetKeyUp(KeyCode.S))
            animator.SetTrigger("Idle");
        //left
        if (Input.GetKeyDown(KeyCode.A))
            animator.SetTrigger("Left");
        else if (Input.GetKeyUp(KeyCode.A))
            animator.SetTrigger("Idle");
        //right
        if (Input.GetKeyDown(KeyCode.D))
            animator.SetTrigger("Right");
        else if (Input.GetKeyUp(KeyCode.D))
            animator.SetTrigger("Idle");


        //run
        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.W))
        {
            animator.SetTrigger("Run");
        }
        else if (Input.GetMouseButtonUp(1) && Input.GetKey(KeyCode.W))
        {
            animator.SetTrigger("Forward");
        }

        else if (Input.GetKeyUp(KeyCode.W)) //from D
            animator.SetTrigger("Idle");

        //fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("IsFire");
        }

    }

    //스코프 활성화 시 움직임 제한 
    void movementLimit()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

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
            animator.SetTrigger("IsFire");
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
        
    }
}
