using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovenment;

    private void Start()
    {
        playerMovenment = GameObject.FindAnyObjectByType<PlayerMovement>();   
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
