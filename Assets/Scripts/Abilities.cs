using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
        SneakAttack = 0, //
        Shoot = 1, //
        Warp = 2, //
        Dash = 3,
        Swap = 4, //
        SelfDestruct = 5, //
        Poisonous = 6, //
        Hideaway = 7,
        Freeze = 8, //
        Conqueror = 9,
        Devolve = 10, //
        Draft = 11, //
        Goliath = 12,
        Immunity = 13,
        Order66 = 14,
        Oath = 15, // Oath to the crown
        Henry = 16, // Off with her head
        FormShift = 17,
        Empower = 18,
        Infiltrate = 19, //
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
            int random = 17;
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
        if (piece != null)
        {
            piece.canTarget = true;
        }
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
        if (piece != null)
        {
            piece.canTarget = true;
        }
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
        if (piece != null)
        {
            if (!piece.canTarget)
            {
                piece = null;
            }
        }
    }

    string DisplayAbilityName(int ability)
    {
        switch (ability)
        {
            case 0:
                return "Sneak Attack";

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
                return "Form Shift";

            case 18:
                return "Empower";

            case 19:
                return "Infiltrate";

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
                //return "Sneak Attack";
                piece.usedAbility = 0;
                EnemyCheck();
                break;

            case 1:
                //return "Shoot";
                piece.usedAbility = 1;
                Shoot(2);
                break;

            case 2:
                //return "Warp";
                piece.usedAbility = 2;
                EmptyCheck();
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
                piece.usedAbility = 5;
                OmniShot(2, 0);
                UseAbility(5, null);
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
                Shoot(2);
                break;

            case 9:
                //return "Conqueror";
                break;

            case 10:
                piece.usedAbility = 10;
                OmniShot(1, 2);
                break;
            //return "Devolve";

            case 11:
                piece.usedAbility = 11;
                LineShot(false, 0, 3);
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
                // return "Form Shift";
                piece.usedAbility = 17;
                UseAbility(17, null);
                break;

            case 18:
                //return "Empower";
                break;

            case 19:
                //return "Infiltrate";
                piece.usedAbility = 19;
                EnemyCheck();
                break;

            case 20:
                //return "Guardian";
                break;
        }
    }

    public void UseAbility(int ability, Tile Target)
    {
        if (ability != 5 && ability != 17)
        {
            controlCenter.TargetSet(piece, Target.transform.position, ability);
        }
        else
        {
            if (ability == 5)
            {
                controlCenter.EffectAllInRange(2);
            }
            else if (ability == 17)
            {
                FormShift();
            }
        }
        //else if (ability == 4)
        //{
        //    controlCenter.TargetSet(piece, Target.transform.position, ability);
        //}
    }

    void Shoot(int attack)
    {
        int y = (piece.currentTile.tilePos.GetLength(0));
        int x = (piece.currentTile.tilePos.GetLength(1));

        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x + i), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y, x + i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y, x + i), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x + i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x + i), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x - i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x - i), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y, x - i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y, x - i), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x - i));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x - i), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x));
        for (int i = 1; i < 9; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x), piece, attack) == true)
            {
                break;
            }
        }
        controlCenter.ColorSet(true);
    }

    void OmniShot(int distance, int attack)
    {
        int y = (piece.currentTile.tilePos.GetLength(0));
        int x = (piece.currentTile.tilePos.GetLength(1));

        for (int i = 1; i <= distance; i++)
        {
            controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x + i), piece, attack);
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y, x + i));
        for (int i = 1; i <= distance; i++)
        {
            controlCenter.CheckForTarget(new Tuple<int, int>(y, x + i), piece, attack);
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x + i));
        for (int i = 1; i <= distance; i++)
        {
            controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x + i), piece, attack);
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x));
        for (int i = 1; i <= distance; i++)
        {
            controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x), piece, attack);
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x - i));
        for (int i = 1; i <= distance; i++)
        {
            controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x - i), piece, attack);
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y, x - i));
        for (int i = 1; i <= distance; i++)
        {
            controlCenter.CheckForTarget(new Tuple<int, int>(y, x - i), piece, attack);
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x - i));
        for (int i = 1; i <= distance; i++)
        {
            controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x - i), piece, attack);
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x));
        for (int i = 1; i <= distance; i++)
        {
            controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x), piece, attack);
        }

        if (distance > 1)
        {
            controlCenter.CheckForTarget(new Tuple<int, int>(y + 2, x + 1), piece, attack);
            controlCenter.CheckForTarget(new Tuple<int, int>(y + 2, x - 1), piece, attack);
            controlCenter.CheckForTarget(new Tuple<int, int>(y + 1, x + 2), piece, attack);
            controlCenter.CheckForTarget(new Tuple<int, int>(y - 1, x + 2), piece, attack);
            controlCenter.CheckForTarget(new Tuple<int, int>(y - 2, x + 1), piece, attack);
            controlCenter.CheckForTarget(new Tuple<int, int>(y - 2, x - 1), piece, attack);
            controlCenter.CheckForTarget(new Tuple<int, int>(y + 1, x - 2), piece, attack);
            controlCenter.CheckForTarget(new Tuple<int, int>(y - 1, x - 2), piece, attack);
        }
        controlCenter.ColorSet(true);
    }

    void RangedShoot(int distance, int attack)
    {
        int y = (piece.currentTile.tilePos.GetLength(0));
        int x = (piece.currentTile.tilePos.GetLength(1));
        for (int i = 1; i <= distance; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x + i), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y, x + i));
        for (int i = 1; i <= distance; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y, x + i), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x + i));
        for (int i = 1; i <= distance; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x + i), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x));
        for (int i = 1; i <= distance; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y - i, x - i));
        for (int i = 1; i <= distance; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y - i, x - i), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y, x - i));
        for (int i = 1; i <= distance; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y, x - i), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x - i));
        for (int i = 1; i <= distance; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x - i), piece, attack) == true)
            {
                break;
            }
        }
        //controlCenter.possibles.Add(new Tuple<int, int>(y + i, x));
        for (int i = 1; i <= distance; i++)
        {
            if (controlCenter.CheckForTarget(new Tuple<int, int>(y + i, x), piece, attack) == true)
            {
                break;
            }
        }
        
       
        controlCenter.ColorSet(true);
    }

    void LineShot(bool setLine, int line, int effect)
    {
        int y = (piece.currentTile.tilePos.GetLength(0));
        int x = (piece.currentTile.tilePos.GetLength(1));
        if (!setLine)
        {
            for (int i = 1; i <= 8; i++)
            {
                controlCenter.CheckForTarget(new Tuple<int, int>(y, i), piece, effect);
            }
        }
        else
        {
            for (int i = 1; i <= 8; i++)
            {
                controlCenter.CheckForTarget(new Tuple<int, int>(line, i), piece, effect);
            }
        }
        controlCenter.ColorSet(true);
    }

    void FriendCheck()
    {
        controlCenter.CheckAll(piece, 1);
        controlCenter.ColorSet(true);
    }

    void EnemyCheck()
    {
        controlCenter.CheckAll(piece, 2);
        controlCenter.ColorSet(true);
    }

    void EmptyCheck()
    {
        controlCenter.CheckAll(piece, 0);
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

    public void DevolvePiece(Tile tile, int color)
    {
        Kill(tile);
        if (color == 1) color = 2;
        else if (color == 2) color = 1;
        CreatePiece(tile, 5, color);
    }

    public void CreatePiece(Tile tile, int pieceType, int color)
    {
        if (color == 1) {
            Instantiate(controlCenter.WPieces[pieceType], tile.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(controlCenter.BPieces[pieceType], tile.transform.position, Quaternion.identity);
        }
        EndAbility();
    }

    public void MoveTo(Tile tile)
    {
        controlCenter.PositionSet(tile.transform.position, piece, false);
        EndAbility();
    }

    //public void Assassinate(Tile tile)
    //{
    //    tile.SetPiece(piece, true);
    //    EndAbility();
    //}

    void FormShift()
    {
        int random = UnityEngine.Random.Range(2, 5);
        piece.currentTile.SetPromote(random);
        piece.currentTile.Promote(piece.pieceColor);
        EndAbility();
    }

    public void Kill(Tile tile)
    {
        tile.SetPiece(null, true);
    }

    public void CallEndAbility()
    {
        EndAbility();
    }

    public void FireShot(Tile tile)
    {
        tile.SetPiece(null, true);
        EndAbility();
    }

    public void Freeze(Tile tile)
    {
        tile.currentPiece.frozen = true;
        EndAbility();
    }

    void EndAbility()
    {
        if (piece != null)
        {
            piece.canTarget = false;
        }
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
        if (piece.usedAbility == 5)
        {
            Kill(piece.currentTile);
        }
        StartCoroutine(WaitForPromote());
    }

    IEnumerator WaitForPromote()
    {
        yield return new WaitUntil(() => controlCenter.promoteCanvas.activeInHierarchy == false);
        piece = null;
    }
}
