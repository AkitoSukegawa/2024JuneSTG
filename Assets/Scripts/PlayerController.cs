using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>通常時の移動する力</summary>
    [SerializeField] float m_normalMovePower = 4f;
    /// <summary>低速移動時の移動する力</summary>
    [SerializeField] float m_slowMovePower = 2f;
    /// <summary>スプライトの管理変数</summary>
    [SerializeField] Sprite[] m_sprites = new Sprite[3];
    /// <summary>通常時に撃つ玉のプレハブ</summary>
    [SerializeField] GameObject m_bulletNormal;
    /// <summary>水平方向の入力値</summary>
    float m_h;
    /// <summary>垂直方向の入力値</summary>
    float m_v;

    Rigidbody2D m_rb = default;
    SpriteRenderer m_sr;

    Vector3 m_initialPosition;
    /// <summary>低速移動状態か否か </summary>
    bool m_isSllow = false;
    /// <summary>弾が生成された時の弾ごとの距離 </summary>
    [SerializeField] float m_bulletDistance = 0.1f;

    /// <summary>プレイヤーの座標取得用 </summary>
    Vector3 m_playerPotition = default;

    // Start is called before the first frame update
    void Start()
    {
        m_sr = GetComponent<SpriteRenderer>();
        m_rb = GetComponent<Rigidbody2D>();   
        m_initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_h = Input.GetAxisRaw("Horizontal");
        m_v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) )
        {
            m_isSllow = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) 
        { 
            m_isSllow = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bulletNormal_1 = Instantiate(m_bulletNormal);
            bulletNormal_1.transform.position = new Vector2(this.transform.position.x - m_bulletDistance, this.transform.position.y);
            GameObject bulletNormal_2 = Instantiate(m_bulletNormal);
            bulletNormal_2.transform.position = new Vector2(this.transform.position.x + m_bulletDistance, this.transform.position.y);
        }

        if (m_isSllow)
        {
            m_rb.velocity = new Vector2(m_h, m_v).normalized * m_slowMovePower;
        }
        else
        {
            m_rb.velocity = new Vector2(m_h, m_v).normalized * m_normalMovePower;
        }

        if (m_h >= 1)
        {
            m_sr.sprite = m_sprites[1];
        }
        else if (m_h <= -1)
        {
            m_sr.sprite = m_sprites[2];
        }
        else 
        {
            m_sr.sprite = m_sprites[0];
        }
    }

}
