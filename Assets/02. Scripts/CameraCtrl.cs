using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform targetTr;
    public float dist = 10.0f;
    public float height = 5.0f;

    public Transform scopeTr;
    public int isScope = -1;
    
    public float dampTrace = 20.0f;

    //ī�޶��� Transform
    private Transform tr;
    
    void Start()
    {
        tr = GetComponent<Transform>();
        
    }

    //�÷��̾� �̵� �� ī�޶� ����
    private void LateUpdate()
    {
        //������ ������� ���� �� 
        if (isScope == -1)
        {
            tr.position = Vector3.Lerp(
            tr.position,
            targetTr.position - (targetTr.forward * dist) + (Vector3.up * height),
            Time.deltaTime * dampTrace);

            tr.LookAt(targetTr.position);

        }
        //������ ����� ��
        else if (isScope == 1)
        {
            tr.position = scopeTr.position;
            
        }
        
        
        



    }
    //!������: ��ü ����ȭ
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BACKGROUND")
        {
            other.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.SetActive(true);
    }
    */



    public void OnScopePressed()
    {
        isScope = isScope * -1;
    }
   
}
