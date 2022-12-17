using UnityEngine;
using System.Collections;

public class brick : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.1f);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnCollisionExit2D(Collision2D col)
    {
        gameObject.SetActive(false);
        gameController.Score += 20;
        gameController.BlocksAlive--;
    }
}
