using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;

[System.Serializable]
public class SaveData
{
    public int highScore;
}

public class gameController : MonoBehaviour
{

    public GameObject statusText;
    public GameObject ballObj;
    TMP_Text statusTextMesh;
    public static int lives = 3;
    public static int score = 0;
    public static SaveData save;
    public static int bricksAlive = 20;
    public static GameState CurrentGameState = GameState.start;
    public static ball ball;
    private GameObject[] bricks;
    public enum GameState
    {
        start,
        playing,
        victory,
        lostAllLives
    }

    //Init
    void Start() {
        bricks = GameObject.FindGameObjectsWithTag("brick");
        ball = ballObj.GetComponent<ball>();
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        statusTextMesh = statusText.GetComponent<TMP_Text>();

        save = new SaveData();

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
                    FMODUnity.RuntimeManager.CreateInstance("event:/win").start();
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
                ball.stopBall();
                if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0)
                {
                    Restart();
                    LoadData();
                    BinaryFormatter formatter = new BinaryFormatter();
                    string path = Application.persistentDataPath + "/breakout.save";

                    FileStream stream = new FileStream(path, FileMode.Create);

                    if (save.highScore < score)
                    {
                        save.highScore = score;
                    }
                    Debug.Log("High Score: " + save.highScore);

                    formatter.Serialize(stream, save);
                    stream.Close();
                    score = 0;
                    ball.speedY = 3;
                    ball.startBall();
                    statusTextMesh.text = string.Format("SCORE: {0}  LIVES: {1}", score, lives);
                    CurrentGameState = GameState.playing;
                }
                break;
            default:
                break;
        }
    }

    private void Restart() {
        bricksAlive = 0;
        foreach (var item in bricks)
        {
            bricksAlive++;
            item.SetActive(true);
        }
        lives = 3;
    }

    public static void LoadData() {
        string path = Application.persistentDataPath + "/breakout.save";

        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            save = formatter.Deserialize(stream) as SaveData;

            stream.Close();

        } else {
            Debug.LogError("Error: Save file not found in " + path);
        }
    }

    public void decreaseLives()
    {
        if (lives > 0) {
            lives--;
        }

        if(lives == 0) {
            statusTextMesh.text = "GAME OVER. Tap to restart";
            CurrentGameState = GameState.lostAllLives;
            FMODUnity.RuntimeManager.CreateInstance("event:/lose").start();
        }
    }
}
