using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Collection : MonoBehaviour
{

    private bool isUITeam;
    private bool isUIMerge;

    public bool IsUIteam { get => isUITeam; set => isUITeam = value; }
    public bool IsUIMerge { get => isUIMerge; set => isUIMerge = value; }

    private void Start()
    {

    }
    public void ActiveUIMerge()
    {
        isUIMerge = true;
        isUITeam = false;
    }
    public void ActiveUIteam()
    {
        isUITeam = true;
        isUIMerge = false;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }
}
