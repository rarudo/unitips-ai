using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCharacter : CharacterBase {
    public GroundDetector GroundDetector => groundDetector;

    [SerializeField] private List<RayDetector> rayDetectors;

    public List<RayDetector> RayDetectors => rayDetectors;

    public int JumpCount => jumpCount;

    // リスポーン用に初期ポジションを保存しておく
    private Vector3 initialPosition;

    public bool isDead {
        get => DeathDetector.IsDead;
    }

    public DeathDetector DeathDetector;

    protected override void OnAwake() {
        initialPosition = transform.position;
    }

    public IEnumerator Reborn() {
        // イベントのループを避けるため，1フレーム待ってからリボーンする
        yield return null;

        this.gameObject.SetActive(true);

        DeathDetector.Revive();
        transform.position = initialPosition;
        Rigidbody.velocity = Vector3.zero;
    }
}