using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    public Movement piece;
    //public List<int> abilities = new List<int>();
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
        Draft = 11
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
            int random = Random.Range(0, 12);
            piece.abilityType.Add(random);
        }
        else if (count == 1)
        {
            bool t = true;
            while (t)
            {
                int currentAbility = piece.abilityType[0];
                int random = Random.Range(0, 12);
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
    }

    public void ActivateAbilityTwo()
    {
        int ability = piece.abilityType[1];
        AbilityAction(ability);
    }

    public void CloseAbilities()
    {
        buttonOne.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
        buttonTwo.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
        buttonOne.gameObject.SetActive(false);
        buttonTwo.gameObject.SetActive(false);
        buttonClose.gameObject.SetActive(false);
        piece = null;
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
                break;

            case 2:
                //return "Warp";
                break;

            case 3:
                //return "Dash";
                break;

            case 4:
                //return "Swap";
                break;

            case 5:
                //return "Self Destruct";
                break;

            case 6:
                //return "Poisonous";
                break;

            case 7:
                //return "Hideaway";
                break;

            case 8:
                //return "Freeze";
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
        }
        CloseAbilities();
    }
}
