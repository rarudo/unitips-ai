using MLAgents;
using UnityEngine;

public class MarathonExampleAgent : Agent {
     [SerializeField] private StageGenerator stageGenerator;
     [SerializeField] private AiCharacter character;
 
     public override void InitializeAgent() {
         base.InitializeAgent();
         stageGenerator.Initialize(transform.position);
     }
 
     public override void CollectObservations() {
         
         // 自分のポジション
         AddVectorObs(character.transform.localPosition.y);
         // 速度
         AddVectorObs(character.Rigidbody.velocity.y);
 
         // キャラクターのジャンプ回数
         AddVectorObs(character.JumpCount);
 
         // 接地情報
         AddVectorObs(character.GroundDetector.IsGround);
 
         // Rayの情報
         foreach (var rayDetector in character.RayDetectors) {
             var distance = rayDetector.LastHitInfo.distance;
             AddVectorObs(distance);
         }
     }
 
 
     public override void AgentAction(float[] vectorAction, string textAction) {
         if (vectorAction[0] >= 1) {
             character.Jump();
         }
 
         if (character.DeathDetector.IsDead) {
             AddReward(-1f);
             Done();
         } else {
             AddReward(0.01f);
         }
     }
 
     public override void AgentReset() {
         base.AgentReset();
         stageGenerator.FlatStage();
         StartCoroutine(character.Reborn());
     }
 }