using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    // ============ Distance ============
    private float[] endArr = {1820f, 1820f, 1820f};  // for "End coordinates" of each stage
    private float totalDistance = 0.0f;  // for displaying process rate
    
    // ============ Player ============
    public float forwardSpeed = 10.0f;
    private float initSpeed;
    public float horizontalStep = 10.0f;
    public float slideDuration = 1.0f;
    
    public float jumpForce = 50.0f;  // Related to jump action : implements more 'arcade-game-like' jump
    public float gravityMultiplier = 70.0f;
    
    public float hopForce = 10.0f;  // Related to hop action : when player moves left or right, it 'hops' naturally
    public float hopDuration = 0.5f;
    
    // ============ Player State ============
    private int currentLane = 3;
    private int currentStage = 1;

    private bool isJumping = false;
    private bool isMoving = false;
    public bool isSliding = false;
    private bool canDoubleJump = false;
    private bool hasCollided = false;

    private bool itemBoost = false;  // Item
    private bool itemFly = false;
    private bool itemDouble = false;
    
    private bool triggerJump = false;  // Input

    // ============ Game Control ============
    private int score = 0;
    private UIManager uiManager;
    private SceneController sceneController;
    private GameStateManager gameStateManager;
    private EffectManager effectManager;

    // ============ Player Control ============
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private Vector3 boxColliderCenter;
    private Vector3 boxColliderSize;
    private Animator animator;
    public ParticleSystem collisionParticle;

    private bool keyArrowAllowed = true;
    private bool keyWASDAllowed = false;

    void Start()  // Start is called before the first frame update
    {
        currentStage = GetCurrentStage();  // Setup
        totalDistance = GetTotalDistance();
        InitScore();
        SetKey();

        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        boxCollider = GetComponent<BoxCollider>();
        boxColliderCenter = boxCollider.center;
        boxColliderSize = boxCollider.size;

        animator = GetComponent<Animator>();

        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        sceneController = GameObject.Find("UIManager").GetComponent<SceneController>();
        gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        effectManager = GameObject.Find("EffectManager").GetComponent<EffectManager>();
    }

    void Update()  // Update is called once per frame
    {
        if(gameStateManager.GetState() == "GamePlay")
        {
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
            // RestrictTransform();

            bool isJumpKeyPressed = ((keyArrowAllowed && Input.GetKeyDown(KeyCode.UpArrow))
                || (keyWASDAllowed && Input.GetKeyDown(KeyCode.W)));
            bool isLeftKeyPressed = ((keyArrowAllowed && Input.GetKeyDown(KeyCode.LeftArrow))
                || (keyWASDAllowed && Input.GetKeyDown(KeyCode.A)));
            bool isRightKeyPressed = ((keyArrowAllowed && Input.GetKeyDown(KeyCode.RightArrow))
                || (keyWASDAllowed && Input.GetKeyDown(KeyCode.D)));
            bool isSlideKeyPressed = ((keyArrowAllowed && Input.GetKeyDown(KeyCode.DownArrow))
                || (keyWASDAllowed && Input.GetKeyDown(KeyCode.S)));

            bool canJump = (!isJumping || canDoubleJump) && !isMoving && !isSliding;
            bool canMoveLeft = (!isJumping || itemFly) && !isMoving && !isSliding && (currentLane > 1);
            bool canMoveRight = (!isJumping || itemFly) && !isMoving && !isSliding && (currentLane < 5);
            bool canSlide = !isJumping && !isMoving && !isSliding;

            if(isJumpKeyPressed && canJump)
            {
                triggerJump = true;
            }
            else if(isLeftKeyPressed && canMoveLeft)
            {
                StartCoroutine(MoveLeftSmooth());
            }
            else if(!isLeftKeyPressed && isRightKeyPressed && canMoveRight)
            {
                StartCoroutine(MoveRightSmooth());
            }
            else if(!isJumpKeyPressed & isSlideKeyPressed && canSlide)
            {
                StartCoroutine(Slide());
            }
        }
    }

    void FixedUpdate()
    {
        if(triggerJump)
        {
            Jump();
            triggerJump = false;
        }

        if(isJumping)
        {
            rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
        }

        uiManager.UpdateProgressBar(GetProcessRate());
    }

    private void Jump()
    {
        if(!isJumping)
        {
            isJumping = true;
            rb.velocity = Vector3.up * jumpForce;  // movement
            animator.SetTrigger("Jump_t");  // animation
            effectManager.PlayJumpSound();  // sound

            if(itemFly)
            {
                canDoubleJump = true;
            }
        } else if(canDoubleJump)
        {
            rb.velocity = Vector3.up * jumpForce;  // movement
            animator.SetTrigger("Jump_t");  // animation
            effectManager.PlayJumpSound();  // sound
            canDoubleJump = false;
        }
    }

    private IEnumerator MoveLeftSmooth()
    {
        isMoving = true;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.forward * horizontalStep;
        float elapsedTime = 0.0f;
        // rb.velocity = new Vector3(rb.velocity.x, hopForce, rb.velocity.z);

        while(elapsedTime < hopDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / hopDuration;

            t = t * t * (3.0f - 2.0f * t);
            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }

        transform.position = endPos;
        currentLane--;
        isMoving = false;
    }

    private IEnumerator MoveRightSmooth()
    {
        isMoving = true;
        
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.back * horizontalStep;
        float elapsedTime = 0.0f;

        while(elapsedTime < hopDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / hopDuration;

            t = t * t * (3.0f - 2.0f * t);
            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }

        transform.position = endPos;
        currentLane++;
        isMoving = false;
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);  // movement
        boxCollider.center = new Vector3(boxColliderCenter.x, 0.4f, boxColliderCenter.z);
        boxCollider.size = new Vector3(boxColliderSize.x, 1.5f, boxColliderSize.z);
        animator.SetTrigger("Slide_t");  // animation
        effectManager.PlaySlideSound();  // sound
        
        yield return new WaitForSeconds(slideDuration);

        transform.position = new Vector3(transform.position.x, 0.9f, transform.position.z);
        boxCollider.center = boxColliderCenter;
        boxCollider.size = boxColliderSize;
        isSliding = false;
    }

    // ========================================== Collision ==========================================
    private void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "Obstacle" ||
            collider.gameObject.transform.parent != null && collider.gameObject.transform.parent.tag == "Obstacle") 
        {
            if(!itemBoost && !hasCollided)  // Game Over
            {
                hasCollided = true;
                animator.SetBool("Collide_b", true);  // animation
                effectManager.PlayGameOverSound();  // sound
                collisionParticle.Play();  // particle
                gameStateManager.EnterGameOverState();
            } else if(!hasCollided)  // Item Boost
            {
                Destroy(collider.gameObject);
            }
        }
        
        if (collider.gameObject.CompareTag("Ground") && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            isJumping = false;
        }
    }

    // ========================================== Functions ==========================================
    private int GetCurrentStage()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if(char.IsDigit(sceneName[5]))
        {
            return sceneName[5] - '0';
        }
        return 0;
    } 

    private float GetTotalDistance()
    {
        float distance = 0.0f;
        for(int i = 0; i < 3; i++)
        {
            distance += endArr[i];
        }
        return distance;
    }

    public float GetCurrentCoordinate()
    {
        return transform.position.x;
    }

    public float GetProcessRate()
    {
        float currentXCoordinate = transform.position.x;
        CheckStageCleared(currentXCoordinate);
        switch(currentStage)
        {
            case 1:
                return (currentXCoordinate) / totalDistance;
            case 2:
                return (currentXCoordinate + endArr[0]) / totalDistance;
            case 3:
                return (currentXCoordinate + endArr[1] + endArr[0]) / totalDistance;
            default:
                return 0;
        }
    }

    public void CheckStageCleared(float currentXCoordinate)
    {
        if(currentXCoordinate > endArr[currentStage - 1])  // Stage Clear
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            PlayerPrefs.SetInt("Score", score);
            animator.SetTrigger("Jump_t");  // animation
            animator.SetFloat("Speed_f", 0.0f);
            effectManager.PlayGameClearSound();  // sound
            gameStateManager.EnterStageClearState();
        }
    }

    public void SetSpeed(float speed)
    {
        forwardSpeed = speed;
    }
    
    public void InitScore()
    {
        score = PlayerPrefs.GetInt("Score");
    }

    public void AddScore()
    {
        if(itemDouble)
        {
            score += 2;
        } else
        {
            score += 1;
        }
        uiManager.UpdateScoreText(score);
    }

    public int GetScore() 
    {
        return score;
    }

    public void RemoveEffects()
    {
        foreach(Transform obj in GameObject.Find("Duck").transform)
        {
            if(obj.name == "TejavaParticle" || obj.name == "BoostParticle" || obj.name == "BoostParticleRun"
                || obj.name == "FlyParticle" || obj.name == "DoubleParticle")
            {
                Destroy(obj.gameObject);
            }
        }
    }

    // ========================================== Item ==========================================
    public void BoostOn()
    {
        itemBoost = true;
        initSpeed = forwardSpeed;
        SetSpeed(2.0f * forwardSpeed);
        animator.SetFloat("Speed_f", 20);
    }

    public void BoostOff()
    {
        itemBoost = false;
        SetSpeed(initSpeed);
        animator.SetFloat("Speed_f", 10);
    }

    public void FlyOn()
    {
        itemFly = true;
        canDoubleJump = true;
    }

    public void FlyOff()
    {
        itemFly = false;
        canDoubleJump = false;
    }

    public void DoubleOn()
    {
        itemDouble = true;
    }

    public void DoubleOff()
    {
        itemDouble = false;
    }
    
    // ========================================== Key ==========================================
    public void SetKey()
    {
        if(PlayerPrefs.GetString("key") != "WASD")
        {
            ActivateKeyArrow();
        } else
        {
            ActivateKeyWASD();
        }
    }

    public void ActivateKeyArrow()
    {
        keyArrowAllowed = true;
        keyWASDAllowed = false;
        PlayerPrefs.SetString("key", "Arrow");
    }

    public void ActivateKeyWASD()
    {
        keyWASDAllowed = true;
        keyArrowAllowed = false;
        PlayerPrefs.SetString("key", "WASD");
    }
}