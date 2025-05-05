using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Variables related to temporary invincibility
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;
    
    //public InputAction LeftAction;
    public InputAction MoveAction;
    
    Rigidbody2D rigidbody2d;
    Vector2 move;
    
    // Variables related to the health system
    public int maxHealth = 5;
    internal int currentHealth;
    public int health { get { return currentHealth; }}
    
    public float playerSpeed = 3.0f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //LeftAction.Enable();
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        
        // two lines below: set the game to fixed 10 fps
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        
        currentHealth = maxHealth;
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
        
        // user input is being updated in each frame. But the actual move is being programmed in FixedUpdate() 
        move = MoveAction.ReadValue<Vector2>();
        
        //Debug.Log(move);
            
        // Old way to move the main character - without incorporating physics
        // Vector2 position = (Vector2)transform.position + move * 3f * Time.deltaTime; // "* Time.deltaTime" makes it FPS independent 
        // transform.position = position;
        
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * playerSpeed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
    
    public void ChangeHealth (int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
        }
        
        // prevents from changing currentHealth below 0 or above maxHealth
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
