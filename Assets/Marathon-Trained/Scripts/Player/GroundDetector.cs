using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundDetector : MonoBehaviour
{
    //接地した場合の処理
    public UnityEvent OnEnterGround { get; private set; }

    //地面から離れた場合の処理
    public UnityEvent OnExitGround { get; private set; }

    // 地面に接地しているか
    public bool IsGround => _enterNum >= 1;

    //接地数
    private int _enterNum = 0;

    private void Awake()
    {
        OnEnterGround  = new UnityEvent();
        OnExitGround  = new UnityEvent();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _enterNum++;
        OnEnterGround.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _enterNum--;
        if (_enterNum <= 0)
        {
            OnExitGround.Invoke();
            _enterNum = 0;
        }
    }
}
