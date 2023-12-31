using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
       // CheckIfTaken();
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
            Destroy(currentPiece.gameObject);
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
}
