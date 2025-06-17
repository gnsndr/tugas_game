using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.position;
    }

    private void Update()
    {
        Vector3 targetPos = player.position + offset;
        transform.position = targetPos;

    }
}
