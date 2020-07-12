using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private List<GameObject> players;
    private Score[] playerScore;
    private Scene currentScene;
    private BallController ball;

    void Start()
    {
        players = new List<GameObject>();
        players.Add(GameObject.Find("Player1"));
        players.Add(GameObject.Find("Player2"));

        playerScore = FindObjectsOfType<Score>();
        currentScene = SceneManager.GetActiveScene();
        ball = FindObjectOfType<BallController>();
    }

    void Update()
    {
        if (currentScene.name == "StartScene" && Input.GetKeyDown("space"))     //Start Scene
        {
            SceneManager.LoadScene("GameScene");
        }

        if(currentScene.name == "GameScene")                                    //Game Scene
        {
            foreach (Score score in playerScore)
            {
                if (score.gameOver)
                {
                    ball.gameOn = false;
                    foreach(GameObject player in players)
                    {
                        player.SetActive(false);
                    }
                }
            }
        }
    }
}