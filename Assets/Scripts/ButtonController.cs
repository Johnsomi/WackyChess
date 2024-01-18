using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public ControlCenter controlCenter;  
    public void RestartGame()
    {
        SceneManager.LoadScene(0);        
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Promote(int value)
    {
        if (controlCenter.current != null)
        {
            controlCenter.current.currentTile.SetPromote(value);
        }
        else if (controlCenter.abilities.piece != null)
        {
            controlCenter.abilities.piece.currentTile.SetPromote(value);
        }
        else
        {
            Debug.Log("Error with promotion");
        }
    }
}
