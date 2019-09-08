using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerDetector2D : MonoBehaviour
{
    //接地した場合の処理
    public UnityEvent OnCollisionEnterEvent { get; private set; }

    //地面から離れた場合の処理
    public UnityEvent OnCollisionExitEvent { get; private set; }
    
    private void Awake()
    {
        OnCollisionEnterEvent = new UnityEvent();
        OnCollisionExitEvent = new UnityEvent();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnCollisionEnterEvent.Invoke();
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        OnCollisionExitEvent.Invoke();
    }
}
