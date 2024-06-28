using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] GameObject m_back;

    Rigidbody2D m_rb;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rb.velocity = new Vector2(0, -1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Delete4") 
        {
            GameObject back = Instantiate(m_back);
            back.transform.position = new Vector3(-3.5f, 13.25f, 0);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "Delete4")
        { 
        Destroy(this.gameObject);
        }
    }
}
