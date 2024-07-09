using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PolygonCollider2D))]
public abstract class ItemBase : MonoBehaviour
{
    /// <summary>ƒAƒCƒeƒ€‚ðŽæ‚Á‚½Žž‚É–Â‚éŒø‰Ê‰¹</summary>
    [SerializeField] AudioClip _sound = default;

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
            if (_sound)
            {
                AudioSource.PlayClipAtPoint(_sound, Camera.main.transform.position);
            }
            OnActivated();
            Destroy(this.gameObject);
        }
        if (collider.tag == "Delete")
        {
            Destroy(this.gameObject);
        }
    }
}
