using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private readonly HashSet<HealthBar> alreadyHit = new();
    public LayerMask breakableLayer;

    public void PerformHit()
    {
        Debug.Log($"[{gameObject.name}] PerformHit called.");
        ClearHits();

        if (!TryGetComponent(out BoxCollider box))
        {
            Debug.LogWarning($"[{gameObject.name}] No BoxCollider found.");
            return;
        }

        Vector3 boxCenter = transform.TransformPoint(box.center);
        Vector3 boxHalfExtents = Vector3.Scale(box.size * 0.5f, transform.lossyScale);

        Collider[] hits = Physics.OverlapBox(boxCenter, boxHalfExtents, transform.rotation, breakableLayer);
        Debug.Log($"[{gameObject.name}] OverlapBox found {hits.Length} hits.");

        foreach (Collider hit in hits)
        {
            ProcessHit(hit);
        }
    }

    private void ProcessHit(Collider other)
    {
        Debug.Log($"[{gameObject.name}] ProcessHit called on {other.gameObject.name}");
        Item selectedItem = Inventory.Instance.GetSelectedItem();

        HealthBar breakable = other.GetComponentInChildren<HealthBar>();
        if (breakable == null)
        {
            Debug.LogWarning($"[{other.gameObject.name}] No HealthBar found.");
            return;
        }

        if (((1 << other.gameObject.layer) & breakableLayer) == 0)
        {
            Debug.LogWarning($"[{other.gameObject.name}] Not on breakable layer.");
            return;
        }

        if (alreadyHit.Contains(breakable))
        {
            Debug.Log($"[{other.gameObject.name}] Already hit, skipping.");
            return;
        }

        int damage = (int)selectedItem.damage;
        Debug.Log($"[{other.gameObject.name}] Taking damage: {damage}");
        breakable.TakeDamage(damage);
        alreadyHit.Add(breakable);
    }

    public void ClearHits()
    {
        alreadyHit.Clear();
    }
}
