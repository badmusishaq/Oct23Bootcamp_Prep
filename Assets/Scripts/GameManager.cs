using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private Pin[] pins;

    private bool isGamePlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        isGamePlaying = true;

        //Get the first throw
        player.StartThrow();
    }

    public void SetNextThrow()
    {
        Invoke("NextThrow", 3.0f);
    }

    void NextThrow()
    {
        int fallenPins = CalculateFallenPins();
        scoreManager.SetFrameScore(fallenPins);

        if(scoreManager.currentFrame == 0)
        {
            uiManager.ShowGameOver(scoreManager.CalculateTotalScore());
            //Debug.Log($"Game over. Your total score is {scoreManager.CalculateTotalScore()}");
            return;
        }

        //Calculate the frame total from UI
        int frameTotal = 0;
        for(int i = 0; i < scoreManager.currentFrame - 1; i++)
        {
            frameTotal += scoreManager.GetFrameScores()[i];
            uiManager.SetFrameTotal(i, frameTotal);
        }

        player.StartThrow();

        /*else
        {
            Debug.Log($"Frame {scoreManager.currentFrame} - Throw {scoreManager.currentThrow}");
            scoreManager.SetFrameScore(CalculateFallenPins());
            Debug.Log($"Current score : {scoreManager.CalculateTotalScore()}");

            player.StartThrow();
        }*/
    }

    public void ResetAllPins()
    {
        foreach(Pin p in pins)
        {
            p.ResetPin();
        }
    }

    public int CalculateFallenPins()
    {
        int count = 0;

        foreach(Pin pin in pins)
        {
            if(pin.isFallen && pin.gameObject.activeSelf)
            {
                count++;
                pin.gameObject.SetActive(false);
            }
        }

        Debug.Log($"Total fallen pins = {count}");
        return count;
    }
}
