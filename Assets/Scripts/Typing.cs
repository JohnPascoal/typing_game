using System;
using UnityEngine;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    public Text txtOutputWord;
    private string remainWord = string.Empty;
    private string currWord = "PASCOAL";

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
        txtOutputWord.text = remainWord;
    }

    private void InputLetter(string InputLetter)
    {
        if (IsCorrectLetter(InputLetter))
        {
            RemoveLetter();
            if (IsAllLetterCurrect())
                SetCurrWord();
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
}
