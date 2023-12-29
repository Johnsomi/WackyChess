using System;
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
    public int pieceColor = 0;
    [HideInInspector] public Tile currentTile;
    public List<int> moveTypes;
    [HideInInspector] public List<int> abilityType;
    bool canPlace = true;
    private bool hasMoved = false;
    private enum MovementType
    {
        King = 0,
        Queen = 1,
        Bishop = 2,
        Knight = 3,
        Rook = 4,
        Pawn = 5
    }

    private enum Abilities
    {
        Dig = 11,
        Shoot = 12,
        Protect = 13,
        Redirect = 14,
        AreaEffect = 15,
        Swap = 16
    }


  //  private List<int> movements;


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
        //SetMovement();
    }

    //void SetMovement()
    //{
    //    for (int i = 0; i < pieceType.Count; i++)
    //    {
    //        if (!movements.Contains(pieceType[i]))
    //        {
    //            movements.Add(pieceType[i]);
    //        }
    //    }
    //    for (int i = 0; i < abilityType.Count; i++)
    //    {
    //        if (!movements.Contains(abilityType[i]))
    //        {
    //            movements.Add(abilityType[i]);
    //        }
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
       // Debug.DrawLine(gameObject.transform.position, Vector3.back, UnityEngine.Color.red, 5f);
        if (canDrag && mouse.follow)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
        if (Input.GetMouseButtonDown(1) && canDrag) // right click
        {
            Return();
            // Display Ability Menu
        }
    }

    void DisplayMovement(List<int> movements)
    {

    }

    public void GetMovement()
    {
        for (int i = 0; i < moveTypes.Count; i++)
        {
            switch (moveTypes[i])
            {
                case 0:
                    SurroundMove();
                    break;

                case 1:
                    LineMove(0);
                    break;

                case 2: 
                    LineMove(2);
                    break;

                case 3:
                    LMove();
                    break;

                case 4:
                    LineMove(1);
                    break;

                case 5:
                    PawnMove();
                    break;
            }
        }

    }

    void CheckForJumps(Tile tile)
    {

    }

    void PawnMove()
    {
        int y = (currentTile.tilePos.GetLength(0));
        int x = (currentTile.tilePos.GetLength(1));
        if (pieceColor == 1)
        {
            controlCenter.possibles.Add(new Tuple<int, int>(y + 1, x));
            controlCenter.possibles.Add(new Tuple<int, int>(y + 1, x + 1));
            controlCenter.possibles.Add(new Tuple<int, int>(y + 1, x - 1));
            if (!hasMoved)
            {
                controlCenter.possibles.Add(new Tuple<int, int>(y + 2, x));
            }
        }
        else
        {
            controlCenter.possibles.Add(new Tuple<int, int>(y - 1, x));
            controlCenter.possibles.Add(new Tuple<int, int>(y - 1, x + 1));
            controlCenter.possibles.Add(new Tuple<int, int>(y - 1, x - 1));
            if (!hasMoved)
            {
                controlCenter.possibles.Add(new Tuple<int, int>(y - 2, x));
            }
        }
    }

    void LMove()
    {
        int y = (currentTile.tilePos.GetLength(0));
        int x = (currentTile.tilePos.GetLength(1));

        controlCenter.possibles.Add(new Tuple<int, int>(y + 2, x + 1));
        controlCenter.possibles.Add(new Tuple<int, int>(y + 2, x - 1));
        controlCenter.possibles.Add(new Tuple<int, int>(y + 1, x + 2));
        controlCenter.possibles.Add(new Tuple<int, int>(y - 1, x + 2));
        controlCenter.possibles.Add(new Tuple<int, int>(y - 2, x + 1));
        controlCenter.possibles.Add(new Tuple<int, int>(y - 2, x - 1));
        controlCenter.possibles.Add(new Tuple<int, int>(y + 1, x - 2));
        controlCenter.possibles.Add(new Tuple<int, int>(y - 1, x - 2));
    }

    void LineMove(int dir)
    {
        int y = (currentTile.tilePos.GetLength(0));
        int x = (currentTile.tilePos.GetLength(1));
        switch (dir)
        {
            // Omni
            case 0:
                for (int i = 0; i < 9; i++)
                {
                    controlCenter.possibles.Add(new Tuple<int, int>(y + i, x + i));
                    controlCenter.possibles.Add(new Tuple<int, int>(y, x + i));
                    controlCenter.possibles.Add(new Tuple<int, int>(y - i, x + i));
                    controlCenter.possibles.Add(new Tuple<int, int>(y - i, x));
                    controlCenter.possibles.Add(new Tuple<int, int>(y - i, x - i));
                    controlCenter.possibles.Add(new Tuple<int, int>(y, x - i));
                    controlCenter.possibles.Add(new Tuple<int, int>(y + i, x - i));
                    controlCenter.possibles.Add(new Tuple<int, int>(y + i, x));
                }
                break;

            // Hor / Ver
            case 1:
                for (int i = 0; i < 9; i++)
                {
                    controlCenter.possibles.Add(new Tuple<int, int>(y, x + i));
                    controlCenter.possibles.Add(new Tuple<int, int>(y - i, x));
                    controlCenter.possibles.Add(new Tuple<int, int>(y, x - i));
                    controlCenter.possibles.Add(new Tuple<int, int>(y + i, x));
                }
                break;

            // Diag
            case 2:
                for (int i = 0; i < 9; i++)
                {
                    controlCenter.possibles.Add(new Tuple<int, int>(y + i, x + i));
                    controlCenter.possibles.Add(new Tuple<int, int>(y - i, x + i));
                    controlCenter.possibles.Add(new Tuple<int, int>(y - i, x - i));
                    controlCenter.possibles.Add(new Tuple<int, int>(y + i, x - i));
                }
                break;
        }
    }

    void SurroundMove()
    {

        int y = (currentTile.tilePos.GetLength(0));
        int x = (currentTile.tilePos.GetLength(1));

        controlCenter.possibles.Add(new Tuple<int, int>(y + 1, x + 1));
        controlCenter.possibles.Add(new Tuple<int, int>(y, x + 1));
        controlCenter.possibles.Add(new Tuple<int, int>(y - 1, x + 1));
        controlCenter.possibles.Add(new Tuple<int, int>(y - 1, x));
        controlCenter.possibles.Add(new Tuple<int, int>(y - 1, x - 1));
        controlCenter.possibles.Add(new Tuple<int, int>(y, x - 1));
        controlCenter.possibles.Add(new Tuple<int, int>(y + 1, x - 1));
        controlCenter.possibles.Add(new Tuple<int, int>(y + 1, x));
    }

    //public bool CheckCanPlace(Vector2 pos)
    //{
    //    if (currentTile)
    //    return true;
    //}

    private void OnMouseDown()
    {
        // controlCenter.pieceMoved = this.gameObject;
        //controlCenter.movingPiece = true;
        if (controlCenter.current == null)
        {
            controlCenter.current = this;
            canDrag = true;
           // DisplayMovement(moveTypes);
        }
        else
        {
            //CheckCanPlace(transform.position);
            if (canDrag && canPlace && controlCenter.current == this)
            {
                // canDrag = false;
                controlCenter.PositionSet(transform.position, this);
            }
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

    public void Lock(Vector2 pos, Tile tile, bool take)
    {
        if (currentTile != null)
        {
            currentTile.SetPiece(null, false);
            hasMoved = true;
        }
        gameObject.transform.position = pos;
        currentTile = tile;
        currentTile.SetPiece(this, take);
    }

    public void Return()
    {
        gameObject.transform.position = currentTile.transform.position;
        if (canDrag)
        {
            controlCenter.current = null;
            canDrag = false;
        }
    }
}
