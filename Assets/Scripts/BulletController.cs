using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    /// <summary>通常弾の移動する力</summary>
    [SerializeField] float m_nMovePower = 15f;
    /// <summary>低速弾の移動する力</summary>
    [SerializeField] float m_sMovePower = 10f;
    /// <summary>弾のタグ取得用 </summary>
    string m_bulletTag;
    /// <summary>敵のタグ取得用 </summary>
    string m_enemyTag;

    /// <summary>弾を削除するまでの間隔(秒)</summary>
    [SerializeField] float m_bDInterval = 3f;
    /// <summary>弾削除タイマー用変数</summary>
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
        m_enemyTrans = thisTransform;

    }
}
