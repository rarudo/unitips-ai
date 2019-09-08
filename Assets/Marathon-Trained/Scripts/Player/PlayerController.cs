using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CharacterBase player;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            // タッチ情報を取得する
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                player.Jump();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Jump();
        }
    }
}
