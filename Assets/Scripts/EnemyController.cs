using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(PolygonCollider2D))]
public class EnemyController : MonoBehaviour
{
    /// <summary>湧いた場所の取得用 </summary>
    public int m_SpawnPointChecker;

    /// <summary>飛んでいく場所用変数</summary>
    [SerializeField] GameObject[] m_StayPoint;

    /// <summary>敵の体力</summary>
    [SerializeField] int m_eHP = 100;
    /// <summary>敵の弾の弾速</summary>
    [SerializeField] float m_eBSpeed = 5.0f;
    /// <summary>敵の弾が何Wayか</summary>
    [SerializeField] int m_eBWay = 5;
    /// <summary>敵の敵の弾のプレハブ</summary>
    [SerializeField] GameObject m_eBPrefab;

    Rigidbody2D m_rb = default;
    PolygonCollider2D m_pc = default;

    string tags;
    bool m_isStay = false;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_pc = GetComponent<PolygonCollider2D>();
        m_StayPoint = GameObject.FindGameObjectsWithTag("StayPoint");
        m_rb.gravityScale = 0.0f;
        m_pc.isTrigger = true;
        this.tag = "Enemy";
        Vector2 v2 = m_StayPoint[m_SpawnPointChecker].transform.position - this.transform.position;
        m_rb.velocity = v2.normalized * 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isStay == true)
        {
            m_rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        tags = collider.tag;
        if (tags == "StayPoint")
        {
            m_isStay = true;
        }
    }
    public void EnemyHPChanger(int hpChanger)
    {
        m_eHP = m_eHP - hpChanger;
        Debug.Log(m_eHP);
        if (m_eHP < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
