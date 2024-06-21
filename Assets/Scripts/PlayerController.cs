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
    /// <summary>���������̓��͒l</summary>
    float m_h;
    /// <summary>���������̓��͒l</summary>
    float m_v;

    Rigidbody2D m_rb = default;
    SpriteRenderer m_sr;

    Vector3 m_initialPosition;
    /// <summary>�ᑬ�ړ���Ԃ��ۂ� </summary>
    bool m_isSllow = false;

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