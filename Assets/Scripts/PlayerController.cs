using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>�ʏ펞�̈ړ������</summary>
    [SerializeField] float m_normalMovePower = 4f;
    /// <summary>�ᑬ�ړ����̈ړ������</summary>
    [SerializeField] float m_slowMovePower = 2f;
    /// <summary>�X�v���C�g�̊Ǘ��ϐ�</summary>
    [SerializeField] Sprite[] m_sprites = new Sprite[3];
    /// <summary>�ʏ펞�Ɍ��ʂ̃v���n�u</summary>
    [SerializeField] GameObject m_bulletNormal;
    /// <summary>�ᑬ�ړ����Ɍ��ʂ̃v���n�u</summary>
    [SerializeField] GameObject m_bulletSlow;
    /// <summary>�ᑬ�ړ����Ɍ��ʂ̒Ǐ]�p�̃v���n�u</summary>
    [SerializeField] GameObject m_targetting;
    /// <summary>���������̓��͒l</summary>
    float m_h;
    /// <summary>���������̓��͒l</summary>
    float m_v;
    /// <summary>�e�𐶐�����Ԋu(�b)</summary>
    [SerializeField] float m_bulletInterval = 0.5f;
    /// <summary>�e�����^�C�}�[�p�ϐ�</summary>
    float m_bulletTimer;
    /// <summary>�e�����˂ł��邩�ۂ�</summary>
    private bool m_canFire = true;

    /// <summary>�����ʒu�̕ۑ��p�ϐ�</summary>
    Vector3 m_initialPosition;
    /// <summary>�ᑬ�ړ���Ԃ��ۂ� </summary>
    bool m_isSllow = false;
    /// <summary>�e���������ꂽ���̒e���Ƃ̋��� </summary>
    [SerializeField] float m_bulletDistance = 0.1f;


    Rigidbody2D m_rb = default;
    SpriteRenderer m_sr;
    Targetting m_tg1;
    Targetting m_tg2;
    // Start is called before the first frame update
    void Start()
    {
        m_sr = GetComponent<SpriteRenderer>();
        m_rb = GetComponent<Rigidbody2D>();   
        m_initialPosition = this.transform.position;
        m_bulletTimer = m_bulletInterval;
    }

    // Update is called once per frame
    void Update()
    {
        m_h = Input.GetAxisRaw("Horizontal");
        m_v = Input.GetAxisRaw("Vertical");
        m_bulletTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) )
        {
            m_isSllow = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) 
        { 
            m_isSllow = false;
        }
        if (Input.GetKey(KeyCode.Space) && m_canFire)
        {
            if (m_isSllow) 
            {
                GameObject bulletSlow_1 = Instantiate(m_bulletSlow);
                GameObject target_1 = Instantiate(m_targetting);
                bulletSlow_1.transform.position = new Vector2(this.transform.position.x - m_bulletDistance , this.transform.position.y + 0.5f);
                m_tg1 = target_1.GetComponent<Targetting>();
                m_tg1.transform.position = bulletSlow_1.transform.position;
                m_tg1.bulletController = bulletSlow_1.GetComponent<BulletController>();
                GameObject bulletSlow_2 = Instantiate(m_bulletSlow);
                GameObject target_2 = Instantiate(m_targetting);
                bulletSlow_2.transform.position = new Vector2(this.transform.position.x + m_bulletDistance , this.transform.position.y + 0.5f);
                m_tg2 = target_2.GetComponent<Targetting>();
                m_tg2.transform.position = bulletSlow_2.transform.position;
                m_tg2.bulletController = bulletSlow_2.GetComponent<BulletController>();
            }
            else
            {
                GameObject bulletNormal_1 = Instantiate(m_bulletNormal);
                bulletNormal_1.transform.position = new Vector2(this.transform.position.x - m_bulletDistance, this.transform.position.y + 0.5f);
                GameObject bulletNormal_2 = Instantiate(m_bulletNormal);
                bulletNormal_2.transform.position = new Vector2(this.transform.position.x + m_bulletDistance, this.transform.position.y + 0.5f);
            }
            m_bulletTimer = 0f;
            m_canFire = false;
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

        if (m_bulletTimer >= m_bulletInterval) 
        { 
            m_bulletTimer = 0f;
            m_canFire = true;
        }
    }
    private void FixedUpdate()
    {
        if (m_isSllow)
        {
            m_rb.velocity = new Vector2(m_h, m_v).normalized * m_slowMovePower;
        }
        else
        {
            m_rb.velocity = new Vector2(m_h, m_v).normalized * m_normalMovePower;
        }
    }
}
