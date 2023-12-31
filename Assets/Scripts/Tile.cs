using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //ControlCenter controlCenter;
    public bool taken = false;
    public Material shadeMat = null;
    public Material clearMat = null;
    public int promotionType = 0;

    //[HideInInspector] public int tileID = -1;
    public int[,] tilePos = new int[0, 0];
    //[HideInInspector] public int piece = 0;
    [HideInInspector] public Movement currentPiece;
    private ControlCenter controlCenter;
    // Start is called before the first frame update
    void Start()
    {
        // CheckIfTaken();
        controlCenter = GameObject.Find("ControlCenter").GetComponent<ControlCenter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void SwapTaken(bool taken)
    //{
    //    this.taken = taken;
    //}

    //public bool CheckIfTaken()
    //{
    //    if (!taken)
    //    {
    //        return false;
    //    }
    //    else { return true; }
    //}

    public void SetPiece(Movement id, bool take)
    {
        if (take)
        {
            currentPiece.Death();
            //Destroy(currentPiece.gameObject);
        }
        currentPiece = id;
        if (!taken)
        {
            taken = true;
        }
        else if (id == null)
        {
            taken = false;
        }
    }

    public void ColorChange(bool show)
    {
        if (show)
        {
            GetComponent<Renderer>().material = shadeMat;
        }
        else
        {
            GetComponent<Renderer>().material = clearMat;
        }
    }

    private void OnMouseDown()
    {
        // controlCenter.pieceMoved = this.gameObject;
        //controlCenter.movingPiece = true;
        if (currentPiece != null)
        {
            if (controlCenter.turn == currentPiece.pieceColor)
            {
                if (controlCenter.current == null)
                {
                    controlCenter.current = currentPiece;
                    currentPiece.canDrag = true;
                    currentPiece.GetMovement();
                    controlCenter.ColorSet(true);
                    // DisplayMovement(moveTypes);
                }
                //else
                //{
                //    //CheckCanPlace(transform.position);
                //    if (currentPiece.canDrag && currentPiece.canPlace && controlCenter.current == currentPiece)
                //    {
                //        // canDrag = false;
                //        controlCenter.PositionSet(transform.position, currentPiece);
                //    }
                //}
            }
            else if (controlCenter.current != null)
            {
                if (controlCenter.current.canDrag && controlCenter.current.canPlace)
                {
                    //CheckCanPlace(transform.position);

                    // canDrag = false;
                    controlCenter.PositionSet(transform.position, controlCenter.current);
                }

            }
        }
        else if (controlCenter.current != null)
        {
            //CheckCanPlace(transform.position);
            if (controlCenter.current.canDrag && controlCenter.current.canPlace)
            {
                // canDrag = false;
                controlCenter.PositionSet(transform.position, controlCenter.current);
            }
        }
    }
}
