using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class Abilities : MonoBehaviour
{
    [SerializeField] ControlCenter controlCenter;
    public Movement piece;
    public int abilities = 21;
    public Button buttonOne;
    public Button buttonTwo;
    public Button buttonClose;
    private enum AbilityTypes
    {
        Dig = 0,
        Shoot = 1,
        Warp = 2,
        Dash = 3,
        Swap = 4,
        SelfDestruct = 5,
        Poisonous = 6,
        Hideaway = 7,
        Freeze = 8,
        Conqueror = 9,
        Devolve = 10,
        Draft = 11,
        Goliath = 12,
        Immunity = 13,
        Order66 = 14,
        Oath = 15, // Oath to the crown
        Henry = 16, // Off with her head
        FastPromote = 17,
        Empower = 18,
        SelfSacrifice = 19,
        Guardian = 20,
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(1)) // right click
    //    {
            
    //    }
    //}

    public void AddAbility()
    {
        int count = piece.abilityType.Count;
        if (count == 0)
        {

            //int random = UnityEngine.Random.Range(0, abilities);
            int random = 6;
            piece.abilityType.Add(random);
        }
        else if (count == 1)
        {
            bool t = true;
            while (t)
            {
                int currentAbility = piece.abilityType[0];
                int random = UnityEngine.Random.Range(0, abilities);
                if (currentAbility != random)
                {
                    piece.abilityType.Add(random);
                    t = false;
                }
            }
        }
        piece = null;
    }

    public void ShowAbilities()
    {
        buttonClose.gameObject.SetActive(true);
        if (piece.abilityType.Count == 2)
        {
            buttonOne.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = DisplayAbilityName(piece.abilityType[0]);
            buttonTwo.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = DisplayAbilityName(piece.abilityType[1]);
            buttonOne.gameObject.SetActive(true);
            buttonTwo.gameObject.SetActive(true);
        }
        else if (piece.abilityType.Count == 1)
        {
            buttonOne.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = DisplayAbilityName(piece.abilityType[0]);
            buttonOne.gameObject.SetActive(true);
        }
        //else
        //{
        //    buttonClose.gameObject.SetActive(false);
        //    piece = null;
        //}
    } 

    public void ActivateAbilityOne()
    {
        int ability = piece.abilityType[0];
        AbilityAction(ability);
        piece.canTarget = true;
        //if (piece.pieceColor == 1)
        //{
        //    controlCenter.turn++;
        //}
        //else
        //{
        //    controlCenter.turn--;
        //}
        CloseAbilities();
    }

    public void ActivateAbilityTwo()
    {
        int ability = piece.abilityType[1];
        AbilityAction(ability);
        piece.canTarget = true;
        //if (piece.pieceColor == 1)
        //{
        //    controlCenter.turn++;
        //}
        //else
        //{
        //    controlCenter.turn--;
        //}
        CloseAbilities();
    }

    public void CloseAbilities()
    {
        buttonOne.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
        buttonTwo.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
        buttonOne.gameObject.SetActive(false);
        buttonTwo.gameObject.SetActive(false);
        buttonClose.gameObject.SetActive(false);
        if (!piece.canTarget)
        {
            piece = null;
        }
    }

    string DisplayAbilityName(int ability)
    {
        switch (ability)
        {
            case 0:
                return "Dig";

            case 1:
                return "Shoot";

            case 2:
                return "Warp";

            case 3:
                return "Dash";

            case 4:
                return "Swap";

            case 5:
                return "Self Destruct";

            case 6:
                return "Poisonous";

            case 7:
                return "Hideaway";

            case 8:
                return "Freeze";

            case 9:
                return "Conqueror";

            case 10:
                return "Devolve";

            case 11:
                return "Draft";

            case 12:
                return "Goliath";

            case 13:
                return "Immunity";

            case 14:
                return "Order 66";

            case 15:
                return "Oath to the king";

            case 16:
                return "Off with her head";

            case 17:
                return "Quick Promotion";

            case 18:
                return "Empower";

            case 19:
                return "Self Sacrifice";

            case 20:
                return "Guardian";
        }
        return null;
    }

    void AbilityAction(int ability)
    {
        switch (ability)
        {
            case 0:
                //return "Dig";
                break;

            case 1:
                //return "Shoot";
                piece.usedAbility = 1;
                Shoot();
                break;

            case 2:
                //return "Warp";
                break;

            case 3:
                //return "Dash";
                break;

            case 4:
                //return "Swap";
                piece.usedAbility = 4;
                FriendCheck();
                break;

            case 5:
                //return "Self Destruct";
                break;

            case 6:
                //return "Poisonous";
                //piece.usedAbility = 6;
                //Shoot();
                break;

            case 7:
                //return "Hideaway";
                break;

            case 8:
                //return "Freeze";
                piece.usedAbility = 8;
                Shoot();
                break;

            case 9:
                //return "Conqueror";
                break;

            case 10:
                break;
                //return "Devolve";

            case 11:
                break;
            //return "Draft";

            case 12:
                // return "Goliath";
                break;

            case 13:
                //return "Immunity";
                break;

            case 14:
                // return "Order 66";
                break;

            case 15:
                //return "Oath to the king";
                break;

            case 16:
                //return "Off with her head";
                break;

            case 17:
                // return "Quick Promotion";
                break;

            case 18:
                //return "Empower";
                break;

            case 19:
                //return "Self Sacrifice";
                break;

            case 20:
                //return "Guardian";
                break;
        }
    }

    public void UseAbility(int ability, Tile Target)
    {
        if (ability == 1)
        {
            controlCenter.TargetSet(piece, Target.transform.position, ability);
        }
        else if (ability == 4)
        {
            controlCenter.TargetSet(piece, Target.transform.position, ability);
        }
    }

    void Shoot()
    {
        int y = (piece.currentTile.tilePos.GetLength(0));
        int x = (piece.currentTile.tilePos.GetLength(1));

        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x + i), piece, false) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y, x + i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y, x + i), piece, false) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x + i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x + i), piece, false) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x), piece, false) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x - i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x - i), piece, false) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y, x - i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y, x - i), piece, false) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x - i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x - i), piece, false) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x), piece, false) == true)
            {
                break;
            }
        }
        controlCenter.ColorSet(true);
    }

    void FriendCheck()
    {
        controlCenter.CheckAll(piece, 1);
        controlCenter.ColorSet(true);
    }

    public void PassiveAbilityCheck(Movement attacked)
    {
        var cur = controlCenter.current;
        var abi = piece;
        if (attacked.abilityType.Contains(6))
        {
            if (cur != null)
            {
                cur.poison = true;
            }
            else if (abi != null)
            {
                abi.poison = true;
            }
            
        }
    }

    /*void FriendlyInRange()
    {
        int y = (piece.currentTile.tilePos.GetLength(0));
        int x = (piece.currentTile.tilePos.GetLength(1));

        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x + i), piece, true) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y, x + i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y, x + i), piece, true) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x + i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x + i), piece, true) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x), piece, true) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x - i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x - i), piece, true) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y, x - i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y, x - i), piece, true) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x - i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x - i), piece, true) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x), piece, true) == true)
            {
                break;
            }
        }
        controlCenter.ColorSet(true);
    }*/

    public void Swap(Tile tile)
    {
        var temp = tile.currentPiece;
        var swapper = piece.currentTile;
        controlCenter.PositionSet(temp.transform.position, piece, true);
        controlCenter.PositionSet(swapper.transform.position, temp, true);
        //tile.SetPiece(piece, false);
        //swapper.SetPiece(temp, false);
        EndAbility();
    }

    public void FireShot(Tile tile)
    {
        tile.SetPiece(null, true);
        EndAbility();
    }

    void EndAbility()
    {
        piece.canTarget = false;
        controlCenter.possibles.Clear();
        controlCenter.ColorSet(false);
        if (piece.pieceColor == controlCenter.turn)
        {
            if (piece.pieceColor == 1)
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
        piece = null;
    }
}
