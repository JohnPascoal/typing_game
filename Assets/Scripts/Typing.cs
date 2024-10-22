using System;
using TMPro;
using UnityEngine;

public class Typing : MonoBehaviour
{
    public static Typing Instance { get; set; }
    public TextMeshProUGUI tmpText;
    private string remainWord = string.Empty;
    private string currWord = "AS";
    private bool isDirection;

    public string CurrentWord
    {
        get { return currWord; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetCurrWord();
    }

    private void Update()
    {
        CheckLetterInput();
    }

    private void SetCurrWord()
    {
        SetRemainWord(currWord);
    }

    private void SetRemainWord(string currWord)
    {
        remainWord = currWord;
        tmpText.text = remainWord;
    }

    private void InputLetter(string InputLetter)
    {
        if (IsCorrectLetter(InputLetter))
        {
            LevelManager.Instance.SetScore(10);
            InstantiateLetter();
            RemoveLetter();
            if (IsAllLetterCurrect())
                Destroy(gameObject);
        }
    }

    private void CheckLetterInput()
    {
        if (Input.anyKeyDown)
        {
            var letter = Input.inputString;
            if (letter.Length == 1)
                InputLetter(letter.ToUpper());
        }
    }

    private bool IsCorrectLetter(string value)
    {
        return remainWord.IndexOf(value) == 0;
    }

    private void RemoveLetter()
    {
        var newWord = remainWord.Remove(0, 1);
        SetRemainWord(newWord);
    }

    private bool IsAllLetterCurrect()
    {
        return remainWord.Length == 0;
    }

    private void InstantiateLetter()
    {
        Paper.Instance = gameObject.GetComponent<Paper>();
        GameObject rg = Instantiate(Paper.Instance.Letters[0], gameObject.transform.position, gameObject.transform.rotation);
        rg.GetComponent<Rigidbody2D>().AddForce((isDirection ? Vector2.left : Vector2.right) * 5f, ForceMode2D.Impulse);
        Paper.Instance.Letters.RemoveAt(0);
        isDirection = !isDirection;
        Destroy(rg, 5f);
    }
}
