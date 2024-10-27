using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//
// Summary:
//     Handle all of the current level's gameObjects.
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] private GameObject pausedPanel;
    [SerializeField] private GameObject paperObject;
    [SerializeField] private GameObject lettersObject;
    [SerializeField] private Text txtTime;
    [SerializeField] private Text txtScore;
    [SerializeField] private Text txtLife;
    private Vector2 lastPosition = Vector2.zero;
    [SerializeField] private int totalLife = 10;
    private string time;
    private float second;
    private float timeToNewWord;
    private int totalScore;
    private int minute;
    private int secondInt;
    private bool isPaused;
    private bool isGameOver;
    private bool isGameWin;
    public GameObject LettersObject => lettersObject;
    public bool IsPausedGame { get { return isPaused; } set { isPaused = value; } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        txtLife.text = totalLife.ToString();
    }

    private void Start() => CreatePaperObject();

    private void Update()
    {
        timeToNewWord += Time.deltaTime;

        if (WordsRepository.Instance.Size() > 0 && timeToNewWord >= 3f)
        {
            CreatePaperObject();
            timeToNewWord = 0;
        }

        IsGameWin();
        SetTime();

        if (isPaused || isGameOver || isGameWin)
            PausedGame();
        else
            ResumeGame();

        if (!isGameOver && !isGameWin && Input.GetKeyDown(KeyCode.Pause))
        {
            pausedPanel.transform.Find("txtGamePaused").GetComponent<Text>().text = "Paused";
            isPaused = !isPaused;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paper"))
        {
            SetLife(-1);
            other.gameObject.GetComponent<PaperTyping>().enabled = false;
            Destroy(other.gameObject, 5f);
        }
    }

    // 
    // Summary:
    //     Instantiate a object Paper in scene.
    //
    // Parameters:
    //   position:
    //     Represent the object's position in scene.
    private void CreatePaperObject()
    {
        var newPaperObject = Instantiate(paperObject, GetRandomPosition(), paperObject.transform.rotation);
        PaperTyping.Instance = newPaperObject.GetComponent<PaperTyping>();

        if (PaperTyping.Instance.Word.Length >= 7)
            newPaperObject.transform.position = new Vector2(Random.Range(-3.67f, 1.79f), 5.87f);

        var width = PaperTyping.Instance.Word.Length * 0.8f;

        var frontalCanvas = newPaperObject.transform.Find("FrontalCanvas").gameObject;
        frontalCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 1.39f);

        var backCanvas = newPaperObject.transform.Find("BackCanvas").gameObject;
        backCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 1.39f);

        var imageCanvas = newPaperObject.transform.Find("ImageCanvas").gameObject;
        imageCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 1.01f);
    }

    // 
    // Summary:
    //     Update time of Game [00:00].
    private void SetTime()
    {
        second += Time.deltaTime;
        secondInt = (int)second;

        if (second >= 60)
        {
            minute++;
            second = 0;
        }

        time = (minute < 10 ? "0" + minute : minute) + ":" + (secondInt < 10 ? "0" + secondInt : secondInt);
        txtTime.text = time;
    }

    // 
    // Summary:
    //     To increase or to decrease scores quantity.
    //
    // Parameters:
    //   scoreQuantity:
    //     Represent the game's score quantity to increase or to decrease.
    public void SetScore(int scoreQuantity)
    {
        totalScore += scoreQuantity;
        txtScore.text = totalScore.ToString();
    }

    // 
    // Summary:
    //     To increase or to decrease life quantity of player.
    //
    // Parameters:
    //   lifeQuantity:
    //     Represent the player's life quantity to increase or to decrease.
    public void SetLife(int lifeQuantity)
    {
        totalLife += lifeQuantity;
        txtLife.text = totalLife >= 0 ? totalLife.ToString() : "0";
        IsGameOver(totalLife <= 0);
    }

    // 
    // Summary:
    //     Create a random position to paperObject.
    private Vector2 GetRandomPosition()
    {
        var tempPosition = new Vector2(Random.Range(-5.44f, 2.5f), 5.87f);
        if (lastPosition != tempPosition)
            lastPosition = tempPosition;
        else
            GetRandomPosition();

        return tempPosition;
    }

    private void IsGameOver(bool status)
    {
        if (status)
        {
            pausedPanel.transform.Find("txtGamePaused").GetComponent<Text>().text = "Game over";
            isPaused = true;
            isGameOver = true;
        }
    }

    private void IsGameWin()
    {
        if (WordsRepository.Instance.Size() == 0 && GameObject.FindGameObjectsWithTag("Paper").Length == 0)
        {
            pausedPanel.transform.Find("txtGamePaused").GetComponent<Text>().text = "You win";
            isGameWin = true;
        }
    }
    public void Restart()
    {
        isPaused = false;
        SceneManager.LoadScene("Scene_1");
    }

    private void PausedGame()
    {
        pausedPanel.SetActive(true);
        GetComponent<AudioSource>().mute = isPaused;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausedPanel.SetActive(false);
        GetComponent<AudioSource>().mute = false;
        Time.timeScale = 1;
    }
}
