using UnityEngine;
using System.Collections;

public class brick : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.1f);
    }

    void OnCollisionExit2D(Collision2D col) {
        gameController.score += 1;
        gameController.bricksAlive--;
        gameObject.SetActive(false);
    }
}
