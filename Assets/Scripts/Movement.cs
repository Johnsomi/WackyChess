using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Mouse mouse;
    ControlCenter controlCenter;
    [SerializeField] LayerMask tileLayer;
    [HideInInspector] public bool canDrag = false;
    public string pieceID = "null";
    [HideInInspector] public Tile currentTile;

    private void Awake()
    {
        mouse = GameObject.Find("Mouse").GetComponent<Mouse>();
        tileLayer = LayerMask.NameToLayer("Tile");
        controlCenter = GameObject.Find("ControlCenter").GetComponent<ControlCenter>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        controlCenter.PositionSet(transform.position, this);
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.DrawLine(gameObject.transform.position, Vector3.back, UnityEngine.Color.red, 5f);
        if (canDrag && mouse.follow)
        {
            //if (controlCenter.currentPiece == this)
            //{
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                ////mousePos.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
                //// RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                //if (hit.collider != null)
                //{
                //    transform.position = mousePos;
                //}
            transform.position = mousePos;
            //}
        }
    }

    private void OnMouseDown()
    {
        // controlCenter.pieceMoved = this.gameObject;
        //controlCenter.movingPiece = true;
        if (controlCenter.current == null)
        {
            controlCenter.current = this;
            canDrag = true;
        }
        else if (canDrag && controlCenter.current == this)
        {
           // canDrag = false;
            controlCenter.PositionSet(transform.position, this);
        }
    }

    //private void OnMouseDrag()
    //{
    //    if (controlCenter.currentPiece == this || controlCenter.currentPiece == null)
    //    {
    //        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
    //        //mousePos.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
    //        // RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
    //        if (hit.collider != null)
    //        {
    //            hit.collider.transform.position = mousePos;
    //        }
    //    }
    //}

  //  private void OnMouseUp()
  //  {
       // canDrag = false;
      //  controlCenter.PositionSet(transform.position);



        //controlCenter.movingPiece = false;
        //Vector2 hitpos = Camera.main.ScreenToWorldPoint(gameObject.transform.position);

        // GetComponent<BoxCollider2D>().autoTiling = true;
        //RaycastHit hit;
        
        //if (Physics.Raycast(gameObject.transform.position, Vector3.back, out hit, Mathf.Infinity, tileLayer))
        //{
        //    if (hit.collider != null)
        //    {
        //        try
        //        {
        //            tile = hit.collider.gameObject.GetComponent<Tile>();
        //            gameObject.transform.position.Set(tile.transform.position.x, tile.transform.position.y, transform.position.z);
        //        }
        //        catch { Debug.Log("Can't see tile."); }
        //    }
        //}



  //  }

    public void Lock(Vector2 pos, Tile tile)
    {
        if (currentTile != null)
        {
            currentTile.SetPiece("");
        }
        gameObject.transform.position = pos;
        currentTile = tile;
        currentTile.SetPiece(pieceID);
    }

    public void Return()
    {
        gameObject.transform.position = currentTile.transform.position;
    }
}
