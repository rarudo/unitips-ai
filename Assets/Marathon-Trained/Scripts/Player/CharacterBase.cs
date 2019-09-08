using System.Collections;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; 
    public Rigidbody2D Rigidbody { get => _rigidbody ? _rigidbody : _rigidbody = this.GetComponent<Rigidbody2D>(); }

    [SerializeField]
    protected GroundDetector groundDetector;

    [SerializeField]
    private float jumpForce = 0.0f;
    
    [SerializeField]
    private DeathDetector _deathDetector;

    protected int jumpCount = 0;
    
    // リスポーン用に初期ポジションを保存しておく
    private Vector3 _initialPosition;
    

    public bool IsDead {
        get => _deathDetector.IsDead;
    }
    

    private void Awake()
    {
        _initialPosition = transform.position;
        OnAwake();
    }
    
    protected virtual void OnAwake() {
    }
    
    private void Start()
    {
        // 地面に接地した時にジャンプの回数をリセットする
        groundDetector.OnEnterGround.AddListener(() =>
        {
            jumpCount = 0;
            Rigidbody.velocity = Vector2.zero;
        });
        
        OnStart();
    }
    


    

    protected virtual void OnStart() {}

    public IEnumerator Reborn() {
        // イベントのループを避けるため，1フレーム待ってからリボーンする
        yield return null;

        gameObject.SetActive(true);

        _deathDetector.Revive();
        transform.position = _initialPosition;
        Rigidbody.velocity = Vector3.zero;
    }

    public void Jump()
    {
        // 2段ジャンプまで認める
        if (jumpCount < 2)
        {
            jumpCount++;
            // 速度を殺してから再度ジャンプ力を与える
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
        }
    }
}
