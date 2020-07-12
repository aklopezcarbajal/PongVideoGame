using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private float speed;
    private float horizontalLimit;

    public bool gameOn;
    private PlayerController player1, player2;

    public AudioClip hit;
    public AudioClip miss;
    private AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 500.0f;
        horizontalLimit = 10.0f;
        audioSource = GetComponent<AudioSource>();
    }

    Vector2 getRandomDirection()
    {
        int signedX, signedY;
        signedX = ((Random.Range(1, 3) % 2) == 0) ? 1 : -1;
        signedY = ((Random.Range(1, 3) % 2) == 0) ? 1 : -1;

        Vector2 randomDirection = new Vector2(signedX * Random.Range(0.4f, 0.6f), signedY * Random.Range(0.4f, 0.6f));
        randomDirection.Normalize();

        return randomDirection;
    }

    private void Start()
    {
        gameOn = true;

        direction = getRandomDirection();
        rb.AddForce(direction * speed);

        player1 = (PlayerController)GameObject.Find("Player1").GetComponent(typeof(PlayerController));
        player2 = (PlayerController)GameObject.Find("Player2").GetComponent(typeof(PlayerController));
    }

    void ResetPosition()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = new Vector2( 0f, Random.Range(-3f, 3f) );
        rb.constraints = RigidbodyConstraints2D.None;

        direction = getRandomDirection(); 
        rb.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            audioSource.PlayOneShot(hit, 0.5f);
        }
    }

    void Update()
    {
        //Ball outside limits
        if ((transform.position.x > horizontalLimit || transform.position.x < (-1)*horizontalLimit) && gameOn)
        {
            //Update Score
            if (transform.position.x > horizontalLimit)             //Goal for Player1
            {
                Score scoreP1 = (Score)GameObject.Find("ScoreP1").GetComponent(typeof(Score));
                scoreP1.IncreaseScore();
            }
            
            else if (transform.position.x < (-1) * horizontalLimit) //Goal for Player2
            {
                Score scoreP2 = (Score)GameObject.Find("ScoreP2").GetComponent(typeof(Score));
                scoreP2.IncreaseScore();
            }
            
            //Play Miss clip
            audioSource.PlayOneShot(miss, 0.5f);
            //Reset position of players and ball
            ResetPosition();
            player1.ResetPosition();
            player2.ResetPosition();
        }
    }
}
