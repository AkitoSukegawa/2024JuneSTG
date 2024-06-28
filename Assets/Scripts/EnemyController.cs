using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(PolygonCollider2D))]
public class EnemyController : MonoBehaviour
{
    /// <summary>—N‚¢‚½êŠ‚Ìæ“¾—p </summary>
    public int m_SpawnPointChecker;

    /// <summary>”ò‚ñ‚Å‚¢‚­êŠ—p•Ï”</summary>
    [SerializeField] GameObject[] m_StayPoint;

    /// <summary>“G‚Ì‘Ì—Í</summary>
    [SerializeField] int m_eHP = 100;
    /// <summary>“G‚Ì’e‚Ì’e‘¬</summary>
    [SerializeField] float m_eBSpeed = 5.0f;
    /// <summary>“G‚Ì’e‚ª‰½Way‚©</summary>
    [SerializeField] int m_eBWay = 5;
    /// <summary>“G‚Ì“G‚Ì’e‚ÌƒvƒŒƒnƒu</summary>
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
