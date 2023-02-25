using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] points;
    public GameObject spawnPoints;
    public GameObject monsterPrefab;
    public List<GameObject> monsterPool = new List<GameObject>();

    public float createTime = 3.0f;
    public int maxMonster = 5;
    public bool isGameOver = false;

    public float sfxVolumn = 1.0f;
    public bool isSfxMute = false;

    //�̱������� ����
    public static GameManager instance = null;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //��������Ʈ ã��
        points = spawnPoints.GetComponentsInChildren<Transform>();

        //���� Ǯ�� ����
        for (int i = 0; i < maxMonster; i++)
        {
            GameObject monster = (GameObject)Instantiate(monsterPrefab);
            monster.name = "Monster_" + i.ToString();
            monster.SetActive(false);
            monsterPool.Add(monster);
        }
        //����Ʈ ã������ ���� ���� ����
        if (points.Length > 0)
        {
            StartCoroutine(this.CreateMonster());
        }
    }

    IEnumerator CreateMonster()
    {
        while (!isGameOver)
        {
            //������ �ð��� ���� �ѹ���
            yield return new WaitForSeconds(createTime);
            if (isGameOver) yield break;

            foreach (GameObject monster in monsterPool)
            {
                if (!monster.activeSelf)
                {
                    //���� Ǯ���� ���� ������ �ϳ� Ȱ��ȭ �� for������ ����
                    int idx = Random.Range(1, points.Length);
                    monster.transform.position = points[idx].position;
                    monster.SetActive(true);
                    break;
                }
            }
        }
    }

    public void PlaySfx(Vector3 pos, AudioClip sfx)
    {
        if (isSfxMute) return;
        GameObject soundObj = new GameObject("Sfx");
        soundObj.transform.position = pos;

        AudioSource audioSource = soundObj.AddComponent<AudioSource>();
        audioSource.clip = sfx;
        audioSource.minDistance = 10.0f;
        audioSource.maxDistance = 30.0f;

        audioSource.volume = sfxVolumn;
        audioSource.Play();

        Destroy(soundObj, sfx.length);
    }
}
