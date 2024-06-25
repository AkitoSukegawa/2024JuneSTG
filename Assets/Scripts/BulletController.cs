using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    /// <summary>�ʏ�e�̈ړ������</summary>
    [SerializeField] float m_nMovePower = 15f;
    /// <summary>�ᑬ�e�̈ړ������</summary>
    [SerializeField] float m_sMovePower = 10f;
    /// <summary>�e�̃^�O�擾�p </summary>
    string m_bulletTag;
    /// <summary>�G�̃^�O�擾�p </summary>
    string m_enemyTag;

    /// <summary>�e���폜����܂ł̊Ԋu(�b)</summary>
    [SerializeField] float m_bDInterval = 1.5f;
    /// <summary>�e�폜�^�C�}�[�p�ϐ�</summary>
    float m_bDTimer;

    Transform m_enemyTrans = default;

    Rigidbody2D m_rb = default;

    

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_bulletTag = this.tag;
        m_bDTimer = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        m_bDTimer += Time.deltaTime;
        if (m_bulletTag == "SlowBullet")
        {
            if (m_enemyTrans != null)
            {
                Vector2 v2 = m_enemyTrans.position - this.transform.position;
                m_rb.velocity = v2.normalized * m_sMovePower;
            }
            else
            {
                m_rb.velocity = new Vector2(0, 1).normalized * m_sMovePower;
            }
        }
        else if (m_bulletTag == "NormalBullet")
        {
            m_rb.velocity = new Vector2(0, 1).normalized * m_nMovePower;
        }
        if (m_bDTimer >= m_bDInterval)
        {
            Destroy(this.gameObject);
        }
    }
    public void TagChecker(string thisTag , Transform thisTransform) 
    {
        m_enemyTag = thisTag;
        Debug.Log(thisTag);
        if (m_enemyTag == "Enemy")
        {
            m_enemyTrans = thisTransform;
        }

    }
}
