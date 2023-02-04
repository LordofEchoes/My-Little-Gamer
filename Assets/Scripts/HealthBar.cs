using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TextMeshProUGUI TextBox;
    // Start is called before the first frame update
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
        TextBox.text = $"{slider.value}/{slider.maxValue}";
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        TextBox.text = $"{slider.value}/{slider.maxValue}";
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
