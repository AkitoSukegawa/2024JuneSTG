using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(PolygonCollider2D))]
public class EnemyController : MonoBehaviour
{
    /// <summary>�N�����ꏊ�̎擾�p </summary>
    public int m_SpawnPointChecker;

    /// <summary>���ł����ꏊ�p�ϐ�</summary>
    [SerializeField] GameObject[] m_StayPoint;

    /// <summary>�G�̗̑�</summary>
    [SerializeField] int m_eHP;
    /// <summary>�G�̒e�̒e��</summary>
    [SerializeField] int m_eBSpeed;
    /// <summary>�G�̒e����Way��</summary>
    [SerializeField] int m_eBWay;
    /// <summary>�G�̓G�̒e�̃v���n�u</summary>
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
            Debug.Log(tags);
            m_isStay = true;
        }
    }
}
