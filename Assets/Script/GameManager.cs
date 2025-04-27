using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour

{
  
 
  private Spawner spawner;
  public static float speed = 5f;
   private static int pipeCount = 0; 
   private float leftEdge;
   public Player player;
   public Text scoreText;
   public GameObject playButton;

   public GameObject gameOver;
   private int score;

   AudioManager audioManager;
   public static GameManager Instance { get; private set; }

   private void Awake()
   {
    
    audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    Application.targetFrameRate = 60;
    Pause();
   }
 private void Start()
    {
      Spawner[] spawner = FindObjectsByType<Spawner>(FindObjectsSortMode.None);
    }
 public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
 
   
   public void Play()
   {
    audioManager.PlaySFX(audioManager.swooshing);
    score = 0 ;
    UpdateScoreText();

    playButton.SetActive(false);

    gameOver.SetActive(false);

    Time.timeScale = 1f;
    player.enabled = true;

    Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);
    
    for (int i = 0 ; i < pipes.Length ; i ++)
    {
      Destroy(pipes[i].gameObject);
    }
   }

  
   public void Pause ()
   {

    Time.timeScale = 0f;
    player.enabled = false;
   }
   public static void ResetSpeed()
 {
    speed = 5f; // Resets the speed to the initial value
    pipeCount = 0; // Resets the pipe counter to 0
 }

   public void GameOver()
   {
    Pipes.ResetSpeed();
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    audioManager.PlaySFX(audioManager.death);
    gameOver.SetActive(true);
    playButton.SetActive(true);
    
    Pause();
   }

   public void IncreaseScore()
   {
    audioManager.PlaySFX(audioManager.point);
    score ++  ;
    scoreText.text = score.ToString();
   }
}




