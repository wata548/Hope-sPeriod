using UnityEngine;
using System;
using System.Collections.Generic;

public class SkillButtonManager: InteractButtonManager {
    
    //==================================================||Set Inspector 
    
    [SerializeField] private GameObject skillList;
    [SerializeField] private Cursor cursor;
 
    //==================================================||Field 

    private RectTransform rect;    
    private static FloatingItemInfo floating = null;
    private readonly Vector2 defaultPos = new(0, 70);
    private readonly Vector2 interval = new(633, 0);
    private const int MaxCharacterCount = 3;
    
    //==================================================||Property 
    public static SkillButtonManager Instance { get; private set; } = null;
    public override bool Interactable { get; protected set; } = false;
    public int CharacterIndex { get; private set; } = 0;
    public List<(int code, int selectTarget)> SelectList { get; private set; } = new();
    
    //==================================================||Method 
    
    public static void SetFloating(FloatingItemInfo floating) {
        SkillButtonManager.floating = floating;
    }
    
    public void TurnOn() {
        skillList.SetActive(true);
        Interactable = true;
        CharacterIndex = 0;

        SkillSelectInit();
        var characterControler = CharactersInfoBattle.Instance;
        int characterCount =  characterControler.CharacterCount;
        while (CharacterIndex < characterCount && characterControler.Dead(CharacterIndex)) {
                    
            CharacterIndex++;
        }
       
        Refresh();
    }

    public void SkillSelectInit() { 
        CharacterIndex = 0;
        while (SelectList.Count < MaxCharacterCount) {
            SelectList.Add((0,0));
        }
               
        for (int i = 0; i < SelectList.Count; i++)
            SelectList[i] = (0,0);
               
        var characterControler = CharactersInfoBattle.Instance;
        int characterCount =  characterControler.CharacterCount; 
    }

    public void SkipSelect() {

        SelectList[CharacterIndex] = (9401,CharacterIndex); 
        CharacterIndex++;

        var characterControler = CharactersInfoBattle.Instance;
        int characterCount =  characterControler.CharacterCount;
        
        while (CharacterIndex < characterCount && characterControler.Dead(CharacterIndex)) {
            
            CharacterIndex++;
        }
        
        if (CharacterIndex < characterCount) {
            
            Refresh();
            return;
        }
        
        TurnOff();
        GameFSM.Instance.SkipState();
    } 
    
    public void NextSelect(int target = 0) {

        SelectList[CharacterIndex] = (Parse(buttons[Selecting]).Code, target);
        CharacterIndex++;

        var characterControler = CharactersInfoBattle.Instance;
        int characterCount =  characterControler.CharacterCount;
        
        while (CharacterIndex < characterCount && characterControler.Dead(CharacterIndex)) {
            
            CharacterIndex++;
        }
        
        if (CharacterIndex < characterCount) {
            
            Refresh();
            return;
        }
        
        TurnOff();
        GameFSM.Instance.SkipState();
    }

    public void PriviousSelect() {

        SelectList[CharacterIndex] = (0,0);
        CharacterIndex--;

        while (CharacterIndex >= 0 && CharactersInfoBattle.Instance.Dead(CharacterIndex)) {
            CharacterIndex--;
        }

        if (CharacterIndex >= 0) {
            Refresh();
            return;
        }

        GameFSM.Instance.DefaultPlayerTurnState();
    }
    
    public void Refresh() {

        rect.localPosition = defaultPos + CharacterIndex * interval;
        Selecting = 0;
        cursor.SetIndex(Selecting);
        
        foreach (var button in buttons) {

            Parse(button).Refresh();
        }
    }
    
    public void TurnOff() {
        CharacterIndex = 0;
        skillList.SetActive(false);
        floating.TurnOff();
        Interactable = false;
        InitSelect();
        
        foreach (var button in buttons) {

            Parse(button).TurnOff();
        }
    }

    #region Interaction

    public override void SelectIn(InteractButton target) {

        if (!Parse(buttons[Selecting]).Show)
            return;
        
        cursor.SetIndex(Selecting);
    }
    public override void SelectOut(InteractButton target) {}
    
    #endregion

    private void Input() {
            
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

        bool select = InputManager.Instance.Click(KeyTypes.Select);
        bool skip = InputManager.Instance.Click(KeyTypes.Right);
        bool cancel = InputManager.Instance.Click(KeyTypes.Cancel) || InputManager.Instance.Click(KeyTypes.Left);
        if (select) {
            
            int index = Selecting;
                
            Parse(buttons[index]).Click();
        }
        else if (skip) {
            SkipSelect();
        }
        else if (cancel) {

            PriviousSelect();
        }
    }
    
    private static SkillButton Parse(InteractButton button) {
        if (button is not SkillButton itemButton) {
            throw new TypeMissMatched(button.gameObject, typeof(SkillButton));
        }
    
        return itemButton;
    }
    
    //==================================================||Unity Func 
    private void Start() {
        TurnOff();
    }

    private void Awake() {

        SkillInfo.SetTable();
        
        base.Awake();

        Instance = this;

        rect = skillList.GetComponent<RectTransform>();
    }

    private void Update() {

        if (!Interactable)
            return;

        Input();
    }

   
}