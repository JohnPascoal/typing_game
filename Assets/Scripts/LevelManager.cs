using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private List<string> repository = new List<string>()
    {
        "JULIA","ALIOUNE","SATUTA","ARQUEL","JORDAO"
    };
    private List<string> words = new List<string>();
    private float second;
    private float timeToNewWord;
    private int totalScore;
    private int minute, secondInt;
    private int totalLife = 10;
    [SerializeField] private GameObject paperObject;
    [SerializeField] private GameObject lettersObject;
    [SerializeField] private Text txtTime;
    [SerializeField] private Text txtScore;
    [SerializeField] private Text txtLife;
    private string time;

    private void Awake()
    {
        Instance = this;
        words.AddRange(repository);
    }

    public GameObject LettersObject
    {
        get { return lettersObject; }
    }

    private void Start()
    {
        //if (Typing.Instance.CurrentWord.Length != 0)
        InstantiatePaper(-5.24f, 6.19f);
        //InstantiatePaper(0.97f, 6.19f);
    }

    void Update()
    {
        timeToNewWord += Time.deltaTime;

        if (words.Count != 0 && timeToNewWord > 3)
        {
            InstantiatePaper(0.97f, 6.19f);
            timeToNewWord = 0;
        }
        SetTime();

    }

    private void InstantiatePaper(float x, float y)
    {
        var ob = Instantiate(paperObject, new Vector2(x, y), paperObject.transform.rotation);
        Typing.Instance = ob.GetComponent<Typing>();
        var width = Typing.Instance.CurrentWord.Length * 0.8f;

        var frontalCanvas = ob.transform.Find("FrontalCanvas").gameObject;
        frontalCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 1.39f);

        var backCanvas = ob.transform.Find("BackCanvas").gameObject;
        backCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 1.39f);

        var imageCanvas = ob.transform.Find("ImageCanvas").gameObject;
        imageCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(width + 0.59f, 1.01f);
    }

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

    public void SetScore(int score)
    {
        totalScore += score;
        txtScore.text = totalScore.ToString();
    }

    public void SetLife(int life)
    {
        totalLife -= life;
        txtLife.text = totalLife.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paper"))
        {
            SetLife(1);
            other.gameObject.GetComponent<Typing>().enabled = false;
            Destroy(other.gameObject, 5f);
        }
    }

    public string GetWord()
    {
        Debug.Log(words.Count);
        var newWord = string.Empty;
        if (words.Count != 0)
        {
            newWord = words.Last();
            //words.Remove(newWord);
        }
        return newWord;
    }

    public void RemWord()
    {
        words.Remove(words.Last());
    }
}
