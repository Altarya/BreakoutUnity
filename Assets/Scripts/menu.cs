using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void loadGame() {
        Debug.Log("Loading Game!");
        FMODUnity.RuntimeManager.CreateInstance("event:/boop").start();
        SceneManager.LoadScene(sceneName: "gaem"); 
    }
}
