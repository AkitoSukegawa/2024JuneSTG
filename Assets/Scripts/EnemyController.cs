using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(PolygonCollider2D))]
public class EnemyController : MonoBehaviour
{
    /// <summary>湧いた場所の取得用 </summary>
    public int m_SpawnPointChecker;

    /// <summary>飛んでいく場所用変数</summary>
    [SerializeField] GameObject[] m_StayPoint;
    /// <summary>ボスが飛んでいく場所用変数</summary>
    [SerializeField] GameObject m_BossStayPoint;
    /// <summary>敵の体力</summary>
    [SerializeField] int m_eHP = 100;
    /// <summary>敵の弾の弾速</summary>
    [SerializeField] float m_eBSpeed = 5.0f;
    /// <summary>敵の弾が何Wayか</summary>
    [SerializeField] int m_eBWay = 5;
    /// <summary>敵が撃つ弾の角度</summary>
    [SerializeField] float m_eBAngle = 45.0f;
    /// <summary>敵の弾の発射間隔</summary>
    [SerializeField] float m_eBInterval = 0.5f;
    /// <summary>敵の弾の発射間隔用タイマー</summary>
    float m_eBITimer = 1.0f;
    /// <summary>敵が消えた時にいくつポイントを出すか</summary>
    [SerializeField] int m_pointCount = 1;
    /// <summary>敵の敵の弾のプレハブ</summary>
    [SerializeField] GameObject m_eBPrefab;
    /// <summary>ボスの弾のプレハブ</summary>
    [SerializeField] GameObject[] m_BossBPrefab;
    /// <summary>クリア用のプレハブ</summary>
    [SerializeField] GameObject m_clear;
    /// <summary>ポイントのプレハブ用</summary>
    [SerializeField] GameObject m_normalPointPrefab;
    /// <summary>ボス用の大きいポイントのプレハブ用</summary>
    [SerializeField] GameObject m_bigPointPrefab;
    /// <summary>パワーのプレハブ用</summary>
    [SerializeField] GameObject m_normalPowerPrefab;
    /// <summary>パワーのプレハブ用</summary>
    [SerializeField] GameObject m_bigPowerPrefab;


    Rigidbody2D m_rb = default;
    PolygonCollider2D m_pc = default;

    string tags;
    bool m_isStay = false;
    float m_PI = Mathf.PI;
    float _theta;
    float _bossAnglePlus;
    int m_bossBulletP = 0;
    float BossAngleRange;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_pc = GetComponent<PolygonCollider2D>();
        m_StayPoint = GameObject.FindGameObjectsWithTag("StayPoint");
        m_BossStayPoint = GameObject.Find("BossStay");
        m_rb.gravityScale = 0.0f;
        m_pc.isTrigger = true;
        _bossAnglePlus = Mathf.PI * (m_eBAngle / 180) / 10;
        if (this.tag == "Enemy" || this.tag == "Mid_Boss")
        {
            Vector2 v2 = m_StayPoint[m_SpawnPointChecker].transform.position - this.transform.position;
            m_rb.velocity = v2.normalized * 5;
        }
        else if (this.tag == "Boss")
        {
            Vector2 v2 = m_BossStayPoint.transform.position - this.transform.position;
            m_rb.velocity = v2.normalized * 5;
        }
    }

        // Update is called once per frame
    void Update()
    {
        if (m_isStay == true)
        {
            m_rb.velocity = Vector2.zero;
            m_eBITimer -= Time.deltaTime;
            if (m_eBITimer < 0.0f)
            {
                m_eBITimer = m_eBInterval;
                for (int i = 0; i <= (m_eBWay - 1); i++)
                {
                    float AngleRange = m_PI * (m_eBAngle / 180);
                    if (this.tag == "Boss")
                    {
                        if (m_eBWay > 1) _theta = (AngleRange / (m_eBWay - 1)) * i + m_PI + _bossAnglePlus;
                        else _theta = m_PI;
                        GameObject Bullet = Instantiate(m_BossBPrefab[m_bossBulletP], this.transform.position, this.transform.rotation);
                        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
                        Vector2 bulletV = rb.velocity;
                        bulletV.x = m_eBSpeed * Mathf.Cos(_theta);
                        bulletV.y = m_eBSpeed * Mathf.Sin(_theta);
                        rb.velocity = bulletV;
                        Destroy(Bullet, 5.0f);
                    }
                    else 
                    { 
                        if (this.tag == "Enemy" && m_eBAngle <= 45)
                        {
                            if (m_eBWay > 1) _theta = (AngleRange / (m_eBWay - 1)) * i + -0.833f * (m_PI - AngleRange);
                            else _theta = -0.8f * m_PI;
                        }
                        if (this.tag == "Enemy" && m_eBAngle >= 90)
                        {
                            if (m_eBWay > 1) _theta = (AngleRange / (m_eBWay - 1)) * i + -0.79f * m_PI ;
                            else _theta = -0.8f * m_PI;
                        }
                        if (this.tag == "Mid_Boss")
                        {
                            if (m_eBWay > 1) _theta = (AngleRange / (m_eBWay - 1)) * i + m_PI + _bossAnglePlus;
                            else _theta = m_PI;
                        }
                        GameObject Bullet = Instantiate(m_eBPrefab, this.transform.position, this.transform.rotation);
                        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
                        Vector2 bulletV = rb.velocity;
                        bulletV.x = m_eBSpeed * Mathf.Cos(_theta);
                        bulletV.y = m_eBSpeed * Mathf.Sin(_theta);
                        rb.velocity = bulletV;
                        Destroy(Bullet,5.0f);
                    }
                }
                if(this.tag == "Mid_Boss")
                {
                    BossAngleRange = m_PI * (m_eBAngle / 180) / 7;
                    _bossAnglePlus = _bossAnglePlus + BossAngleRange;
                }
                if (this.tag == "Boss")
                {
                    BossAngleRange = m_PI * (m_eBAngle / 180) / 10;
                    _bossAnglePlus = _bossAnglePlus + BossAngleRange;
                    m_bossBulletP++;
                    if (m_bossBulletP >= m_BossBPrefab.Length ) 
                    {
                        m_bossBulletP = 0;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        tags = collider.tag;
        if (tags == "StayPoint")
        {
            m_isStay = true;
        }
        else if(this.tag == "Boss" && collider.name == "BossStay")
        {
            m_isStay = true;
        }
    }
    public void EnemyHPChanger(int hpChanger)
    {
        m_eHP = m_eHP - hpChanger;
        Debug.Log(m_eHP);
        if (m_eHP <= 0)
        {
            if(this.tag == "Boss")
            {
                Instantiate(m_clear);
            }
            else if (this.tag == "Mid_Boss")
            {
                int i = 0;
                while (i < m_pointCount)
                {
                    GameObject point = Instantiate(m_bigPointPrefab);
                    point.transform.position = this.transform.position;
                    i++;
                }
                GameObject power = Instantiate(m_bigPowerPrefab);
                power.transform.position = this.transform.position;
            }
            else
            {
                int i = 0;
                while (i < m_pointCount)
                {
                    GameObject point = Instantiate(m_normalPointPrefab);
                    point.transform.position = this.transform.position;
                    i++;
                }
                GameObject power = Instantiate(m_normalPowerPrefab);
                power.transform.position = this.transform.position;
            }
            Destroy(this.gameObject);
        }
    }
}
