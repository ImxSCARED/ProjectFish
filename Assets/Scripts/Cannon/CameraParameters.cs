using UnityEngine;

[CreateAssetMenu(menuName = "Camera Parameters")]
public class CameraParameters : ScriptableObject
{
    public float rotationSpeed = 1f;
    public float requiredDelta = 0.1f;
    public Vector2 xBounds = new Vector2(-60f, 60f);
    public Vector2 yBounds = new Vector2(-60f, 60f);
    public bool freeLookX = false;
    public bool freeLookY = true;
    public bool lockCamera = false;
    public bool lockRotation = false;
    public CursorLockMode lockCursor = CursorLockMode.Locked;
    public bool showCursor = false;
}
