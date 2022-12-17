using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class gameController : MonoBehaviour
{

    public GameObject statusText;

    TMP_Text statusTextMesh;

    //Init
    void Start() {
        blocks = GameObject.FindGameObjectsWithTag("brick");
        ball = GameObject.Find("ball").GetComponent<ball>();
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        statusTextMesh = statusText.GetComponent<TMP_Text>();

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
                    statusTextMesh.text = string.Format("SCORE: {0}  LIVES: {1}", Score, Lives);
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
                    statusTextMesh.text = string.Format("SCORE: {0}  LIVES: {1}", Score, Lives);
                    CurrentGameState = GameState.Playing;
                }
                break;
            case GameState.LostALife:
                if (InputTaken())
                {
                    ball.Startball();
                    statusTextMesh.text = string.Format("SCORE: {0}  LIVES: {1}", Score, Lives);
                    CurrentGameState = GameState.Playing;
                }
                break;
            case GameState.LostAllLives:
                if (InputTaken())
                {
                    Restart();
                    ball.Startball();
                    statusTextMesh.text = string.Format("SCORE: {0}  LIVES: {1}", Score, Lives);
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
            statusTextMesh.text = "GAME OVER. Tap to play again";
            CurrentGameState = GameState.LostAllLives;
        }
        else
        {
            statusTextMesh.text = "Lost a life. Tap to continue";
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
