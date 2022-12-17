using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class gameController : MonoBehaviour
{

    Text statusText;

    //Init
    void Start() {
        blocks = GameObject.FindGameObjectsWithTag("brick");
        ball = GameObject.Find("ball").GetComponent<ball>();
        statusText = GameObject.Find("statusIndicator").GetComponent<Text>();
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");

        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.1f);
        }
    }
    private bool InputTaken() {

        return UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0;
    }


    // Update is called once per frame
    void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Start:
                if (InputTaken())
                {
                    statusText.text = string.Format("Lives: {0}  Score: {1}", Lives, Score);
                    CurrentGameState = GameState.Playing;
                    ball.Startball();
                }
                break;
            case GameState.Playing:
                break;
            case GameState.Won:
                if (InputTaken())
                {
                    Restart();
                    ball.Startball();
                    statusText.text = string.Format("Lives: {0}  Score: {1}", Lives, Score);
                    CurrentGameState = GameState.Playing;
                }
                break;
            case GameState.LostALife:
                if (InputTaken())
                {
                    ball.Startball();
                    statusText.text = string.Format("Lives: {0}  Score: {1}", Lives, Score);
                    CurrentGameState = GameState.Playing;
                }
                break;
            case GameState.LostAllLives:
                if (InputTaken())
                {
                    Restart();
                    ball.Startball();
                    statusText.text = string.Format("Lives: {0}  Score: {1}", Lives, Score);
                    CurrentGameState = GameState.Playing;
                }
                break;
            default:
                break;
        }
    }

    private void Restart()
    {
        foreach (var item in blocks)
        {
            item.SetActive(true);
            //item.GetComponent<brick>().InitializeColor();
        }
        Lives = 3;
        Score = 0;
    }


    public void DecreaseLives()
    {
        if (Lives > 0) {
            Lives--;
        }

        if(Lives == 0)
        {
            statusText.text = "Lost all lives. Tap to play again";
            CurrentGameState = GameState.LostAllLives;
        }
        else
        {
            statusText.text = "Lost a life. Tap to continue";
            CurrentGameState = GameState.LostALife;
        }
        ball.Stopball();
    }

    public static int Lives = 3;
    public static int Score = 0;
    public static int BlocksAlive = 20;
    public static GameState CurrentGameState = GameState.Start;

    public static ball ball;
    private GameObject[] blocks;

    public enum GameState
    {
        Start,
        Playing,
        Won,
        LostALife,
        LostAllLives
    }
}
