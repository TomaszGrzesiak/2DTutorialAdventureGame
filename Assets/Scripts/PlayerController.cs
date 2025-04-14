using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //public InputAction LeftAction;
    public InputAction MoveAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //LeftAction.Enable();
        MoveAction.Enable();
        // two lines below: set the game to fixed 10 fps
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        /* below an old, simplistic implementation of player controlling input
        float horizontal = 0.0f;
        if (LeftAction.IsPressed()) horizontal = -1.0f; 
        else if (Keyboard.current.rightArrowKey.isPressed) horizontal = 1.0f;
        
        float vertical = 0.0f;
        if (Keyboard.current.upArrowKey.isPressed) vertical = 1.0f;
        else if (Keyboard.current.downArrowKey.isPressed) vertical = -1.0f;
        
        Vector2 position = transform.position;
        position.x = position.x + 0.1f * horizontal;
        position.y = position.y + 0.1f * vertical;
        transform.position = position;
        */
        // below a more scalable example of player controlling input 
        Vector2 move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
        Vector2 position = (Vector2)transform.position + move * 3f * Time.deltaTime; // "* Time.deltaTime" makes it FPS independent 
        transform.position = position;
    }
}
