using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(PolygonCollider2D))]
public class EnemyController : MonoBehaviour
{
    Rigidbody2D m_rb = default;
    PolygonCollider2D m_pc = default;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_pc = GetComponent<PolygonCollider2D>();
        m_rb.gravityScale = 0.0f;
        m_pc.isTrigger = true;
        this.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
