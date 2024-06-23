using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    /// <summary>�ʏ�e�̈ړ������</summary>
    [SerializeField] float m_nMovePower = 15f;
    /// <summary>�ᑬ�e�̈ړ������</summary>
    [SerializeField] float m_sMovePower = 10f;
    string m_tag;

    Rigidbody2D m_rb = default;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_tag = this.tag;
    }
    // Update is called once per frame
    void Update()
    {
        if (m_tag == "SlowBullet")
        {
            m_rb.velocity = new Vector2(0, 1).normalized * m_sMovePower;
        }
        else if (m_tag == "NormalBullet")
        {
            m_rb.velocity = new Vector2(0, 1).normalized * m_nMovePower;
        }
    }
}
