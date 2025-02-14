using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScenceChangeEffecter: MonoBehaviour {
    private Image changer;
    public static ScenceChangeEffecter Instance { get; private set; } = null;
    public Tween StartEffect() {
        
        changer.fillAmount = 0;
        changer.enabled = true;
        return DOTween.To(() => changer.fillAmount, x => changer.fillAmount = x, 1f, 1.5f);
    }

    private void Awake() {
        changer = GetComponent<Image>();
        changer.enabled = false;

        if (Instance == null) {

            Instance = this;
        }
        else if (Instance != this)
            Destroy(this);
    }
}