using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text brickText;
    private int brickCount;
    public GameObject ball;

    void Start()
    {
    
    }

    
    void Update()
    {
        brickCount = GameObject.FindGameObjectsWithTag("Brick").Length;
        brickText.text = "BRICKS: "+ brickCount;

        if (brickCount ==0){
            FindObjectOfType<GameManager>().winner();
            Destroy(ball);
        }
    }
}
