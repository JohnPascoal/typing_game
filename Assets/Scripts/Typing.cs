using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    private string remainWord = string.Empty;
    private string currWord = "JEDP";

    void Start()
    {
        SetCurrWord();
    }

    void Update()
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
                InputLetter(letter);
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
        var script = gameObject.GetComponent<Paper>();
        Paper.Instance = script;
        GameObject rg = Instantiate(Paper.Instance.Letters[0], gameObject.transform.position, gameObject.transform.rotation);
        rg.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5f, ForceMode2D.Impulse);
        Paper.Instance.Letters.RemoveAt(0);
        Destroy(rg,3f);
    }
}
