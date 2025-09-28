using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Image healthFill;
    public float tweenDuration = 0.25f;
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;
    public ImageGradient imageGradient;

    void Start()
    {
        SetHealth(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        Debug.Log($"[{gameObject.name}] TakeDamage called with amount: {amount}");
        if (isDead)
        {
            Debug.Log($"[{gameObject.name}] Already dead. Ignoring damage.");
            return;
        }

        SetHealth(Mathf.Max(currentHealth - amount, 0));

        if (currentHealth <= 0)
        {
            Debug.Log($"[{gameObject.name}] Health <= 0. Starting Die coroutine.");
            StartCoroutine(Die());
        }
        else
        {
            Debug.Log($"[{gameObject.name}] Health after damage: {currentHealth}. Attempting Hurt animation.");

            SpriteSheetAnimator sheetAnimator = GetComponentInParent<SpriteSheetAnimator>();
            if (sheetAnimator != null)
            {
                Debug.Log($"[{gameObject.name}] Found SpriteSheetAnimator on {sheetAnimator.gameObject.name}, playing Hurt.");
                sheetAnimator.SetAnimation("Hurt");
            }
            else
            {
                Debug.LogWarning($"[{gameObject.name}] No SpriteSheetAnimator found for Hurt animation.");
            }
        }
    }

    IEnumerator Die()
    {
        if (isDead)
        {
            Debug.Log($"[{gameObject.name}] Already dead. Exiting Die coroutine.");
            yield break;
        }

        isDead = true;
        Debug.Log($"[{gameObject.name}] Die coroutine started.");

        SpriteSheetAnimator sheetAnimator = GetComponentInParent<SpriteSheetAnimator>();
        if (sheetAnimator != null)
        {
            Debug.Log($"[{gameObject.name}] Found SpriteSheetAnimator for Death animation.");
            sheetAnimator.SetAnimation("Death");

            while (!sheetAnimator.AnimEnd)
            {
                yield return null;
            }
        }
        else
        {
            Debug.LogWarning($"[{gameObject.name}] No SpriteSheetAnimator found for Death animation.");
        }

        Enemy enemy = GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            Debug.Log($"[{gameObject.name}] Destroying enemy {enemy.gameObject.name} after death.");
            TicketManager ticketManager = FindAnyObjectByType<TicketManager>();
            if (ticketManager != null)
            {
                Debug.Log($"[{gameObject.name}] Adding ticket.");
                ticketManager.AddTicket();
            }
            Destroy(enemy.gameObject);
        }
        else
        {
            Debug.LogWarning($"[{gameObject.name}] No Enemy component found to destroy.");
        }
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        Debug.Log($"[{gameObject.name}] SetHealth called. New health: {currentHealth}");

        float fill = (float)currentHealth / maxHealth;
        healthFill.DOFillAmount(fill, tweenDuration).SetEase(Ease.OutQuad);

        if (imageGradient != null)
            imageGradient.SetImageColor(fill);
    }

    public int GetHealth() => currentHealth;
}
