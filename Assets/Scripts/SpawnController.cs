using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    /// <summary>�N���ꏊ��ۑ����Ă����ϐ�</summary>
    [SerializeField] GameObject[] m_spawnController;
    /// <summary>�N���G��ۑ����Ă����ϐ�</summary>
    [SerializeField] GameObject[] m_spawnEnemy;
    /// <summary>�{�X�̃v���n�u�p</summary>
    [SerializeField] GameObject m_boss;
    /// <summary>�{�X�̃X�|�[���ꏊ�p</summary>
    [SerializeField] GameObject m_bossSP;
    /// <summary>�G��S�ł����Ă��玟�ɗN���܂ł̊Ԋu(�b)</summary>
    [SerializeField] float m_eSInterval = 5f;
    /// <summary>�N���Ǘ��̃^�C�}�[�p�ϐ�</summary>
    float m_eSTimer;
    /// <summary>Wave�Ǘ��p�ϐ�</summary>
    int m_spawnCount = 0;
    /// <summary>�G�̗N���𐧌䂷��ϐ�</summary>
    bool m_isSpawned = false;
    /// <summary>�G����ʓ��ɂ��邩���m�F����ϐ�</summary>
    bool m_isEnabled = false;
    /// <summary>�{�X���N���������m�F����ϐ� </summary>
    bool m_bossSpawned = false;
    void Start()
    {
        m_spawnController = GameObject.FindGameObjectsWithTag("SpawnPoint");
        m_bossSP = GameObject.Find("BossSpawn");
        m_eSTimer = 0;
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
            m_spawnCount++;
            for(int i = 0; i < m_spawnController.Length; i++)
            {
                if (m_spawnCount >= 8)
                {
                    GameObject Boss = Instantiate(m_boss);
                    Boss.transform.position = new Vector2(m_bossSP.transform.position.x, m_bossSP.transform.position.y);
                    m_bossSpawned = true;
                    m_isEnabled = true;
                    break;
                }
                else if (m_spawnCount >= 4) 
                {

                    int rand = Random.Range(0, i);
                    GameObject Enemy = Instantiate(m_spawnEnemy[rand]);
                    Enemy.transform.position = new Vector2(m_spawnController[i].transform.position.x, m_spawnController[i].transform.position.y);
                }
                else
                {

                    int rand = Random.Range(m_spawnEnemy.Length - i, m_spawnEnemy.Length);
                    GameObject Enemy = Instantiate(m_spawnEnemy[rand]);
                    Enemy.transform.position = new Vector2(m_spawnController[i].transform.position.x, m_spawnController[i].transform.position.y);
                }
                m_isEnabled = true;
            }
        }
    }
}
