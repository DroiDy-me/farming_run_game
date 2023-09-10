using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerControl : MonoBehaviour
{

    //palyer model variables
    [SerializeField] internal GameObject[] player = new GameObject[3];
    internal Rigidbody playerRb;
    internal Animator playerAnim;
    [SerializeField] internal ParticleSystem explosion;
    [SerializeField] internal ParticleSystem dirt;
    [SerializeField] internal AudioClip jumpSound;
    [SerializeField] internal AudioClip crashSound;
    [SerializeField] internal GameObject GameOverScreen;
    [SerializeField] internal GameObject pauseScreen;
    [SerializeField] internal Slider volumeSlider;
    [SerializeField] internal GameObject start;
    [SerializeField] internal float lerpSpeed;
    [SerializeField] internal TextMeshProUGUI scoreTextEnd;

    //pause variables
    private bool is_paused;

    //starting animation variables
    private Vector3 startPos;

   //sounds
    private AudioSource playerSounds;
    public AudioSource music;
    private AudioClip explSound;



    //moving variables 
    private float jumpforce = 500;
    private float gravityModifier = 1.5f;
    protected bool doubleJump;
    [SerializeField] public float speed = 10;
    [HideInInspector] public bool gameOver = false;
    [HideInInspector] protected bool isOnGround = true;

    //other
    [HideInInspector] public float Score;

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;

        for (int i = 0; i < player.Length; i++)
        {
            player[i].SetActive(true);
            if(i != GameManager.charecterPicked)
            {
                Destroy(player[i].gameObject);
            }
        }

        setComponents();

        Physics.gravity *= gravityModifier;
        gameOver = true;
        StartCoroutine(PlayIntro());
        Time.timeScale = 1;
    }


    // Update is called once per frame
    internal void Update()
    {
        Score += speed / 100 * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJump)
        {
            seccondJump();
        }
        //playerAnim.SetFloat("Speed_f", speed / 10);
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            pause();
        }
    }

    protected virtual IEnumerator PlayIntro()
    {
        startPos = gameObject.transform.position;
        Vector3 endPos = start.transform.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        playerAnim.SetFloat("Speed_f", 0.5f);
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            gameObject.transform.position = Vector3.Lerp(startPos, endPos,
            fractionOfJourney);
            yield return null;
        }
        playerAnim.SetFloat("Speed_f", 1.0f);
        gameOver = false;
    }

    private void setComponents()
    {
        playerAnim = GetComponentInChildren<Animator>();
        music = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        playerSounds = gameObject.GetComponent<AudioSource>();
        explSound = crashSound;
    }

    internal void jump()
    {
        playerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        isOnGround = false;
        dirt.Stop();
        playerAnim.SetTrigger("Jump_trig");
        playerSounds.PlayOneShot(jumpSound, 1.0f);
        doubleJump = false;
    }

    internal void seccondJump()
    {
        doubleJump = true;
        playerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        playerAnim.Play("Running_Jump", 3, 0f);
        playerSounds.PlayOneShot(jumpSound, 1.0f);
    }

    protected void pause()
    {
        is_paused = !is_paused;
        if (is_paused)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
        pauseScreen.SetActive(is_paused);
        volumeSlider.value = GameManager.volume;
    }

    internal void fall()
    {
        dirt.Play();
        isOnGround = true;
    }

    internal void GameOver()
    {
        gameOver = true;
        Debug.Log("Game Over!");
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);
        dirt.Stop();
        explosion.Play();
        music.Stop();
        playerSounds.PlayOneShot(explSound, 1.0f);
        GameManager.scoreUpdate(System.DateTime.Now.ToString("hh:mm") + " " +
                                    System.DateTime.Now.ToString("MM/dd/yyyy") + ": " +
                                    Mathf.Round(Score * 100f) / 100f, Score);
        GameOverScreen.SetActive(true);
        scoreTextEnd.text = Mathf.Round(Score * 100f) / 100f + "";
        GameManager.save();
    }

    public void volumeChange()
    {
        GameManager.volume = volumeSlider.value;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void backToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}