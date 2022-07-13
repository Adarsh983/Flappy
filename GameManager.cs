using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    private BirdControls bc;
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject click;
    bool start;
    [SerializeField] private float coolDownTime;
    private float timer;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI highTxt;
    [SerializeField] private GameObject highScoreText;
    private bool gameEnded;
    [SerializeField] private Animator[] anims;



    // Start is called before the first frame update
    void Start()
    {   
        gameEnded = false;
        score = 0;
        bc = GameObject.FindGameObjectWithTag("Player").GetComponent<BirdControls>();
        Time.timeScale = 0f;
        start = true;
        timer = 0f;
        highTxt.text = PlayerPrefs.GetFloat("HighScore").ToString();
        scoreTxt.text = score.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(bc.jumpButton) && (start))
        {
            StartGame();
            start = false;
        }
        timer -= Time.deltaTime;
        
        if (Input.GetKeyDown(bc.jumpButton) && gameEnded)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].updateMode = AnimatorUpdateMode.UnscaledTime;
        }
        click.SetActive(false);
        highTxt.gameObject.SetActive(false);
        highScoreText.SetActive(false);
        scoreTxt.gameObject.SetActive(true);
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].updateMode = AnimatorUpdateMode.Normal;
        }
        click.SetActive(true);
        highTxt.gameObject.SetActive(true);
        scoreTxt.gameObject.SetActive(false);
        highScoreText.SetActive(true);
        click.GetComponent<TextMeshProUGUI>().text = "CLICK TO RESTART\n\nPRESS Q TO QUIT";
        gameEnded = true;
    }

    public void UpdateScore()
    {
        if (timer <= 0f)
        {
            score++;
            timer = coolDownTime;
        }
        if (PlayerPrefs.GetFloat("HighScore") < score)
        {
            PlayerPrefs.SetFloat("HighScore", score);
            highTxt.text = PlayerPrefs.GetFloat("HighScore").ToString();
        }
        scoreTxt.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
