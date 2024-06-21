using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    /// <summary>ˆÚ“®‚·‚é—Í</summary>
    [SerializeField] float m_movePower = 15f;

    Rigidbody2D m_rb = default;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>(); 
    }
    // Update is called once per frame
    void Update()
    {
        m_rb.AddForce(Vector2.up * m_movePower, ForceMode2D.Force);
    }
}
