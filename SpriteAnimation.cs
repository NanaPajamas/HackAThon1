using UnityEngine;

[System.Serializable]
public class SpriteAnimation
{
    public string name;        // "Idle", "Attack", "Hurt", "Die"
    public Sprite[] frames;
    public bool loop = true;
}
