using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlider: Slider {

    [SerializeField] private SpriteRenderer outLine;
    [SerializeField] private TMP_Text shower;
    private const float Interval = 0.027f;
    private const float CharacterInterval = 0.052f;

    
    public void ChangeColor(Color color) {
        outLine.color = color;
        handleRenderer.color = color;
    }
    
    public ChangeSliderState UpdateInfo(float current, float max) {

        float ratio = current / max;
        int count = (int)((ratio - Interval) / CharacterInterval);
        
        string context = $"{current}/{max}";
        context = new string(context.Take(count)?.ToArray()).AddColor(Color.black) +
                  new string(context.Skip(count)?.ToArray()).AddColor(Color.white);

        shower.text = context;
        return base.UpdateInfo(ratio);
    }

    private void Awake() {
        base.Awake();
    }
}