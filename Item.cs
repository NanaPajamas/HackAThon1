using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public float damage;
    public WeaponType weaponType;

    [Header("For Melee")]
    public float attackCooldown;

    [Header("For Ranged")]
    public GameObject projectile;
    public float fireRate;
}

public enum WeaponType
{
    Melee,
    Ranged
}