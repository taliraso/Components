using System;

public static class GameEvents
{
    public static System.Action<StickType> OnStickMove;
    public static Action OnDialsMatched;
    public static Action OnWithinRange;
    public static Action OnNewTargets;
    public static Action EnterLockMechanic;
    public static Action ExitLockMechanic;
    public static Action OnLockMove;
    public static Action Unlock;
}
