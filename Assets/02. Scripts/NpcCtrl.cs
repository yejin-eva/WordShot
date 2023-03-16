using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcCtrl : MonoBehaviour
{
    public GameObject player;
    private float dist;
    private Animator animator;

    //conversation btn hiding image
    public Image conversationImg;

    // Start is called before the first frame update
    void Start()
    {
        //distance 할당
        dist = Vector3.Distance(player.transform.position, this.transform.position);

        //animator 할당
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.transform.position, this.transform.position);
        if (dist < 2.0f) //if the player gets close to NPC
        {
            //change animation
            animator.SetTrigger("IsTalk");
            //activate conversation button
            conversationImg.enabled = false;

        }
        else //if player gets further away
        {
            //change animation
            animator.SetTrigger("IsWait");
            //deactivate conversation button
            conversationImg.enabled = true;

        }
    }
}
