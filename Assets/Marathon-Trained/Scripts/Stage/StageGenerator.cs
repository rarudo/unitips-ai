using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] private StageParts stageParts;

    public List<StageParts> createdStageParts;

    [SerializeField] private float scrollSpeed = 1;

    [SerializeField] private float spawnPositionX = 4f;
    [SerializeField] private float despawnPositionX = -4f;

    private Vector3 _spawnPosition;
    private Vector3 _spawnScale = new Vector3(1, 1, 0.2f);

    [SerializeField]
    private float scaleXmin = 0.2f;
    [SerializeField]
    private float scaleXmax = 1f;
    
    [SerializeField]
    private float scaleYmin = 1;
    
    [SerializeField]
    private float scaleYmax= 2;

    [SerializeField] private float stageGenerateMin = 0.1f;
    [SerializeField] private float stageGenerateMax = 1f;

    public bool GenerateFlatStage;

    public void Initialize(Vector3 playerPosition) {
        _spawnPosition = new Vector3(
            playerPosition.x + 5,
            playerPosition.y - 4,
            playerPosition.z);

        for (float i = despawnPositionX; i < spawnPositionX; i++) {
            var parts = GetStageParts();
            parts.transform.position = new Vector3(i, _spawnPosition.y, _spawnPosition.z);
            parts.transform.localScale = _spawnScale; 
            parts.SetVelocity(new Vector3(scrollSpeed, 0, 0));
        }
        StartCoroutine(CreateRandomStage());
    }

    private StageParts GetStageParts() {
        foreach (var part in createdStageParts)
        {
            var position = part.transform.position;
            if (position.x < despawnPositionX) {
                return part;
            }
        }

        var parts = Instantiate(stageParts, transform);
        parts.Initialize(true);
        createdStageParts.Add(parts);
        return parts;

    }

    /// <summary>
    /// 1秒ごとにループする
    /// </summary>
    IEnumerator CreateRandomStage()
    {
        while (true) {
            var parts = GetStageParts();
            parts.transform.position = _spawnPosition;
            parts.transform.localScale = new Vector3(
                x: Random.Range(scaleXmin, scaleXmax),
                y: Random.Range(scaleYmin, scaleYmax),
                z: 0.2f);
            parts.SetVelocity(new Vector3(scrollSpeed, 0, 0));

            // 乱数にコクをつけてまろやかに
            var waitValue = (Random.Range(stageGenerateMin, stageGenerateMax) +
                         Random.Range(stageGenerateMin, stageGenerateMax) +
                         Random.Range(stageGenerateMin, stageGenerateMax) +
                         Random.Range(stageGenerateMin, stageGenerateMax) +
                         Random.Range(stageGenerateMin, stageGenerateMax)) / 5f;
            waitValue = waitValue - (stageGenerateMin + stageGenerateMax) / 2;
            waitValue = Mathf.Abs(waitValue);
            
            // waitForSecondsだとTimescaleを上げたときに、ちゃんと動かない
            float ttl = 0;
            while(waitValue > ttl){
                ttl += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public void FlatStage() {
        foreach (var part in createdStageParts) {
            part.transform.localScale = _spawnScale;
        }
    }
}