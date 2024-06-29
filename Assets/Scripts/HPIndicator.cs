using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPIndicator : MonoBehaviour
{
    SpriteRenderer m_sr;

    [SerializeField] Sprite[] m_heart;
    // Start is called before the first frame update
    void Start()
    {
        m_sr = GetComponent<SpriteRenderer>();
    }

public void ChangeHPSprite(int sprite)
    {
        m_sr.sprite = m_heart[sprite];
    }
}
