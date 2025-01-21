using System;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

public enum ButtonType{
    Attack = 0,
    Item = 1,
    Shield = 2,
}




public class ButtonInteractManager: MonoBehaviour {

    public static ButtonInteractManager Instance { get; private set; } = null;
    [SerializeField] private ButtonInteract[] buttons = new ButtonInteract[3];

    public static readonly Dictionary<ButtonType, PlayerTurnState> MatchButtonAndState = new() {
        { ButtonType.Attack , PlayerTurnState.Attack},
        { ButtonType.Item   , PlayerTurnState.Item},
        { ButtonType.Shield , PlayerTurnState.Shield}
    };
    
    private ButtonType selectType = ButtonType.Attack;
    private bool isActive = true;
    
    public void SelectButton(ButtonInteract target, ButtonType type) {
        foreach (var button in buttons) {
            button.EndAnimation();
        }

        selectType = type;
        target.StartAnimation();
    }

    private void SelectBehavior() {

        if (GameFSM.Instance.PlayerTurnState != PlayerTurnState.SelectBehavior)
            return;

        if(InputManager.Instance.Click(KeyTypes.Right) || InputManager.Instance.Hold(KeyTypes.Right)) {
            if (selectType == ButtonType.Shield) {
                SelectButton(buttons[(int)ButtonType.Attack], ButtonType.Attack);
            }
            else {
                        
                SelectButton(buttons[(int)selectType + 1], (ButtonType)((int)selectType + 1));
            }
        }
        
        if (InputManager.Instance.Click(KeyTypes.Left) || InputManager.Instance.Hold(KeyTypes.Left)) {
            if (selectType == ButtonType.Attack) {
                SelectButton(buttons[(int)ButtonType.Shield], ButtonType.Shield);
            }
            else {
                                    
                SelectButton(buttons[(int)selectType - 1], (ButtonType)((int)selectType - 1));
            }
        }
    }

    private void ShowButton() {
        
        if (GameFSM.Instance.State != GameState.PlayerAttack) {
            if (isActive) {
                isActive = false;
                        
                foreach (var button in buttons) {
                    button.gameObject.SetActive(false);
                }
            }
            return;
        }
        
        if (!isActive) {
                    
            isActive = true;
            foreach (var button in buttons) {
                button.gameObject.SetActive(true);
            }
        }
    }
    
    
    private void Update() {

        ShowButton(); 
        SelectBehavior();

        if (InputManager.Instance.Click(KeyTypes.Select)) {

            buttons[(int)selectType].Click();
        }
    }

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

    }
}