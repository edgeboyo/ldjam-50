using System;

namespace Misc
{
    public class Timer
    {
        private float m_totalTime;
        private float m_lapTime;
        private float m_goalTime;
        
        private bool m_isLooping;
        private bool m_isRunning;

        private float m_newGoalTime;
        private bool m_hasNewGoalTime;

        private Action m_newCallback;
        private bool m_hasNewCallback;

        public float TotalTime => m_totalTime;
        public float LapTime => m_lapTime;
        public bool IsRunning => m_isRunning;
        public float Progress => m_lapTime / m_goalTime;
        public bool GoalTimeReached => m_lapTime >= m_goalTime;
        public bool IsDitchable => GoalTimeReached && !m_isRunning;

        public event Action Callback;

        public Timer()
        {
            m_totalTime = 0f;
            m_lapTime = 0f;
            m_goalTime = float.MaxValue;
            m_isLooping = false;
            m_isRunning = false;
        }

        public Timer(float goalTime, bool loop = false, bool run = false) : this()
        {
            m_goalTime = goalTime;
            m_isLooping = loop;

            if (run)
            {
                Run();
            }
        }

        public void ChangeGoalTime(float newGoalTime)
        {
            if (m_isLooping)
            {
                m_newGoalTime = newGoalTime;
                m_hasNewGoalTime = true;
            }
            else
            {
                m_goalTime = newGoalTime;
            }
        }
        
        public void ChangeCallback(Action action)
        {
            if (m_isLooping)
            {
                m_newCallback = action;
                m_hasNewCallback = true;
            }
            else
            {
                Callback = action;
            }
        }

        public void Run()
        {
            m_isRunning = true;
        }

        public void Stop()
        {
            m_isRunning = false;
        }
        
        public void Reset()
        {
            m_totalTime = 0f;
            m_lapTime = 0f;
            
            Run();
        }

        public void ClearCallback()
        {
            Callback = null;
        }

        public void Advance(float deltaTime)
        {
            if (!m_isRunning)
            {
                return;
            }
            
            m_totalTime += deltaTime;
            m_lapTime += deltaTime;

            while (GoalTimeReached && m_isRunning)
            {
                if (m_isLooping)
                {
                    m_lapTime -= m_goalTime;
                    Callback?.Invoke();

                    if (m_hasNewGoalTime)
                    {
                        m_goalTime = m_newGoalTime;

                        m_newGoalTime = default;
                        m_hasNewGoalTime = false;
                    }

                    if (m_hasNewCallback)
                    {
                        Callback = m_newCallback;

                        m_newCallback = null;
                        m_hasNewCallback = false;
                    }
                }
                else
                {
                    Callback?.Invoke();
                    m_isRunning = false;
                }
            }
        }

        public void SetTimer(float time)
        {
            m_totalTime = time;
            m_lapTime = time;
        }
    }
}