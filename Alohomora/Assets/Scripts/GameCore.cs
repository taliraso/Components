using UnityEngine;

public class GameCore : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameEvents.EnterLockMechanic?.Invoke(); // 🎧 fire event once
    }

    // Update is called once per frame
    void Update()
    {
        GameEvents.ExitLockMechanic?.Invoke(); // 🎧 fire event once
    }
}
