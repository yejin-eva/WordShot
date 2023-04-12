using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInCtrl : MonoBehaviour
{
    public float duration = 1f; //�����ϴ� �� �ɸ��� �ð�
    public GameObject effectPrefab; //���� ����� ����Ʈ ������
    public Transform effectSpawnPoint; //����Ʈ ���� ��ġ
    private Vector3 originalScale;

    public bool isPlayer = false;

    private void Start()
    {
        if (isPlayer) //�÷��̾��� �� ����Ʈ�� ���
        {
            effectSpawnPoint = transform; //����Ʈ ���� ��ġ ������Ʈ ��ġ�� ����
            if (effectPrefab != null && effectSpawnPoint != null)
            {
                GameObject effectInstance = Instantiate(effectPrefab, effectSpawnPoint.position, Quaternion.identity);
                Destroy(effectInstance, 3f); //����Ʈ 1�� ��� �� �ı�
            }
        }
        else //�÷��̾ �ƴ� ��
        {
            originalScale = transform.localScale; //ó�� ������ ����
            transform.localScale = Vector3.zero; //������ 0���� ������ ������ �ʵ���
            effectSpawnPoint = transform; //����Ʈ ���� ��ġ ������Ʈ ��ġ�� ����
            StartCoroutine(FadeIn());
        }
        
    }
    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); //��� �ð� ��� ����� (0-1) ���
            transform.localScale = originalScale * t;

            //���� ���� ����Ʈ ����
            if (effectPrefab != null && effectSpawnPoint != null && t > 0.5f)
            {
                GameObject effectInstance = Instantiate(effectPrefab, effectSpawnPoint.position, Quaternion.identity);
                Destroy(effectInstance, 1f); //����Ʈ ����, 1�� �� �ı�
            }

            yield return null; //�� ������ ���
        }
        transform.localScale = originalScale; //������ ���������� ����
    }
}
