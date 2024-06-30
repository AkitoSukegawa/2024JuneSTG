using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    /// <summary>’Êí’e‚ÌˆÚ“®‚·‚é—Í</summary>
    [SerializeField] float m_nMovePower = 15f;
    /// <summary>’á‘¬’e‚ÌˆÚ“®‚·‚é—Í</summary>
    [SerializeField] float m_sMovePower = 10f;
    /// <summary>’e‚Ìƒ^ƒOæ“¾—p </summary>
    string m_bulletTag;

    /// <summary>’e‚ğíœ‚·‚é‚Ü‚Å‚ÌŠÔŠu(•b)</summary>
    [SerializeField] float m_bDInterval = 3f;
    /// <summary>’eíœƒ^ƒCƒ}[—p•Ï”</summary>
    float m_bDTimer;

    Transform m_enemyTrans = default;

    Rigidbody2D m_rb = default;

    EnemyController m_ec;

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
    private void OnTriggerEnter2D(Collider2D collider)
    {
        string tags = collider.tag;
        if (tags == "Enemy" || tags == "Mid_Boss" || tags == "Boss")
        {
            if (m_bulletTag == "NormalBullet")
            {
                m_ec = collider.gameObject.GetComponent<EnemyController>();
                m_ec.EnemyHPChanger(15);
                Destroy(this.gameObject);
            }
            if (m_bulletTag == "SlowBullet")
            {
                m_ec = collider.gameObject.GetComponent<EnemyController>();
                m_ec.EnemyHPChanger(5);
                Destroy(this.gameObject);
            }
        }
        else if (tags == "Wall")
        {
            Destroy (this.gameObject);
        }
    }

    public void TagChecker(string thisTag , Transform thisTransform) 
    {
        m_enemyTrans = thisTransform;

    }
}
