using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    [NonSerialized] public CameraControls instance;

    [SerializeField]
    private Transform mainCamera;

    [Header("Parameters")]
    [SerializeField] private CameraParameters defaultParameters;
    [SerializeField] private CameraParameters cannonParameters;

    private CameraParameters parameters;
    private Vector3 lastPosition;
    private Vector2 rotation;

    public enum CameraSetup { DEFAULT, CANNON };
    private CameraSetup setup = CameraSetup.DEFAULT;
    private CameraSetup lastSetup = CameraSetup.DEFAULT;

    [SerializeField] PlayerInput m_playerInput;
    public void ResetPosition()
    {
        transform.parent = null;
        transform.position = lastPosition;
        rotation = Vector2.zero;
        mainCamera.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    /// <summary>
    /// Sets position to target
    /// </summary>
    /// <param name="adjustX"> Adjust camera's X by this much after </param>
    /// <param name="resetCamera"></param>
    public void MatchTransform(Transform target, float adjustX = 0f, bool resetCamera = false)
    {
        lastPosition = transform.position;
        transform.parent = target;
        transform.localEulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;

        if (resetCamera)
        {
            rotation = Vector2.zero;
            mainCamera.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        mainCamera.localEulerAngles += new Vector3(adjustX, 0f, 0f);
    }

    /// <summary>
    /// Switches camera state (parameter)
    /// </summary>
    /// <param name="value"></param>
    public void SetParameters(CameraSetup value)
    {
        lastSetup = setup;

        setup = value;
        switch (value)
        {
            case CameraSetup.DEFAULT:
                parameters = defaultParameters;
                break;
            case CameraSetup.CANNON:
                parameters = cannonParameters;
                break;
            default:
                break;
        }

        Cursor.lockState = parameters.lockCursor;
        Cursor.visible = parameters.showCursor;
    }

    public void ResetParameters()
    {
        SetParameters(lastSetup);
    }
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetParameters(CameraSetup.DEFAULT);
        rotation = mainCamera.localEulerAngles;
    }

    public Vector2 GetRotation()
    {
        if (parameters.lockRotation)
        {
            return rotation;
        }

        float deltaX = 0;
        Debug.Log(m_playerInput.name);
        deltaX = m_playerInput.actions["Look"].ReadValue<Vector2>().x;

        float deltaY = 0;
        deltaY = -m_playerInput.actions["Look"].ReadValue<Vector2>().y;

        //Adds to rotation unless its under requiredDelta from current parameters
        if(Mathf.Abs(deltaX) >= parameters.requiredDelta)
        {
            rotation.y += deltaX * parameters.rotationSpeed; ;
        }
        if (Mathf.Abs(deltaY) >= parameters.requiredDelta)
        {
            rotation.x += deltaY * parameters.rotationSpeed; ;
        }

        //clamps angle for non free look, cannon mode and whatnot
        if(!parameters.freeLookX)
        {
            rotation.x = ClampAngle(rotation.x, parameters.xBounds.x, parameters.xBounds.y);
        }
        if(!parameters.freeLookY)
        {
            rotation.y = ClampAngle(rotation.y, parameters.yBounds.x, parameters.yBounds.y);
        }

        return rotation;
    } 
    void Update()
    {
        if (parameters.lockCamera)
        {
            return;
        }
        mainCamera.localEulerAngles = GetRotation();
    }

    private float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360.0f) && (angle <= 360.0f))
        {
            if (angle < -360.0f)
            {
                angle += 360.0f;
            }
            if (angle > 360.0f)
            {
                angle -= 360.0f;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }

    public void Reposition(Vector3 pos)
    {
        mainCamera.position = pos;
    }
}
