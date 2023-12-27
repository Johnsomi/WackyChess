using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public bool follow = false;
    public BoxCollider2D mouseBounds;
    // Update is called once per frame
    void Update()
    {
        var cam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cam.z = 0;
        if (mouseBounds.bounds.Contains(cam))
        {
            follow = true;
        }
        else
        {
            follow = false;
        }
    }

}
