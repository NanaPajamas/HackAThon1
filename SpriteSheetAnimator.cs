using UnityEngine;

public class SpriteSheetAnimator : MonoBehaviour
{
    public Material mat;                   // Material on the quad
    public SpriteAnimation[] animations;   // Define in inspector
    public float frameRate = 0.15f;

    private SpriteAnimation currentAnim;
    private int currentFrame;
    private float timer;

    public bool AnimEnd => !currentAnim.loop && currentFrame == currentAnim.frames.Length - 1;

    void Start()
    {
        SetAnimation("Idle"); // Default animation
    }

    void Update()
    {
        if (currentAnim == null || currentAnim.frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            currentFrame++;

            if (currentFrame >= currentAnim.frames.Length)
            {
                if (currentAnim.loop)
                    currentFrame = 0;
                else
                    currentFrame = currentAnim.frames.Length - 1;
            }

            ApplySprite(currentAnim.frames[currentFrame]);
            timer = 0f;
        }
    }

    public void SetAnimation(string name)
    {
        foreach (SpriteAnimation anim in animations)
        {
            if (anim.name == name)
            {
                currentAnim = anim;
                currentFrame = 0;
                ApplySprite(currentAnim.frames[currentFrame]);
                return;
            }
        }
    }

    private void ApplySprite(Sprite sprite)
    {
        if (sprite == null) return;

        // --- UV setup (crop only this sprite) ---
        Rect r = sprite.textureRect;
        Vector2 offset = new(r.x / sprite.texture.width, r.y / sprite.texture.height);
        Vector2 scale = new(r.width / sprite.texture.width, r.height / sprite.texture.height);

        // Flip Y because Unity’s UV origin is bottom-left but textureRect is top-left
        offset.y = 1f - offset.y - scale.y;

        mat.mainTexture = sprite.texture;
        //mat.mainTextureOffset = offset;
        mat.mainTextureScale = scale;

        // --- Resize the quad to match the sprite ---
        float worldWidth = sprite.rect.width / sprite.pixelsPerUnit;
        float worldHeight = sprite.rect.height / sprite.pixelsPerUnit;
        transform.localScale = new Vector3(worldWidth, worldHeight, 1f);
    }
}
