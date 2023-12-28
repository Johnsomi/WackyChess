using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //ControlCenter controlCenter;
    bool taken = false;
    [HideInInspector] public int tileID = -1;
    int currentPiece = -1;
    // Start is called before the first frame update
    void Start()
    {
       // CheckIfTaken();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapTaken(bool taken)
    {
        this.taken = taken;
    }

    public bool CheckIfTaken()
    {
        if (!taken)
        {
            SwapTaken(true);
            return false;
        }
        else { return true; }
    }

    public void SetPiece(int id)
    {

    }
}
