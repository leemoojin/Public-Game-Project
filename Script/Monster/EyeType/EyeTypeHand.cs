using UnityEngine;

public class EyeTypeHand : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 3)
        {
            Debug.Log($"플레이어 공격 성공");

        }
        else if (other.gameObject.layer == 7) 
        {
            Debug.Log($"npc 공격 성공");

        }
    }
}
