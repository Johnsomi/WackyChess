using JetBrains.Annotations;
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
    int promotionNumber = 0;
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
        //if (Input.GetMouseButtonDown(1) && controlCenter.current == null) // right click
        //{
        //    if (currentPiece != null)
        //    {
        //        currentPiece.abilities.piece = currentPiece;
        //        currentPiece.abilities.ShowAbilities();
        //    }
        //}

        //if (Input.GetMouseButtonDown(1) && controlCenter.current == null)
        //{
        //    if (currentPiece != null)
        //    {
        //        Vector3 mousePosition = Input.mousePosition;
        //        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        //        if (Physics.Raycast(ray, out RaycastHit hit))
        //        {
        //            // Use the hit variable to determine what was clicked on.
        //            if (hit.collider != null)
        //            {
        //                Debug.Log(hit.collider);
        //            }
        //        }
        //    }
        //}
    }

    public void TryDisplay()
    {
        if (currentPiece != null && currentPiece.pieceColor == controlCenter.turn)
        {
            currentPiece.abilities.piece = currentPiece;
            currentPiece.abilities.ShowAbilities();
        }
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
        if (id == null)
        {
            taken = false;
        }
        else if (!taken)
        {
            taken = true;
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

    public void Promote(int color)
    {
        StartCoroutine(WaitPromote(color));
        //List<int> abilities = new List<int>();
        //abilities = currentPiece.abilityType;
        //SetPiece(null, true);
        //if (color == 1)
        //{
        //    Instantiate(controlCenter.WPieces[promotionNumber], transform.position, Quaternion.identity);
        //}
        //else
        //{
        //    Instantiate(controlCenter.BPieces[promotionNumber], transform.position, Quaternion.identity);
        //}
        //StartCoroutine(WaitAbility(abilities));
    }

    IEnumerator WaitAbility(List<int> abilities)
    {
        yield return new WaitUntil(() => currentPiece != null);

        currentPiece.abilityType = abilities;
        promotionNumber = 0;
        if (controlCenter.queenCanvas.activeInHierarchy)
        {
            controlCenter.queenCanvas.SetActive(false);
        }
        controlCenter.promoteCanvas.SetActive(false);
    }

    IEnumerator WaitPromote(int color)
    {
        yield return new WaitUntil(() => promotionNumber != 0);

        BeginPromotion(color);
    }

    void BeginPromotion(int color)
    {
        List<int> abilities = new List<int>();
        abilities = currentPiece.abilityType;
        SetPiece(null, true);
        if (color == 1)
        {
            Instantiate(controlCenter.WPieces[promotionNumber], transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(controlCenter.BPieces[promotionNumber], transform.position, Quaternion.identity);
        }
        StartCoroutine(WaitAbility(abilities));
    }

    public void SetPromote(int piece)
    {
        promotionNumber = piece;
    }

    private void OnMouseDown()
    {
        // controlCenter.pieceMoved = this.gameObject;
        //controlCenter.movingPiece = true;
        if (currentPiece != null)
        {
            if (!currentPiece.frozen)
            {
                if (controlCenter.abilities.piece != null)
                {
                    if (controlCenter.abilities.piece.canTarget)
                    {
                        //controlCenter.TargetSet(controlCenter.abilities.piece, transform.position, controlCenter.abilities.piece.usedAbility);
                        controlCenter.abilities.UseAbility(controlCenter.abilities.piece.usedAbility, this);
                    }
                }
                else if (controlCenter.turn == currentPiece.pieceColor)
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
                        controlCenter.PositionSet(transform.position, controlCenter.current, false);
                    }

                }
            }
        }
        else if (controlCenter.current != null)
        {
            //CheckCanPlace(transform.position);
            if (controlCenter.current.canDrag && controlCenter.current.canPlace)
            {
                // canDrag = false;
                controlCenter.PositionSet(transform.position, controlCenter.current, false);
            }
        }
        else if (controlCenter.abilities.piece != null)
        {
            if (controlCenter.abilities.piece.canTarget)
            {
                //controlCenter.TargetSet(controlCenter.abilities.piece, transform.position, controlCenter.abilities.piece.usedAbility);
                controlCenter.abilities.UseAbility(controlCenter.abilities.piece.usedAbility, this);
            }
        }
    }
}
