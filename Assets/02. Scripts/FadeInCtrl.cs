using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInCtrl : MonoBehaviour
{
    public float duration = 1f; //등장하는 데 걸리는 시간
    public GameObject effectPrefab; //같이 재생될 이펙트 프리팹
    public Transform effectSpawnPoint; //이펙트 생성 위치
    private Vector3 originalScale;

    public bool isPlayer = false;

    private void Start()
    {
        if (isPlayer) //플레이어일 때 이펙트만 재생
        {
            effectSpawnPoint = transform; //이펙트 생성 위치 오브젝트 위치로 설정
            if (effectPrefab != null && effectSpawnPoint != null)
            {
                GameObject effectInstance = Instantiate(effectPrefab, effectSpawnPoint.position, Quaternion.identity);
                Destroy(effectInstance, 3f); //이펙트 1초 재생 후 파괴
            }
        }
        else //플레이어가 아닐 때
        {
            originalScale = transform.localScale; //처음 스케일 저장
            transform.localScale = Vector3.zero; //스케일 0으로 설정해 보이지 않도록
            effectSpawnPoint = transform; //이펙트 생성 위치 오브젝트 위치로 설정
            StartCoroutine(FadeIn());
        }
        
    }
    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); //경과 시간 대비 진행률 (0-1) 계산
            transform.localScale = originalScale * t;

            //등장 도중 이펙트 생성
            if (effectPrefab != null && effectSpawnPoint != null && t > 0.5f)
            {
                GameObject effectInstance = Instantiate(effectPrefab, effectSpawnPoint.position, Quaternion.identity);
                Destroy(effectInstance, 1f); //이펙트 생성, 1초 뒤 파괴
            }

            yield return null; //한 프레임 대기
        }
        transform.localScale = originalScale; //스케일 최종값으로 설정
    }
}
