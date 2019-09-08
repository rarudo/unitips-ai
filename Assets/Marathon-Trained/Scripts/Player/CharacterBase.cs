using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; 
    public Rigidbody2D Rigidbody { get => _rigidbody ? _rigidbody : _rigidbody = this.GetComponent<Rigidbody2D>(); }

    [SerializeField]
    protected GroundDetector groundDetector;

    [SerializeField]
    private float jumpForce = 0.0f;

    protected int jumpCount = 0;

    private Vector3 _initPos;

    private void Awake()
    {
        _initPos = this.gameObject.transform.position;
        OnAwake();
    }

    protected virtual void OnAwake() {}

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

    public void Spawn()
    {
        // イベントのループを避けるため，1フレーム待ってからリボーンする
        transform.position = _initPos;
        Rigidbody.velocity = Vector3.zero;
    }
}
