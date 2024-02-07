using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image barImage;

    private float maxValue;
    private float currentValue;

    public void StartProgress(int value, bool fill = false) {
        maxValue = value;
        currentValue = fill ? 0 : value;
        barImage.fillAmount = currentValue / maxValue;
        gameObject.SetActive(true);
    }

    public void ResetProgress() {
        gameObject.SetActive(false);
    }

    public void Step(float amount = 1) {
        currentValue -= amount;
        barImage.fillAmount = Mathf.Max(currentValue, 0) / maxValue;
    }

    public void SetProgress(float value) {
        currentValue = value;
        barImage.fillAmount = Mathf.Max(currentValue, 0) / maxValue;
    }

    public void SetBackgroundColor(Color color) => backgroundImage.color = color;
    public void SetBarColor(Color color) => barImage.color = color;
}
