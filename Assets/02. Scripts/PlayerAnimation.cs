using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Animator animator;

    private CameraCtrl cameraCtrl;
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

    private void Start()
    {
        animator = player.GetComponent<Animator>();
        cameraCtrl = Camera.main.GetComponent<CameraCtrl>();
    }

    private void Update()
    {
        if (cameraCtrl.isScope == -1)
        {
            //movement();
            animMovement();
        }
        else if (cameraCtrl.isScope == 1)
        {
            movementLimit();
        }
    }

    //movementLimit animation
    private void movementLimit()
    {
        //fire animation
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger(AnimMovement.IsFire.ToString());
        }
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
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger(AnimMovement.IsFire.ToString());
        }

    }
}
