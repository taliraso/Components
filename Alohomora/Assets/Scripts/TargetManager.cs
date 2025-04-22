using UnityEngine;
using UnityEngine.InputSystem;

public class TargetManager : MonoBehaviour
{
    private bool wasInRange = false;
    
    [Header("Settings")]
    public float matchTolerance;
    public float hapticThreshold;
    public float holdDuration;
    public float randomDelay;

    private float leftDialTargetY;
    private float rightDialTargetY;

    [Header("Dial Objects")]
    public Transform leftDial;
    public Transform rightDial;

    [Header("Visual Feedback")]
    public Renderer leftDialIndicator;
    public Renderer rightDialIndicator;
    public Material defaultMaterial;
    public Material inRangeMaterial;

    private Gamepad gamepad;
    private float matchTimer = 0f;
    private bool matchTriggered = false;

    void Start()
    {
        gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.LogWarning("No Gamepad connected!");
        }
        GenerateNewTargets();
    }

    void Update()
    {
        bool leftInRange = IsDialCloseToTarget(leftDial, leftDialTargetY);
        bool rightInRange = IsDialCloseToTarget(rightDial, rightDialTargetY);
        bool bothMatched = IsDialMatched(leftDial, leftDialTargetY) && IsDialMatched(rightDial, rightDialTargetY);

        // Material Feedback
        leftDialIndicator.material = leftInRange ? inRangeMaterial : defaultMaterial;
        rightDialIndicator.material = rightInRange ? inRangeMaterial : defaultMaterial;

        // Haptic Feedback
        if (leftInRange || rightInRange)
        {
            TriggerHapticFeedback(0.5f, 0.5f);
        }
        else
        {
            StopHapticFeedback();
        }
        
        // Trigger within range sound ONCE when either dial enters range
        if (!wasInRange && (leftInRange || rightInRange))
        {
            GameEvents.OnWithinRange?.Invoke(); // 🎧 fire event once
            wasInRange = true;
        }
        else if (!leftInRange && !rightInRange)
        {
            wasInRange = false;
        }
        

        // Timer logic for hold-to-match
        if (bothMatched)
        {
            matchTimer += Time.deltaTime;

            if (matchTimer >= holdDuration && !matchTriggered)
            {
                Debug.Log("✅ Both dials matched and held!");
                matchTriggered = true;
                GameEvents.OnDialsMatched?.Invoke(); // 🎧 Broadcast match
                Invoke(nameof(GenerateNewTargets), randomDelay);
            }
        }
        else
        {
            matchTimer = 0f;
            matchTriggered = false;
        }
    }

    void GenerateNewTargets()
    {
        leftDialTargetY = Random.Range(0f, 360f);
        rightDialTargetY = Random.Range(0f, 360f);
        matchTimer = 0f;
        matchTriggered = false;

        GameEvents.OnNewTargets?.Invoke(); // 🎧 Broadcast new target
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
            gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
        }
    }

    void StopHapticFeedback()
    {
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(0f, 0f);
        }
    }
}
