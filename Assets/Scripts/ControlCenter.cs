using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCenter : MonoBehaviour
{
    public Movement current;
    public Transform tileParent;
    [SerializeField] private List<BoxCollider> tiles = new List<BoxCollider>(); 
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < tileParent.childCount; i++)
        {
            tiles.Add(tileParent.GetChild(i).GetComponent<BoxCollider>());
            tileParent.GetChild(i).GetComponent<Tile>().tileID = i;
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
                if (!tiles[i].GetComponent<Tile>().taken)
                {
                    piece.canDrag = false;
                    piece.Lock(tiles[i].gameObject.transform.position, tiles[i].GetComponent<Tile>());
                    //current = null;
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
                break;
            }
        }
    }
}
