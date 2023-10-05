using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform targetTr;
    [SerializeField] private float dist = 10.0f;
    [SerializeField] private float height = 5.0f;

    [SerializeField] private Transform scopeTr;
    public int isScope = -1;

    [SerializeField] private float dampTrace = 20.0f;

    //ī�޶��� Transform
    private Transform tr;

    public static CameraCtrl instance = null;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        tr = GetComponent<Transform>();
        //targetTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

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


    public void OnScopePressed()
    {
        isScope = isScope * -1;
    }
   
}
