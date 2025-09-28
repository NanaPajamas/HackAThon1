using UnityEngine;

public class InputIconAnimator : MonoBehaviour
{
    [SerializeField] private UIImageAnimator animator;

    [System.Serializable]
    public struct InputSpriteGroup
    {
        public string name;
        public Sprite[] frames;
        public bool flipX;
    }

    public InputSpriteGroup[] inputGroups;

    public KeyCode antiClockwiseKey = KeyCode.Q;
    public KeyCode clockwiseKey = KeyCode.E;

    private int currentIndex = 0;

    void Start()
    {
        if (inputGroups.Length > 0)
            ApplyGroup(inputGroups[currentIndex]);
    }

    void Update()
    {
        if (Input.GetKeyDown(clockwiseKey))
        {
            NextGroup();
        }
        else if (Input.GetKeyDown(antiClockwiseKey))
        {
            PreviousGroup();
        }
    }

    void NextGroup()
    {
        if (inputGroups.Length == 0) return;

        currentIndex = (currentIndex + 1) % inputGroups.Length;
        ApplyGroup(inputGroups[currentIndex]);
    }

    void PreviousGroup()
    {
        if (inputGroups.Length == 0) return;

        currentIndex = (currentIndex - 1 + inputGroups.Length) % inputGroups.Length;
        ApplyGroup(inputGroups[currentIndex]);
    }

    void ApplyGroup(InputSpriteGroup group)
    {
        animator.frames = group.frames;

        // Flip via localScale
        Vector3 scale = animator.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (group.flipX ? -1 : 1);
        animator.transform.localScale = scale;
    }
}
