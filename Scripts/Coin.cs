using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Coin : MonoBehaviour
{
    public float turnSpeed = 90f;

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null) {
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.name != "Player"){
            return;
        }

        GameManajer.inst.IncrementScore();

        Destroy(gameObject);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate( 0, 0, turnSpeed * Time.deltaTime);
    }
}
 