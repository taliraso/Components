using UnityEngine;
using UnityEngine.InputSystem;

public class TargetManager : MonoBehaviour
{
    [Header("Settings")]
    public float matchTolerance = 5f; // Degrees
    public float hapticThreshold = 10f; // How close the dial needs to be to trigger haptics
    public float randomDelay = 1f;

    // Pure target data (not attached to any object)
    private float leftDialTargetY;
    private float rightDialTargetY;

    [Header("Dial Objects")]
    public Transform leftDial;
    public Transform rightDial;

    private Gamepad gamepad;

    void Start()
    {
        gamepad = Gamepad.current; // Get the current gamepad (make sure a gamepad is connected)
        if (gamepad == null)
        {
            Debug.LogError("No Gamepad connected!");
        }
        GenerateNewTargets();
    }

    void Update()
    {
        if (IsDialMatched(leftDial, leftDialTargetY) && IsDialMatched(rightDial, rightDialTargetY))
        {
            Debug.Log("✅ Both dials matched!");
            Invoke(nameof(GenerateNewTargets), randomDelay);
        }

        // Check proximity to target for haptics
        if (IsDialCloseToTarget(leftDial, leftDialTargetY))
        {
            TriggerHapticFeedback(0.5f, 0.5f); // Moderate vibration intensity
        }
        else if (IsDialCloseToTarget(rightDial, rightDialTargetY))
        {
            TriggerHapticFeedback(0.5f, 0.5f); // Moderate vibration intensity
        }
        else
        {
            StopHapticFeedback(); // Stop vibration if no match
        }
    }

    void GenerateNewTargets()
    {
        leftDialTargetY = Random.Range(0f, 360f);
        rightDialTargetY = Random.Range(0f, 360f);

        Debug.Log($"🎯 New targets: Left {leftDialTargetY:F1}°, Right {rightDialTargetY:F1}°");
    }

    bool IsDialMatched(Transform dial, float targetY)
    {
        float dialY = dial.eulerAngles.y;
        float angleDiff = Mathf.Abs(Mathf.DeltaAngle(dialY, targetY));
        return angleDiff < matchTolerance;
    }

    bool IsDialCloseToTarget(Transform dial, float targetY)
    {
        float dialY = dial.eulerAngles.y;
        float angleDiff = Mathf.Abs(Mathf.DeltaAngle(dialY, targetY));
        return angleDiff < hapticThreshold;
    }

    void TriggerHapticFeedback(float lowFrequency, float highFrequency)
    {
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(lowFrequency, highFrequency); // Set motor speeds for vibration
        }
    }

    void StopHapticFeedback()
    {
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(0f, 0f); // Stop vibration
        }
    }
}


