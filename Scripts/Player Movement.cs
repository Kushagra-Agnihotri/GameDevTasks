using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rbd;
    public GameObject[] ground;
    public GameObject currentGround, Coin;
    public float _speed = 10f;
    private float Horizontal;
    private bool isgrounded;
    public float jumpPower = 20;

    public TMP_Text scoreText;
    public TMP_Text healthText;

    public GameObject gameOverScreen;
    public ParticleSystem crashParticles;


    private int score = 0;
    private int health = 100;
    private int highScore = 0;

    public Color hitColor = Color.yellow;
    private Color originalColor;
    private bool isSlowed = false;

    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        originalColor = GetComponent<Renderer>().material.color;
        UpdateScoreUI();
        UpdateHealthUI();
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;

        // Load high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update()
    {
        Movement();
        LaneSwitching();
        groundcheck();
        Jump();

        if (health <= 0)
        {
            GameOver();
        }
    }

    void Movement()
    {
        rbd.velocity = new Vector3(rbd.velocity.x, rbd.velocity.y, _speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Load")
        {
            currentGround = Instantiate(ground[Random.Range(0, ground.Length - 1)], currentGround.transform.position + new Vector3(0, 0, 20f), Quaternion.identity);
        }
        else if (other.tag == "Delete")
        {
            EnvControl env = other.GetComponentInParent<EnvControl>();
            GameObject Parent = env.Parent;
            Destroy(Parent, 0.5f);
        }
        else if (other.tag == "Coin")
        {
            Addscore(1);
            FindObjectOfType<AudioManager>().PlayCoinSound();
            Destroy(other.gameObject);
        }
        else if (other.tag == "Obstacle")
        {
            TakeDamage(10);
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().PlayCrashSound();
            PlayCrashParticles();
            StartCoroutine(HandleObstacleHit());
        }
    }
void PlayCrashParticles()
    {
        crashParticles.transform.position = transform.position + new Vector3(0,0,2);
        crashParticles.Play();
    }

    void LaneSwitching()
    {
        Horizontal = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(Horizontal * 10 * Time.deltaTime, 0, 0));
        var temppos = transform.position;
        temppos.x = Mathf.Clamp(transform.position.x, -1.5f, +1.5f);
        transform.position = temppos;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isgrounded == true)
        {
            rbd.velocity = new Vector3(0, jumpPower, 0);
            FindObjectOfType<AudioManager>().PlayJumpSound();
            isgrounded = false;
        }
    }

    void Addscore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;

        UpdateHealthUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateHealthUI()
    {
        healthText.text = "Health: " + health.ToString();
    }

    void GameOver()
    {
        FindObjectOfType<AudioManager>().StopBackgroundMusic(); // Stop background music
        FindObjectOfType<AudioManager>().PlayDeathSound();
        // Check for high score and save if needed
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);  // Save high score
            PlayerPrefs.Save();
        }
        
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        
        // Display scores on the Game Over screen
        EndScreen gameOverMenu = gameOverScreen.GetComponent<EndScreen>();
        gameOverMenu.DisplayScores(score, highScore);

    }


    void groundcheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20f))
        {
            if (hit.distance < 0.6f)
            {
                isgrounded = true;
            }
            else
            {
                isgrounded = false;
            }
        }
    }

    private IEnumerator HandleObstacleHit()
    {
        if (!isSlowed)
        {
            isSlowed = true;
            _speed /= 2;
            GetComponent<Renderer>().material.color = hitColor;

            yield return new WaitForSeconds(2f);

            _speed *= 2;
            GetComponent<Renderer>().material.color = originalColor;
            isSlowed = false;
        }
    }
}
