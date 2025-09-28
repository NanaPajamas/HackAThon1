using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        // Face the main camera
        transform.LookAt(Camera.main.transform);

        // Lock rotation so it only rotates on Y (no tilting when looking up/down)
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        transform.Rotate(0, 180f, 0);
    }
}
