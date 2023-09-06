using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerControl : MonoBehaviour
{
    [SerializeField] private GameObject[] player = new GameObject[3];
    [HideInInspector] private Rigidbody playerRb;
    [SerializeField] public ParticleSystem explosionParticle;
    [SerializeField] public ParticleSystem dirtParticles;

    //pause variables
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Slider volumeSlider;
    private bool is_paused;

    //starting animation variables
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    [SerializeField] private float lerpSpeed;

    //sounds
    [HideInInspector] public AudioSource playerSounds;
    [HideInInspector] public AudioSource music;
    [SerializeField] public AudioClip jumpSound;
    [SerializeField] public AudioClip crashSound;

    [SerializeField] public GameObject GameOver;
    [SerializeField] public TextMeshProUGUI scoreTextEnd;

    public float jumpforce = 500;
    private float gravityModifier = 1;
    private bool doubleJump;

    [HideInInspector] public bool gameOver = false;

    [HideInInspector] public bool isOnGround = true;

    [HideInInspector] public Animator playerAnim;

    [HideInInspector] public float Score;
    [HideInInspector] public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < player.Length; i++)
        {
            player[i].SetActive(false);
        }
        player[GameManager.charecterPicked].SetActive(true);

        playerRb = player[GameManager.charecterPicked].GetComponent<Rigidbody>();
        music = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        playerSounds = GetComponent<AudioSource>();

        playerAnim = GetComponentInChildren<Animator>();

        Physics.gravity *= gravityModifier;
        gameOver = true;
        StartCoroutine(PlayIntro());
    }

    

    // Update is called once per frame


    void Update()
    {
        Score += speed / 100 * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            isOnGround = false;
            dirtParticles.Stop();
            playerAnim.SetTrigger("Jump_trig");
            playerSounds.PlayOneShot(jumpSound, 1.0f);
            doubleJump = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJump)
        {
            doubleJump = true;
            playerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
            playerSounds.PlayOneShot(jumpSound, 1.0f);
        }
        playerAnim.SetFloat("Speed_f", speed / 10);
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            pause();
        }
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = gameObject.transform.position;
        Vector3 endPos = start.transform.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        playerAnim.SetFloat("Speed_f",
        0.5f);
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

    private void pause()
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
        SceneManager.LoadScene(0);
    }
}