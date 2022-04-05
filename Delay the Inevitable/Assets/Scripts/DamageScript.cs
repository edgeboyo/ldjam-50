using Game;
using Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public string TriggerLayer = "Player";
    public float Damage = 1f;
    public float WaitTime = 1f;

    private Timer _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = new Timer();

    }

    private void Update()
    {
        _timer.Advance(Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_timer.IsRunning && !_timer.GoalTimeReached)
        {
            return;
        }


        if (collision.gameObject.layer == LayerMask.NameToLayer(TriggerLayer))
        {
            CharacterHealth healthManager = collision.gameObject.GetComponent<CharacterHealth>();

            if(healthManager != null)
            {
                healthManager.ReceiveDamage(Damage);
                _timer.ChangeGoalTime(WaitTime);
                _timer.Reset();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_timer.IsRunning && !_timer.GoalTimeReached)
        {
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer(TriggerLayer))
        {
            CharacterHealth healthManager = collision.gameObject.GetComponent<CharacterHealth>();

            if (healthManager != null)
            {
                healthManager.ReceiveDamage(Damage);
                _timer.ChangeGoalTime(WaitTime);
                _timer.Reset();
            }
        }
    }
}
