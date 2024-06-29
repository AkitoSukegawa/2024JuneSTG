using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHit : MonoBehaviour
{
    [SerializeField]GameObject m_playerPrefab;

    GameObject m_player;

    Rigidbody2D m_rb;

    CircleCollider2D m_cc;

    SpriteRenderer m_sr;

    int m_health;

    HPIndicator m_hp;

    float m_SpawnTimer = 0.0f;

    float m_SpawnInterval = 1.0f;

    public bool isAlived = false;

    bool m_spawned = true;

    PlayerController m_pCon;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("Player");
        m_rb = GetComponent<Rigidbody2D>();
        m_cc = GetComponent<CircleCollider2D>();
        m_sr = GetComponent<SpriteRenderer>();
        m_health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player != null)
        {
            m_rb.transform.position = m_player.transform.position;
        }
        else
        {
            m_SpawnTimer += Time.deltaTime;
            if (m_SpawnTimer >= m_SpawnInterval)
            {
                m_SpawnTimer = 0f ;
                if (m_spawned)
                { 
                    GameObject Player = Instantiate(m_playerPrefab);
                    Player.transform.position = new Vector3(-3.5f, -3.5f, 0f);
                    m_pCon = Player.GetComponent<PlayerController>();   
                    m_pCon.isSheer = true;
                    m_spawned = false;
                }
            }
        }

        if (m_cc.enabled == false)
        {
            if(isAlived)
            {
                m_cc.enabled = true;
                m_sr.color = new Color(255, 255, 255, 255);
                m_player = GameObject.FindWithTag("Player");
                m_rb.transform.position = m_player.transform.position;
                isAlived = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "EnemyBullet")
        {
            m_cc.enabled = false;
            m_sr.color = new Color(255, 255, 255, 0);
            m_spawned = true;
            m_player = null;
            GameObject p = GameObject.FindWithTag("Player");
            Destroy(p);
            if (m_health == 3)
            {
                m_health = 2;
                m_hp = GameObject.Find("HPBox_3").GetComponent<HPIndicator>();
                if (m_hp != null)
                {
                    m_hp.ChangeHPSprite(1);
                }
            }
            else if (m_health == 2)
            {
                m_health = 1;
                m_hp = GameObject.Find("HPBox_2").GetComponent<HPIndicator>();
                if (m_hp != null)
                {
                    m_hp.ChangeHPSprite(1);
                }
            }
            else if (m_health == 1)
            {
                m_health = 0;
                m_hp = GameObject.Find("HPBox_1").GetComponent<HPIndicator>();
                if (m_hp != null)
                {
                    m_hp.ChangeHPSprite(1);
                }
                Invoke(nameof(OnGameOver), 0.5f);

            }
        }
    }

    private void OnGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
