using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class gameController : MonoBehaviour
{

    public GameObject statusText;
    public GameObject ballObj;
    TMP_Text statusTextMesh;
    public static int lives = 3;
    public static int score = 0;
    public static int bricksAlive = 20;
    public static GameState CurrentGameState = GameState.start;
    public static ball ball;
    private GameObject[] blocks;
    public enum GameState
    {
        start,
        playing,
        victory,
        lostAllLives
    }

    //Init
    void Start() {
        blocks = GameObject.FindGameObjectsWithTag("brick");
        ball = ballObj.GetComponent<ball>();
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        statusTextMesh = statusText.GetComponent<TMP_Text>();

        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.start:
                if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0)
                {
                    statusTextMesh.text = string.Format("SCORE: {0}  LIVES: {1}", score, lives);
                    CurrentGameState = GameState.playing;
                    ball.startBall();
                }
                break;
            case GameState.playing:
                statusTextMesh.text = string.Format("SCORE: {0}  LIVES: {1}", score, lives);
                if(bricksAlive == 0) {
                    CurrentGameState = GameState.victory;
                }
                break;
            case GameState.victory:
                ball.stopBall();
                statusTextMesh.text = string.Format("LEVEL CLEARED. Tap to advance");
                if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0)
                {
                    Restart();
                    ball.speedY += 2;
                    ball.startBall();
                    statusTextMesh.text = string.Format("SCORE: {0}  LIVES: {1}", score, lives);
                    CurrentGameState = GameState.playing;
                }
                break;
            case GameState.lostAllLives:
                if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0)
                {
                    Restart();
                    ball.startBall();
                    statusTextMesh.text = string.Format("SCORE: {0}  LIVES: {1}", score, lives);
                    CurrentGameState = GameState.playing;
                }
                break;
            default:
                break;
        }
    }

    private void Restart()
    {
        bricksAlive = 0;
        foreach (var item in blocks)
        {
            bricksAlive++;
            item.SetActive(true);
        }
        lives = 3;
        score = 0;
    }


    public void decreaseLives()
    {
        if (lives > 0) {
            lives--;
        }

        if(lives == 0) {
            statusTextMesh.text = "GAME OVER. Tap to restart";
            CurrentGameState = GameState.lostAllLives;
        }
    }
}
