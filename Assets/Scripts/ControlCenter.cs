using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [HideInInspector] public GameObject promoteCanvas;
    [HideInInspector] public GameObject queenCanvas;
    [HideInInspector] public int turn = 1;
    public List<GameObject> WPieces = new List<GameObject>();
    public List<GameObject> BPieces = new List<GameObject>();


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
        promoteCanvas = canvas.transform.Find("Promote").gameObject;
        queenCanvas = promoteCanvas.transform.Find("Queen").gameObject;
        queenCanvas.SetActive(false);
        promoteCanvas.SetActive(false);
        
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

    public bool CheckForQueen()
    {
        for (int j = 0; j < tiles.Count; j++)
        {
            var tile = tiles[j].GetComponent<Tile>();
            if (tile.taken)
            {
                if (current != null)
                {
                    if (tile.currentPiece.pieceColor == current.pieceColor)
                    {
                        if (tile.currentPiece.moveTypes.Contains(1))
                        {
                            return false;
                        }
                    }
                }
                else if (abilities.piece != null)
                {
                    if (tile.currentPiece.pieceColor == abilities.piece.pieceColor)
                    {
                        if (tile.currentPiece.moveTypes.Contains(1))
                        {
                            return false;
                        }
                    }                   
                }
            }
        }
        return true;
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
                    int ua = abilities.piece.usedAbility;
                    if (ua == 1)
                    {
                        //tiles[i].GetComponent<Tile>().SetPiece(piece, false);
                        piece.abilities.FireShot(tiles[i].GetComponent<Tile>());
                        //current = null;
                    }
                    else if (ua == 0 || ua == 2)
                    {
                        piece.abilities.MoveTo(tiles[i].GetComponent<Tile>());
                    }
                    else if (ua == 4 || ua == 19)
                    {
                        piece.abilities.Swap(tiles[i].GetComponent<Tile>());
                    }
                    else if (ua == 8)
                    {
                        piece.abilities.Freeze(tiles[i].GetComponent<Tile>());
                    }
                    else if (ua == 11)
                    {
                        piece.abilities.CreatePiece(tiles[i].GetComponent<Tile>(), 5, piece.pieceColor);
                    }
                    else if (ua == 10)
                    {
                        piece.abilities.DevolvePiece(tiles[i].GetComponent<Tile>(), piece.pieceColor);
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

    public void EffectAllInRange(int size)
    {
        int x = 8;
        if (size == 2)
        {
            x = 24;
        }
        for (int i = 0; i < tiles.Count; i++)
        {
            if (possibles.Contains(new Tuple<int, int>(tiles[i].GetComponent<Tile>().tilePos.GetLength(0), tiles[i].GetComponent<Tile>().tilePos.GetLength(1))))
            {
                if (abilities.piece.usedAbility == 5)
                {
                    abilities.piece.abilities.Kill(tiles[i].GetComponent<Tile>());
                }
                x--;
                if (x == 0) { break; }
            }
        }
        abilities.piece.abilities.CallEndAbility();
    }

    public void PositionSet(Vector2 pos, Movement piece, bool ignore)
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
                    if (!ignore)
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
                        StartCoroutine(WaitForPromote());
                        break;
                    }
                    else
                    {
                        piece.canDrag = false;
                        piece.IgnoreLock(tiles[i].gameObject.transform.position, tiles[i].GetComponent<Tile>());
                        StartCoroutine(WaitForPromote());
                        break;
                    }
                }
            }
        }
    }

    IEnumerator WaitForPromote()
    {
        yield return new WaitUntil(() => promoteCanvas.activeInHierarchy == false);

        if (current != null)
        {
            current = null;
        }
        canPlace = false;
        canTarget = false;
    }

    void Check(Movement piece, Vector2 pos, Tile tile)
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

    public void Ticker(int color)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            var temp = tiles[i].GetComponent<Tile>().currentPiece;
            if (temp != null)
            {
                if (temp.pieceColor == color)
                {
                    if (temp.poison == true)
                    {
                        temp.PoisonCount();
                    }
                    if (temp.frozen == true)
                    {
                        temp.FrozenCount();
                    }
                }
            }
        }
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

    public void CheckAll(Movement piece, int type)
    {
        // 0 = all empty
        // 1 = all friends
        // 2 = all enemies
        int uaV = 100;
        uaV = piece.usedAbility;
        for (int j = 0; j < tiles.Count; j++)
        {
            var tile = tiles[j].GetComponent<Tile>();
            if (type != 0)
            {
                if (tile.taken)
                {
                    if (type == 1 && tile.currentPiece.pieceColor == piece.pieceColor)
                    {                        
                        var pos = new Tuple<int, int>(tile.tilePos.GetLength(0), tile.tilePos.GetLength(1));
                        possibles.Add(pos);
                    }
                    else if (type == 2 && tile.currentPiece.pieceColor != piece.pieceColor)
                    {
                        var pos = new Tuple<int, int>(tile.tilePos.GetLength(0), tile.tilePos.GetLength(1));
                        if (uaV == 0)
                        {
                            if (tile.currentPiece.moveTypes.Contains(5))
                            {
                                possibles.Add(pos);
                            }
                        }
                        else if (uaV == 19)
                        {
                            if (tile.currentPiece.moveTypes.Contains(piece.moveTypes[0]))
                            {
                                possibles.Add(pos);
                            }
                        }
                        else
                        {
                            possibles.Add(pos);
                        }
                    }
                    // Stop search in that direction
                    //possibles.Remove(tuple);
                }
            }
            else
            {
                if (!tile.taken)
                {
                    var pos = new Tuple<int, int>(tile.tilePos.GetLength(0), tile.tilePos.GetLength(1));
                    possibles.Add(pos);
                    
                    // Stop search in that direction
                    //possibles.Remove(tuple);
                }
            }
        }
    }

    public bool CheckForTarget(Tuple<int, int> tuple, Movement piece, int effect)
    {
        // effect
        // 0 = all
        // 1 = friendy
        // 2 = enemy
        // 3 = none
        //bool digAttack = false;
        //if (piece.abilityType.Contains(0))
        //{
        //    digAttack = true;
        //}
        var temp1 = tuple.Item1;
        var temp2 = tuple.Item2;
        int uaV = 100;
        uaV = piece.usedAbility;

        for (int j = 0; j < tiles.Count; j++)
        {
            var tile = tiles[j].GetComponent<Tile>();

            if (tile.tilePos.GetLength(0) == temp1 && tile.tilePos.GetLength(1) == temp2)
            {
                if (tile.taken && effect != 3)
                {
                    if (effect == 0)
                    {
                        possibles.Add(tuple);
                    }
                    else if (tile.currentPiece.pieceColor != piece.pieceColor && effect != 1)// || digAttack))
                    {
                        if (uaV == 10)
                        {
                            if (!tile.currentPiece.moveTypes.Contains(5) && !tile.currentPiece.moveTypes.Contains(0)) 
                            {
                                possibles.Add(tuple);
                            }
                        } 
                        else 
                        {
                            possibles.Add(tuple);
                        }
                    }
                    else if (tile.currentPiece.pieceColor == piece.pieceColor && effect != 2)
                    {
                        possibles.Add(tuple);
                    }
                        // Stop search in that direction
                    return true;
                    //possibles.Remove(tuple);
                }
                else if (effect == 3 && !tile.taken)
                {
                    possibles.Add(tuple);
                    return true;
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
