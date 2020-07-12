using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public bool gameOver;
    private int currentScore;
    private List<GameObject> numbers = new List<GameObject>();

    private void Awake()
    {
        gameOver = false;
        currentScore = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject numChild = transform.GetChild(i).gameObject;
            numbers.Add(numChild);
            numChild.SetActive(false);
        }
        
        numbers[currentScore].SetActive(true);
    }

    public void IncreaseScore() 
    {
        numbers[currentScore].SetActive(false);
        currentScore++;
        if (currentScore <= 11)
            numbers[currentScore].SetActive(true);
    }

    void Update()
    {
        if (currentScore == 11 && gameOver == false)
        {
            gameOver = true;
        }
            
    }
}
