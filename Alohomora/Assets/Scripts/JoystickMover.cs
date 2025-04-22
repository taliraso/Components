using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickMover : MonoBehaviour
{
    public enum StickType { Left, Right }
    public StickType stick = StickType.Left;
    public float maxRotationSpeed = 180f;

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
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
            float step = tiltAmount * maxRotationSpeed * Time.deltaTime;
            currentYRotation = Mathf.MoveTowardsAngle(currentYRotation, targetAngle, step);
            transform.rotation = Quaternion.Euler(0f, currentYRotation, 0f);
        }
    }

    void OnDisable() => inputActions.Disable();
}