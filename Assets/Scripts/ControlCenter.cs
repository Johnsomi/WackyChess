using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ControlCenter : MonoBehaviour
{
    public Movement current;
    public Transform tileParent;
    [SerializeField] private List<BoxCollider> tiles = new List<BoxCollider>();
    bool canPlace = true;
    public List<Tuple<int, int>> possibles = new List<Tuple<int, int>>();
    public List<Tile> possibleMoves = new List<Tile>();
    public Canvas canvas;
    private GameObject win;
    [HideInInspector] public int turn = 1;


    public Abilities abilities;
    bool canTarget = false;
    // Start is called before the first frame update
    void Awake()
    {
        //for (int i = 0; i < tileParent.childCount; i++)
        //{
        //    tiles.Add(tileParent.GetChild(i).GetComponent<BoxCollider>());
        //    tileParent.GetChild(i).GetComponent<Tile>().tileID = i;

        //}
        win = canvas.transform.Find("Win").gameObject;
        win.SetActive(false);
        int x = 0;
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                tiles.Add(tileParent.GetChild(x).GetComponent<BoxCollider>());
                tileParent.GetChild(x).GetComponent<Tile>().tilePos = new int[i, j];
                x++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ColorSet(bool color)
    {
        if (color)
        {
            // List<Tile> tempTiles = new List<Tile>();
            for (int i = 0; i < possibles.Count; i++)
            {

                var temp1 = possibles[i].Item1;
                var temp2 = possibles[i].Item2;

                for (int j = 0; j < tiles.Count; j++)
                {
                    if (tiles[j].GetComponent<Tile>().tilePos.GetLength(0) == temp1 && tiles[j].GetComponent<Tile>().tilePos.GetLength(1) == temp2)
                    {

                        possibleMoves.Add(tiles[j].GetComponent<Tile>());
                    }
                }
            }
            for (int i = 0; i < possibleMoves.Count; i++)
            {
                possibleMoves[i].ColorChange(true);
            }
        }
        else
        {
            for (int k = 0; k < possibleMoves.Count; k++)
            {
                possibleMoves[k].ColorChange(false);
            }
            possibleMoves.Clear();
        }
        
    }

    public void TargetSet(Movement piece, Vector2 pos, int ability)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].bounds.Contains(pos))
            {
                ColorSet(false);
                if (abilities.piece)
                {
                    Check(piece, pos, tiles[i].GetComponent<Tile>());
                }
                else { canTarget = true; }
                if (canTarget)
                {
                    if (abilities.piece.usedAbility == 1)
                    {
                        //tiles[i].GetComponent<Tile>().SetPiece(piece, false);
                        piece.abilities.FireShot(tiles[i].GetComponent<Tile>());
                        //current = null;
                    }
                    
                    if (current != null)
                    {
                        current = null;
                    }
                    canTarget = false;
                    canPlace = false;
                    break;
                }
            }
        }
    }

    public void PositionSet(Vector2 pos, Movement piece)
    {
       // CheckForJumps(piece);
        for (int i = 0; i < tiles.Count; i++) 
        {
            if (tiles[i].bounds.Contains(pos))
            {
                ColorSet(false);
                if (current)
                {
                    Check(piece, pos, tiles[i].GetComponent<Tile>());
                }
                else { canPlace = true; }
                if (canPlace)
                {
                    int Ptemp = 0;
                    if (piece.moveTypes.Contains(5) && current)
                    {
                        if (tiles[i].GetComponent<Tile>().tilePos.GetLength(1) == piece.currentTile.tilePos.GetLength(1))
                        {
                            Ptemp = 1;
                        }
                        else if ((tiles[i].GetComponent<Tile>().tilePos.GetLength(0) != piece.currentTile.tilePos.GetLength(0)))
                        {
                            Ptemp = 2;
                        }
                    }

                    if (!tiles[i].GetComponent<Tile>().taken && Ptemp != 2)
                    {
                        piece.canDrag = false;
                        //tiles[i].GetComponent<Tile>().SetPiece(piece, false);
                        piece.Lock(tiles[i].gameObject.transform.position, tiles[i].GetComponent<Tile>(), false);
                        //current = null;
                    }
                    else if (tiles[i].GetComponent<Tile>().currentPiece != null && Ptemp != 1) 
                    {
                        if (tiles[i].GetComponent<Tile>().currentPiece.pieceColor != piece.pieceColor)
                        {
                            piece.canDrag = false;
                            // tiles[i].GetComponent<Tile>().SetPiece(piece, true);
                            piece.Lock(tiles[i].gameObject.transform.position, tiles[i].GetComponent<Tile>(), true);
                        }
                    }
                    else
                    {
                        piece.canDrag = false;
                        piece.Return();
                        // current = null;
                    }
                    if (current != null)
                    {
                        current = null;
                    }
                    canPlace = false;
                    canTarget = false;
                    break;
                }
            }
        }
    }

    public void Check(Movement piece, Vector2 pos, Tile tile)
    {
        //for(int i = 0; i < piece.moveTypes.Count; i++)
        //{

        //}
        //piece.GetMovement();
        // tile.tilePos
        //for (int i = 0; i < possibles.Count; i++)
        //{
        if (possibles.Contains(new Tuple<int, int>(tile.tilePos.GetLength(0), tile.tilePos.GetLength(1))))
        {
            canPlace = true;
            canTarget = true;
        }
        possibles.Clear();
        if (!canPlace)
        {
            piece.canDrag = false;
            piece.Return();
            // current = null;
        
            if (current != null)
            {
                current = null;
            }
        }
        else if (!canTarget)
        {
            piece.Return();
            // current = null;

            if (current != null)
            {
                current = null;
            }
        }
        //Debug.Log(possibles.Contains(new Tuple<int, int>(tile.tilePos.GetLength(0), tile.tilePos.GetLength(1))));
        //}
    }

    public bool CheckForJump(Tuple<int, int> tuple, Movement piece, bool FalseAttack)
    {
        //bool digAttack = false;
        //if (piece.abilityType.Contains(0))
        //{
        //    digAttack = true;
        //}
        var temp1 = tuple.Item1;
        var temp2 = tuple.Item2;

        for (int j = 0; j < tiles.Count; j++)
        {
            var tile = tiles[j].GetComponent<Tile>();

            if (tile.tilePos.GetLength(0) == temp1 && tile.tilePos.GetLength(1) == temp2)
            {
                if (tile.taken)
                {
                    if (tile.currentPiece.pieceColor != piece.pieceColor && (!FalseAttack))// || digAttack))
                    {
                        possibles.Add(tuple);
                        
                    }
                    // Stop search in that direction
                    return true;
                    //possibles.Remove(tuple);
                }
                else if (piece.moveTypes.Contains(5) && !FalseAttack)
                {
                    return false;
                }
                else
                {
                    possibles.Add(tuple);
                    return false;
                }
            }
                       
        }
        return false;
    }

    public void ToggleWin(int color)
    {
        win.SetActive(true);
        if (color == 1)
        {
            win.transform.Find("WhiteWin").gameObject.SetActive(false);
        }
        else
        {
            win.transform.Find("BlackWin").gameObject.SetActive(false);
        }
    }

    public bool CheckForTarget(Tuple<int, int> tuple, Movement piece, bool friendly)
    {
        //bool digAttack = false;
        //if (piece.abilityType.Contains(0))
        //{
        //    digAttack = true;
        //}
        var temp1 = tuple.Item1;
        var temp2 = tuple.Item2;

        for (int j = 0; j < tiles.Count; j++)
        {
            var tile = tiles[j].GetComponent<Tile>();

            if (tile.tilePos.GetLength(0) == temp1 && tile.tilePos.GetLength(1) == temp2)
            {
                if (tile.taken)
                {
                    if (tile.currentPiece.pieceColor != piece.pieceColor && !friendly)// || digAttack))
                    {
                        possibles.Add(tuple);
                    }
                    else if (tile.currentPiece.pieceColor == piece.pieceColor && friendly)
                    {
                        possibles.Add(tuple);
                    }
                        // Stop search in that direction
                    return true;
                    //possibles.Remove(tuple);
                }
                else
                {
                    return false;
                }
            }

        }
        return false;
    }



}
