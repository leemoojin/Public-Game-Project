using UnityEngine;

public class EyeTypeHand : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 3)
        {
            Debug.Log($"�÷��̾� ���� ����");

        }
        else if (other.gameObject.layer == 7) 
        {
            Debug.Log($"npc ���� ����");

        }
    }
}
