using UnityEngine;
using UnityEngine.UI;

public class UIImageAnimator : MonoBehaviour
{
    public Image targetImage;
    public Sprite[] frames;
    public float frameRate = 12f;

    private int currentFrame;
    private float timer;

    void Update()
    {
        if (frames == null || frames.Length == 0) return;
        if (targetImage == null) return;

        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            currentFrame = (currentFrame + 1) % frames.Length;
            targetImage.sprite = frames[currentFrame];
            timer = 0f;
        }
    }
}
