using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //ControlCenter controlCenter;
    [HideInInspector] public bool taken = false;
    [HideInInspector] public int tileID = -1;
    string currentPiece = "";
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

    public void SetPiece(string id)
    {
        currentPiece = id;
        if (!taken)
        {
            taken = true;
        }
        else if (id == "")
        {
            taken = false;
        }
    }
}
