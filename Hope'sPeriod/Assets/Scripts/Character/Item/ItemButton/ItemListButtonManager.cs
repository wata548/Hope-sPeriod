using System;
using Unity.VisualScripting;
using UnityEditor.AddressableAssets.Build.BuildPipelineTasks;
using UnityEngine;
using UnityEngine.UI;
using VInspector.Libs;

public class ItemListButtonManager: InteractButtonManager {
    
    //==================================================||Set Inspector 
    
    [SerializeField] private GameObject itemList;

    [SerializeField] private Cursor cursor;
    //==================================================||Field 
    private static FloatingItemInfo floating = null;
    
    //==================================================||Property 
    public static ItemListButtonManager Instance { get; private set; } = null;
    public override bool Interactable { get; protected set; } = false;

    //==================================================||Method 
    
    public static void SetFloating(FloatingItemInfo floating) {
        ItemListButtonManager.floating = floating;
    }
    
    public void TurnOn() {
        
        itemList.SetActive(true);
        ItemListContext.Instance.TurnOn();
        cursor.TurnOn();
        Interactable = true;
    }

    public void TurnOff() {
        itemList.SetActive(false);
        floating.TurnOff();
        Interactable = false;
        InitSelect();
        
        foreach (var button in buttons) {

            Parse(button).TurnOff();
        }
    }

    #region Interaction

    

    public override void SelectIn(InteractButton target) {

        var button = Parse(target);
        
        if (!button.Show)
            return;
        
        cursor.SetIndex(target.Index);
    }
    public override void SelectOut(InteractButton target) {}
    
    #endregion

    public void CheckCursorIndex() {

        int index = cursor.Index;
        var button = Parse(buttons[index]);
        if (!button.Show) {

            Debug.Log("init");
            SelectButton(0);
        }

    }

    private void Input() {
        if (InputManager.Instance.ClickAndHold(KeyTypes.Right)) {
            ItemListContext.Instance.NextPage();
        }
            
        if (InputManager.Instance.ClickAndHold(KeyTypes.Left)) {
            ItemListContext.Instance.PriviousPage();
        }
            
        if (InputManager.Instance.ClickAndHold(KeyTypes.Down)) {
                
            NextButton();
            if (!Parse(buttons[Selecting]).Show) 
                Selecting = 0;
            
            UpdateState();
            cursor.SetIndex(Selecting);
        }
            
        if (InputManager.Instance.ClickAndHold(KeyTypes.Up)) {

            PriviousButton();
            if (!Parse(buttons[Selecting]).Show) {

                for (int i = Selecting - 1; i > -1; i--) {
                    if (Parse(buttons[i]).Show) {
                        
                        Selecting = i;
                        break;
                    }

                    if (i == 0) Selecting = 0;
                }
            }
            
            UpdateState();
            cursor.SetIndex(Selecting);
            
        }
            
        if (InputManager.Instance.Click(KeyTypes.Select)) {
            
            int index = cursor.Index;
                
            Parse(buttons[index]).Click();
        }
    }
    
    private ItemListButton Parse(InteractButton button) {
        if (button is not ItemListButton itemButton) {
            throw new TypeMissMatched(button.gameObject, typeof(ItemListButton));
        }
    
        return itemButton;
    }
    
    //==================================================||Unity Func 
    private void Start() {
        TurnOff();
    }

    private void Awake() {
        base.Awake();

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    private void Update() {

        if (!Interactable)
            return;

        Input();
    }

   
}