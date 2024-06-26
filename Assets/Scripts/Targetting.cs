using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetting : MonoBehaviour 
{
    public BulletController bulletController;

    string tags;

    Transform target;

    bool isTarget = false;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTarget)
        {
            tags = collision.tag;
            target = collision.transform;
            if (tags == "Enemy")
            {
                bulletController.TagChecker(tags, target);
                isTarget = true;
            }
        }
    }
}
