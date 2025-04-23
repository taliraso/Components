using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickMover : MonoBehaviour
{
    [SerializeField] private AudioEventConfig sceneAudioEventConfig;
    public StickType stick = StickType.Left;
    public float maxRotationSpeed = 180f;
    public bool isMoving = false;
    public bool leftIsMoving = false;
    public bool rightIsMoving = false;
    private float currentYRotation = 0f;
    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    void Update()
    {
        Vector2 input = stick == StickType.Left
            ? inputActions.Controls.LeftStick.ReadValue<Vector2>()
            : inputActions.Controls.RightStick.ReadValue<Vector2>();

        float tiltAmount = input.magnitude;

        if (tiltAmount > 0.01f)
        {
            isMoving = true;
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
            float step = tiltAmount * maxRotationSpeed * Time.deltaTime;
            currentYRotation = Mathf.MoveTowardsAngle(currentYRotation, targetAngle, step);
            transform.rotation = Quaternion.Euler(0f, currentYRotation, 0f);
        }
        
        if (input.magnitude > 0.1f)
        {
            GameEvents.OnStickMove?.Invoke(stick); // 🔥 Fire the event with the stick type
        }
        
    }

    void OnDisable() => inputActions.Disable();
}