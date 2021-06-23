using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Button UndoButton;

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    
void Start()
    {
        UndoButton.interactable = false;
    }

    public void ActivateBackButton(){
        UndoButton.interactable = true;
    }

    public void BackButton(){
        ObjectRotationManager.Instance.BackMove();
        if(ObjectRotationManager.Instance.History.Count<1){ UndoButton.interactable = false;}
    }
}
