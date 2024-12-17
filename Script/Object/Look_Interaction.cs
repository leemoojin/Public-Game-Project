using UnityEngine;

public class Look_Interaction : MonoBehaviour
{
    public Transform interaction_image;
    public Transform player;
    void Update()
    {
        interaction_image.transform.rotation = player.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (interaction_image != null)
        {
            Destroy(this.gameObject);
        }
    }
}
