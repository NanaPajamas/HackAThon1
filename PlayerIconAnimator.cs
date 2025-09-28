using UnityEngine;

public class PlayerIconAnimator : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private UIImageAnimator animator;

    [System.Serializable]
    public struct HealthSpriteGroup
    {
        public string name;
        public Sprite[] frames;
    }

    public HealthSpriteGroup[] healthGroups;

    void Update()
    {
        float health = player.playerHealth.GetHealth() / player.playerHealth.maxHealth;

        if (health <= 0.33f)
            animator.frames = GetFramesByName("Low Health");
        else if (health <= 0.66f)
            animator.frames = GetFramesByName("Medium Health");
        else
            animator.frames = GetFramesByName("High Health");
    }

    Sprite[] GetFramesByName(string name)
    {
        foreach (HealthSpriteGroup group in healthGroups)
        {
            if (group.name == name)
                return group.frames;
        }
        return null;
    }
}
