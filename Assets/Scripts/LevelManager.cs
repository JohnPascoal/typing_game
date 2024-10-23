using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//
// Summary:
//     Control all of the current level's gameObjects.
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] private GameObject paperObject;
    [SerializeField] private GameObject lettersObject;
    [SerializeField] private Text txtTime;
    [SerializeField] private Text txtScore;
    [SerializeField] private Text txtLife;
    private readonly List<Vector2> positions = new(){
        new(-5.44f, 5.87f), new(-2.89f, 5.87f), new(0.37f, 5.87f), new(2.5f, 5.87f)
    };
    private string time;
    private float second;
    private float timeToNewWord;
    private int totalScore;
    private int minute, secondInt;
    private int totalLife = 10;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject LettersObject
    {
        get { return lettersObject; }
    }

    private void Start()
    {
        CreatePaperObject(positions[Random.Range(0, positions.Count)]);
    }

    private void Update()
    {
        timeToNewWord += Time.deltaTime;

        if (WordsRepository.Instance.Repository.Count > 0 && timeToNewWord >= 3f)
        {
            CreatePaperObject(positions[Random.Range(0, positions.Count)]);
            timeToNewWord = 0;
        }
        SetTime();

    }

    // 
    // Summary:
    //     Instantiate a object Paper in scene.
    //
    // Parameters:
    //   position:
    //     Represent the object's position in scene.
    private void CreatePaperObject(Vector2 position)
    {
        var newPaperObject = Instantiate(paperObject, position, paperObject.transform.rotation);
        TypingControl.Instance = newPaperObject.GetComponent<TypingControl>();
        var width = TypingControl.Instance.Word.Length * 0.8f;

        var frontalCanvas = newPaperObject.transform.Find("FrontalCanvas").gameObject;
        frontalCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 1.39f);

        var backCanvas = newPaperObject.transform.Find("BackCanvas").gameObject;
        backCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 1.39f);

        var imageCanvas = newPaperObject.transform.Find("ImageCanvas").gameObject;
        imageCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 1.01f);
    }

    // 
    // Summary:
    //     Game's time [00:00].
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
        txtLife.text = totalLife.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paper"))
        {
            SetLife(-1);
            other.gameObject.GetComponent<TypingControl>().enabled = false;
            Destroy(other.gameObject, 5f);
        }
    }

}
