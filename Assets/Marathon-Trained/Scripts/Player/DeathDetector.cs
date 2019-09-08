using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDetector : MonoBehaviour
{
    // プレーヤーのY座標がこれを下回ったらゲームオーバーとする
    [SerializeField]
    private float deathLine = -5f;

    [SerializeField] private TriggerDetector2D sideTriggerDetector2D;

    public bool IsDead { get; private set; }

    private void Start()
    {
        sideTriggerDetector2D.OnCollisionEnterEvent.AddListener(() =>
        {
            if (IsDead) return;
            this.Dead();
        });
    }

    private void FixedUpdate()
    {
        if (transform.localPosition.y < deathLine && IsDead == false)
        {
            this.Dead();
        }
    }

    private void Dead()
    {
        IsDead = true;
    }

    public void Revive()
    {
        IsDead = false;
    }
}
