using System;
using UnityEngine;

namespace Tools
{
    public class Timer : MonoBehaviour
    {
        private float currentTime;
        private bool isActive;

        private void OnEnable()
        {
            EventManager.EnvironmentEvents.OnRunStart += StartCounting;
            EventManager.PlayerEvents.OnPlayerWin += StopTimer;
        }

        private void StartCounting()
        {
            isActive = true;
        }

        private void StopTimer()
        {
            EventManager.GameStateEvents.CallOnTimerStop(currentTime);
            isActive = false;
        }
        
        
        private void FixedUpdate()
        {
            if (isActive)
            {
                currentTime += Time.fixedDeltaTime;
            }
        }
        
        private void OnDisable()
        {
            EventManager.PlayerEvents.OnPlayerWin -= StopTimer;
            EventManager.EnvironmentEvents.OnRunStart -= StartCounting;
        }
    }
}