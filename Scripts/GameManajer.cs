using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManajer : MonoBehaviour
{

    int score;
    public static GameManajer inst;

    [SerializeField] Text scoreText;

    [SerializeField] PlayerMovement playerMovement;

    public void IncrementScore()
    {
        score++;
        scoreText.text = "Score : " + score;

        playerMovement.speed += playerMovement.speedIncreasePerPoint;
    }

    void Start()
    {
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
