using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    /// <summary>湧く場所を保存しておく変数</summary>
    [SerializeField] GameObject[] m_spawnController;
    /// <summary>湧く敵を保存しておく変数</summary>
    [SerializeField] GameObject[] m_spawnEnemy;
    /// <summary>中ボスのプレハブ用</summary>
    [SerializeField] GameObject m_midBoss;
    /// <summary>ボスのプレハブ用</summary>
    [SerializeField] GameObject m_boss;
    /// <summary>ボスのスポーン場所用</summary>
    [SerializeField] GameObject m_bossSP;
    /// <summary>敵を全滅させてから次に湧くまでの間隔(秒)</summary>
    [SerializeField] float m_eSInterval = 5f;
    /// <summary>湧き管理のタイマー用変数</summary>
    float m_eSTimer;
    /// <summary>Wave管理用変数</summary>
    int m_spawnCount;
    /// <summary>敵の湧きを制御する変数</summary>
    bool m_isSpawned = false;
    /// <summary>敵が画面内にいるかを確認する変数</summary>
    bool m_isEnabled = false;
    /// <summary>ボスが湧いたかを確認する変数 </summary>
    bool m_bossSpawned = false;

    EnemyController m_ec;
    void Start()
    {
        m_spawnController = GameObject.FindGameObjectsWithTag("SpawnPoint");
        m_bossSP = GameObject.Find("BossSpawn");
        m_eSTimer = 0;
        m_spawnCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isSpawned && !m_bossSpawned) 
        { 
            m_eSTimer += Time.deltaTime;
            if (m_eSTimer >= m_eSInterval)
            {
                m_isSpawned = true;
            }
        }
        if (m_isSpawned && !m_isEnabled)
        {
            m_isEnabled = true;
            m_spawnCount++;
            Debug.Log(m_spawnCount);
            for(int i = 0; i < m_spawnController.Length; i++)
            {
                if (m_spawnCount >= 9)
                {
                    break;
                }
                if (m_spawnCount == 8)
                {
                    GameObject Boss = Instantiate(m_boss);
                    Boss.transform.position = new Vector2(m_bossSP.transform.position.x, m_bossSP.transform.position.y);
                    m_bossSpawned = true;
                    m_isEnabled = true;
                    break;
                }
                else if (m_spawnCount >= 5)
                {
                    int rand = Random.Range(m_spawnEnemy.Length - i - 1, m_spawnEnemy.Length - 1);
                    GameObject Enemy = Instantiate(m_spawnEnemy[rand]);
                    Enemy.transform.position = new Vector2(m_spawnController[i].transform.position.x, m_spawnController[i].transform.position.y);
                    m_ec = Enemy.GetComponent<EnemyController>();
                    m_ec.m_SpawnPointChecker = i;
                }
                else if (m_spawnCount == 4)
                {
                    if (i <= 2)
                    {
                        GameObject Enemy = Instantiate(m_midBoss);
                        Enemy.transform.position = new Vector2(m_spawnController[i].transform.position.x, m_spawnController[i].transform.position.y);
                        m_ec = Enemy.GetComponent<EnemyController>();
                        m_ec.m_SpawnPointChecker = i;
                    }
                    else 
                    {
                        break;
                    }
                }
                    else
                {
                    int rand = Random.Range(0, i);
                    GameObject Enemy = Instantiate(m_spawnEnemy[rand]);
                    Enemy.transform.position = new Vector2(m_spawnController[i].transform.position.x, m_spawnController[i].transform.position.y);
                    m_ec = Enemy.GetComponent<EnemyController>();
                    m_ec.m_SpawnPointChecker = i;
                }
            }
        }
        if (m_isEnabled)
        {
            GameObject[] EnemyEnabled = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] MidBossEnabled = GameObject.FindGameObjectsWithTag("Mid_Boss");
            GameObject[] BossEnabled = GameObject.FindGameObjectsWithTag("Boss");
            if (EnemyEnabled.Length <= 0 && MidBossEnabled.Length <= 0 && BossEnabled.Length <= 0)
            {
                m_isEnabled = false;
                m_isSpawned = false;
                m_eSTimer = 0f;
            }
        }
    }
}
