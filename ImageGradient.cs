using UnityEngine;
using UnityEngine.UI;

public class ImageGradient : MonoBehaviour
{
    public Image targetImage;
    public Gradient gradient;

    public void SetImageColor(float gradientThreshold)
    {
        if (targetImage == null || gradient == null)
            return;

        gradientThreshold = Mathf.Clamp01(gradientThreshold); // make sure it's 0-1
        targetImage.color = gradient.Evaluate(gradientThreshold);
    }
}
