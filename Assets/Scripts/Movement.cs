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
    public Abilities abilities;
   // [SerializeField] LayerMask tileLayer;
    [HideInInspector] public bool canDrag = false;
    public int pieceColor = 0;
    [HideInInspector] public Tile currentTile;
    public List<int> moveTypes;
     public List<int> abilityType;
    [HideInInspector] public bool canPlace = true;
    [HideInInspector] public bool canTarget = false;
    private int hasMoved = 0;
    [SerializeField] bool king = false;
    //public bool abilityActive = false;
    public int usedAbility;
    [HideInInspector] public bool frozen = false;
    int frozenTick = 3;
    int poisonTick = 3;
    [HideInInspector] public bool poison = false;
    private enum MovementType
    {
        King = 0,
        Queen = 1,
        Bishop = 2,
        Knight = 3,
        Rook = 4,
        Pawn = 5
    }

    //private enum Abilities
    //{
    //    Dig = 11,
    //    Shoot = 12,
    //    Protect = 13,
    //    Redirect = 14,
    //    AreaEffect = 15,
    //    Swap = 16
    //}


  //  private List<int> movements;


    private void Awake()
    {
        mouse = GameObject.Find("Mouse").GetComponent<Mouse>();
       // tileLayer = LayerMask.NameToLayer("Tile");
        controlCenter = GameObject.Find("ControlCenter").GetComponent<ControlCenter>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (abilities == null)
        {
            abilities = GameObject.Find("AbilityController").GetComponent<Abilities>();
        }
        controlCenter.PositionSet(transform.position, this, false);
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
        if (Input.GetMouseButtonDown(1) && canDrag && abilities.piece != this) // right click
        {
            controlCenter.ColorSet(false);
            Return();
            // Display Ability Menu
        }
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


    void PawnMove()
    {
        int y = (currentTile.tilePos.GetLength(0));
        int x = (currentTile.tilePos.GetLength(1));
        if (pieceColor == 1)
        {
            if (hasMoved == 0)
            {
                for (int i = 1; i < 3; i++)
                {
                    if (controlCenter.CheckForJump(new Tuple<int, int>(y + i, x), this, true) == true)
                    {
                        break;
                    }
                }
            }
            else
            {
                controlCenter.CheckForJump(new Tuple<int, int>(y + 1, x), this, true);
            }
            controlCenter.CheckForJump(new Tuple<int, int>(y + 1, x + 1), this, false);
            controlCenter.CheckForJump(new Tuple<int, int>(y + 1, x - 1), this, false);
        }
        else
        {
            if (hasMoved == 0)
            {
                for (int i = 1; i < 3; i++)
                {
                    if (controlCenter.CheckForJump(new Tuple<int, int>(y - i, x), this, true) == true)
                    {
                        break;
                    }
                }
            }
            else
            {
                controlCenter.CheckForJump(new Tuple<int, int>(y - 1, x), this, true);
            }
            controlCenter.CheckForJump(new Tuple<int, int>(y - 1, x + 1), this, false);
            controlCenter.CheckForJump(new Tuple<int, int>(y - 1, x - 1), this, false);
        }
    }

    void LMove()
    {
        int y = (currentTile.tilePos.GetLength(0));
        int x = (currentTile.tilePos.GetLength(1));

        controlCenter.CheckForJump(new Tuple<int, int>(y + 2, x + 1), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y + 2, x - 1), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y + 1, x + 2), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y - 1, x + 2), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y - 2, x + 1), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y - 2, x - 1), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y + 1, x - 2), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y - 1, x - 2), this, false);
    }

    void LineMove(int dir)
    {
        int y = (currentTile.tilePos.GetLength(0));
        int x = (currentTile.tilePos.GetLength(1));
        switch (dir)
        {
            // Omni
            case 0:
                //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x + i));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y + i, x + i), this, false) == true)
                    {
                        break;
                    }
                    
                }
                //controlCenter.possibles.Add(new Tuple<int, int>(y, x + i));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y, x + i), this, false) == true)
                    {
                        break;
                    }
                    
                }
                //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x + i));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y - i, x + i), this, false) == true)
                    {
                        break;
                    }
                    
                }
                //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y - i, x), this, false) == true)
                    {
                        break;
                    }

                }
                //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x - i));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y - i, x - i), this, false) == true)
                    {
                        break;
                    }

                }
                //controlCenter.possibles.Add(new Tuple<int, int>(y, x - i));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y, x - i), this, false) == true)
                    {
                        break;
                    }

                }
                //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x - i));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y + i, x - i), this, false) == true)
                    {
                        break;
                    }

                }
                //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y + i, x), this, false) == true)
                    {
                        break;
                    }

                }                                            
                break;

            // Hor / Ver
            case 1:
                //controlCenter.possibles.Add(new Tuple<int, int>(y, x + i));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y, x + i), this, false) == true)
                    {
                        break;
                    }

                }
                //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y - i, x), this, false) == true)
                    {
                        break;
                    }

                }
                //controlCenter.possibles.Add(new Tuple<int, int>(y, x - i));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y, x - i), this, false) == true)
                    {
                        break;
                    }

                }
                //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y + i, x), this, false) == true)
                    {
                        break;
                    }

                }
                break;

            // Diag
            case 2:
                //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x + i));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y + i, x + i), this, false) == true)
                    {
                        break;
                    }

                }
                //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x + i));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y - i, x + i), this, false) == true)
                    {
                        break;
                    }

                }
                //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x - i));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y - i, x - i), this, false) == true)
                    {
                        break;
                    }

                }
                //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x - i));
                for (int i = 1; i < 9; i++)
                {

                    if (controlCenter.CheckForJump(new Tuple<int, int>(y + i, x - i), this, false) == true)
                    {
                        break;
                    }

                }
                break;
        }
    }

    void SurroundMove()
    {

        int y = (currentTile.tilePos.GetLength(0));
        int x = (currentTile.tilePos.GetLength(1));

        controlCenter.CheckForJump(new Tuple<int, int>(y + 1, x + 1), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y, x + 1), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y - 1, x + 1), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y - 1, x), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y - 1, x - 1), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y, x - 1), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y + 1, x - 1), this, false);
        controlCenter.CheckForJump(new Tuple<int, int>(y + 1, x), this, false);
    }

    //public bool CheckCanPlace(Vector2 pos)
    //{
    //    if (currentTile)
    //    return true;
    //}

    //private void OnMouseDown()
    //{
    //    // controlCenter.pieceMoved = this.gameObject;
    //    //controlCenter.movingPiece = true;
    //    if (controlCenter.turn == pieceColor)
    //    {
    //        if (controlCenter.current == null)
    //        {
    //            controlCenter.current = this;
    //            canDrag = true;
    //            GetMovement();
    //            controlCenter.ColorSet(true);
    //            // DisplayMovement(moveTypes);
    //        }
    //        else
    //        {
    //            //CheckCanPlace(transform.position);
    //            if (canDrag && canPlace && controlCenter.current == this)
    //            {
    //                // canDrag = false;
    //                controlCenter.PositionSet(transform.position, this);
    //            }
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

    public void Death()
    {
        if (king)
        {
            controlCenter.ToggleWin(pieceColor);
        }
        else if (abilityType.Contains(6))
        {
            abilities.PassiveAbilityCheck(this);
        }
        // modify side display
        Destroy(gameObject);
    }

    public void PoisonCount()
    {
        poisonTick--;
        if (poisonTick <= 0)
        {
            currentTile.SetPiece(null, true);
        }
    }

    public void FrozenCount()
    {
        frozenTick--;
        if (frozenTick <= 0)
        {
            frozen = false;
            frozenTick = 3;
        }
    }

    public void Lock(Vector2 pos, Tile tile, bool take)
    {
        if (currentTile != null)
        {
            currentTile.SetPiece(null, false);
            if (hasMoved == 0)
            {
                abilities.piece = this;
                abilities.AddAbility();
                hasMoved++;
            }
            if (pieceColor == 1)
            {
                controlCenter.turn = 2;
                controlCenter.Ticker(1);
            }
            else
            {
                controlCenter.turn = 1;
                controlCenter.Ticker(2);
            }
        }
        gameObject.transform.position = pos;
        currentTile = tile;
        currentTile.SetPiece(this, take);
        //if (pieceColor == 1)
        //{
        //    controlCenter.turn = 2;
        //    controlCenter.Ticker(1);
        //}
        //else
        //{
        //    controlCenter.turn = 1;
        //    controlCenter.Ticker(2);
        //}
        if (moveTypes.Contains(5) && currentTile.promotionType == pieceColor)
        {
            controlCenter.promoteCanvas.SetActive(true);
            if (controlCenter.CheckForQueen())
            {
                controlCenter.queenCanvas.SetActive(true);
            }
            currentTile.Promote(pieceColor);
        }
    }

    public void IgnoreLock(Vector2 pos, Tile tile)
    {
        if (currentTile != null)
        {
            if (pieceColor == 1)
            {
                controlCenter.turn = 2;
                controlCenter.Ticker(1);
            }
            else
            {
                controlCenter.turn = 1;
                controlCenter.Ticker(2);
            }
            //    currentTile.SetPiece(null, false);
            //    if (hasMoved == 0)
            //    {
            //        abilities.piece = this;
            //        abilities.AddAbility();
            //        hasMoved++;
            //    }
        }
        gameObject.transform.position = pos;
        currentTile = tile;
        currentTile.SetPiece(this, false);
        //if (pieceColor == 1)
        //{
        //    controlCenter.turn = 2;
        //    controlCenter.Ticker(1);
        //}
        //else
        //{
        //    controlCenter.turn = 1;
        //    controlCenter.Ticker(2);
        //}
        if (moveTypes.Contains(5) && currentTile.promotionType == pieceColor)
        {
            currentTile.Promote(pieceColor);
        }
    }

    public void Return()
    {
        gameObject.transform.position = currentTile.transform.position;
        if (canDrag)
        {
            controlCenter.current = null;
            canDrag = false;
            controlCenter.possibles.Clear();
        }
    }
}
