using System;

public static class EventManager
{
    public static class PlayerEvents
    {
        public static event Action OnPlayerWin;
        public static void CallOnPlayerWin() => OnPlayerWin?.Invoke();

        public static event Action OnPlayerLose;
        public static void CallOnPlayerLose() => OnPlayerLose?.Invoke();
    }

    public static class EnvironmentEvents
    {
        public static event Action OnRunStart;
        public static void CallOnRunStart() => OnRunStart?.Invoke();
    }

    public static class GameStateEvents
    {
        public static event Action<float> OnTimerStop;
        public static void CallOnTimerStop(float time) => OnTimerStop?.Invoke(time);
    }
}
    
