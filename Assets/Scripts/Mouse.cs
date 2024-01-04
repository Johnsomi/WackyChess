using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] ControlCenter controlCenter;
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

        if (Input.GetMouseButtonDown(1) && controlCenter.current == null)
        {

            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Use the hit variable to determine what was clicked on.
                if (hit.collider != null)
                {
                    try
                    {
                        Tile tile = hit.collider.GetComponent<Tile>();
                        tile.TryDisplay();
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError(e.Message);
                    }
                    
                    //Debug.Log(hit.collider);
                }
            }
            
        }
    }

}
