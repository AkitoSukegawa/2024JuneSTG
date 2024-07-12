using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PolygonCollider2D))]
public abstract class ItemBase : MonoBehaviour
{
    Rigidbody2D m_rb = default;
    PolygonCollider2D m_pc = default;
    public abstract void OnActivated();
    public GameObject Player { get; private set; }

    float m_pullPower = 15.0f;
    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_pc = GetComponent<PolygonCollider2D>();
        m_rb.gravityScale = 0.0f;
        m_pc.isTrigger = true;
        Player = GameObject.FindGameObjectWithTag("Player");
        float i = Random.Range(-0.5f, 0.5f);
        float i2 = Random.Range(-0.5f, 0.5f);
        this.transform.position = new Vector3(this.transform.position.x + i, this.transform.position.y + i2, 0);
    }
    private void Update()
    {
        if (Player != null && Player.transform.position.y >= 2.0f) 
        {
            Vector2 v2 = Player.transform.position - this.transform.position;
            m_rb.velocity = v2.normalized * m_pullPower;
        }
        else
        {
            m_rb.velocity = new Vector2(0, -2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            OnActivated();
            Destroy(this.gameObject);
        }
        if (collider.tag == "Delete")
        {
            Destroy(this.gameObject);
        }
    }
}
