using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetting : MonoBehaviour 
{
    public BulletController bulletController;

    string tags;

    Transform target;

    private Rigidbody2D m_rb;
    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        m_rb.velocity = new Vector2(0, 1).normalized * 10;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
            tags = collision.tag;
            target = collision.transform;
            if (tags == "Enemy" || tags == "Mid_Boss" || tags == "Boss")
            {
                bulletController.TagChecker(tags, target);
                Destroy(this.gameObject);
            }
            else if (tags == "Delete")
        {
            Destroy(this.gameObject);
        }
    }
}
