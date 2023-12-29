using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCenter : MonoBehaviour
{
    public Movement current;
    public Transform tileParent;
    [SerializeField] private List<BoxCollider> tiles = new List<BoxCollider>();
    bool canPlace = true;
    public List<Tuple<int, int>> possibles = new List<Tuple<int, int>>();
    // Start is called before the first frame update
    void Awake()
    {
        //for (int i = 0; i < tileParent.childCount; i++)
        //{
        //    tiles.Add(tileParent.GetChild(i).GetComponent<BoxCollider>());
        //    tileParent.GetChild(i).GetComponent<Tile>().tileID = i;
            
        //}
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

    public void PositionSet(Vector2 pos, Movement piece)
    {
        for (int i = 0; i < tiles.Count; i++) 
        {
            if (tiles[i].bounds.Contains(pos))
            {
                if (current)
                {
                    Check(piece, pos, tiles[i].GetComponent<Tile>());
                }
                else { canPlace = true; }
                if (canPlace)
                {
                    if (!tiles[i].GetComponent<Tile>().taken)
                    {
                        piece.canDrag = false;
                        //tiles[i].GetComponent<Tile>().SetPiece(piece, false);
                        piece.Lock(tiles[i].gameObject.transform.position, tiles[i].GetComponent<Tile>(), false);
                        //current = null;
                    }
                    else if (tiles[i].GetComponent<Tile>().currentPiece.pieceColor != piece.pieceColor)
                    {
                        piece.canDrag = false;
                        // tiles[i].GetComponent<Tile>().SetPiece(piece, true);
                        piece.Lock(tiles[i].gameObject.transform.position, tiles[i].GetComponent<Tile>(), true);
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
        piece.GetMovement();
        // tile.tilePos
        //for (int i = 0; i < possibles.Count; i++)
        //{
            if (possibles.Contains(new Tuple<int, int>(tile.tilePos.GetLength(0), tile.tilePos.GetLength(1))))
            {
                possibles.Clear();
                canPlace = true;
            }
            //Debug.Log(possibles.Contains(new Tuple<int, int>(tile.tilePos.GetLength(0), tile.tilePos.GetLength(1))));
        //}
    }
}
