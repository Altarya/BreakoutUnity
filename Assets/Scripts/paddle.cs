using UnityEngine;
using System.Collections;
 using UnityEngine.InputSystem.EnhancedTouch;

public class paddle : MonoBehaviour
{

    public gameController camera;

    // Use this for initialization
    void Start()
    {
        EnhancedTouchSupport.Enable();
        if(EnhancedTouchSupport.enabled) {
            Debug.Log("Touch Enabled");
        }
        TouchSimulation.Enable();
        if (TouchSimulation.instance.enabled) {
            Debug.Log("Touch Simulation Enabled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float input = 0.0f;

        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeFingers.Count  > 0) {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.InputSystem.EnhancedTouch.Touch.fingers[0].screenPosition.x, Screen.height)).x, transform.position.y, transform.position.z);
        } else {
            input = Input.GetAxis("Horizontal");
            transform.Translate(new Vector2(input * speed * Time.deltaTime, 0));
            float currentX = Mathf.Clamp(transform.position.x, LeftBlockTransform.position.x + 1, RightBLockTransform.position.x - 1);
            transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
        }

        
    }

    public Transform LeftBlockTransform, RightBLockTransform;
    public float speed = 50;
     
}
