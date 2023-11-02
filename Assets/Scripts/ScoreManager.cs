using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private int totalScore;

    public int currentFrame { get; private set; }
    public int currentThrow { get; private set; }

    private int[] frames = new int[10];

    private bool isSpare = false;
    private bool isStrike = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetScore();
    }

    public void SetFrameScore(int score)
    {
        //Ball 1
        if(currentThrow == 1)
        {
            frames[currentFrame - 1] += score;

            if(isSpare)
            {
                frames[currentFrame - 2] += score;
                isSpare = false;
            }

            if(score == 10)
            {
                if(currentFrame == 10)
                {
                    currentThrow++; //waiting for ball 2 to throw
                }
                else
                {
                    isStrike = true;
                    currentFrame++; // moving on to the next frame
                }

                gameManager.ResetAllPins();
            }
            else
            {
                currentThrow++; // waiting for the ball 2
            }

            return;
        }

        //Ball 2
        if(currentThrow == 2)
        {
            frames[currentFrame - 1] += score;

            if(isStrike)
            {
                frames[currentFrame - 2] += frames[currentFrame - 1];
                isStrike = false;
            }

            if(frames[currentFrame - 1] == 10) //is total score for the frame = 10?
            {
                if(currentFrame == 10)
                {
                    currentThrow++; // wait for ball 3 since this is the last frame.
                }
                else
                {
                    isSpare = true;
                    currentFrame++;
                    currentThrow = 1;
                }
            }
            else
            {
                if(currentFrame == 10)
                {
                    //End of the throws
                    currentThrow = 0;
                    currentFrame = 0;
                }
                else
                {
                    currentFrame++;
                    currentThrow = 1;
                }
            }

            gameManager.ResetAllPins();

            return;
        }

        //Ball 3 - if this is frame 10
        if(currentThrow == 3 && currentFrame == 10)
        {
            frames[currentFrame - 1] += score;

            //End of all the throws
            currentFrame = 0;
            currentThrow = 0;

            return;
        }
    }

    public int CalculateTotalScore()
    {
        totalScore = 0;
        foreach(var item in frames)
        {
            totalScore += item;
        }

        return totalScore;
    }

    private void ResetScore()
    {
        totalScore = 0;
        currentFrame = 1;
        currentThrow = 1;
        frames = new int[10];
    }
}
