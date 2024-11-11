using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform monster;
    public float findRange = 30f;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * findRange, Color.red);

    }
}
