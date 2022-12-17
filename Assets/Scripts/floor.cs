using UnityEngine;
using System.Collections;

public class floor : MonoBehaviour
{
    public gameController gameController;

    private void Start() {
        GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.1f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ball")
            gameController.DecreaseLives();
    }
}
