using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public float tweenDuration = 0.25f;
    public TextMeshProUGUI healthText;

    private int maxHealth;
    private int currentHealth;

    public void Initialize(int maxHealth, int currentHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;

        slider.maxValue = maxHealth;
        slider.value = currentHealth;
        UpdateHealthText(currentHealth);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        slider.DOValue(currentHealth, tweenDuration).SetEase(Ease.OutQuad);
        UpdateHealthText(currentHealth);
    }

    private void UpdateHealthText(int health)
    {
        healthText.text = health.ToString();
    }
}
