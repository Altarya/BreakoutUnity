using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UnityEngine;
using static SaveData;

public class highScore : MonoBehaviour
{

    public GameObject HSObj;

    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/breakout.save";
        SaveData highScore = new SaveData();

        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            highScore = formatter.Deserialize(stream) as SaveData;

            stream.Close();

        } else {
            Debug.LogError("Error: Save file not found in " + path);
        }

        HSObj.GetComponent<TMP_Text>().text = new string("High Score: "+highScore.highScore);
    }
}
