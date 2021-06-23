using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    


    #region Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion


    #region Win Condition
    public void CheckWinConditions()
    {
        var (count, line) = LevelManager.Instance.EmptyBoxCount();
        if (count == 1)
        {
            if (line[line.Count - 1].CompareTag("bread") && line[0].CompareTag("bread"))
            {
                EventManager.OnLevelWin.Invoke();
                UIManager.Instance.UndoButton.interactable = false;
            }
        }
    }
    #endregion


    
}
