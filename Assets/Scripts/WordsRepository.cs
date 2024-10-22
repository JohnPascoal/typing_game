//using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WordsRepository : MonoBehaviour
{
    private List<string> repository = new List<string>(){
        "JULIA","ALIOUNE","SATUTA","ARQURL","JORDAO"
        };
    private List<string> words = new List<string>();

    private void Awake()
    {
        words.AddRange(repository);
    }

    public string GetWord()
    {
        Debug.Log("in");
        Debug.Log(words.Count);
        var currentWord = string.Empty;
        if (words.Count != 0)
        {
            currentWord = words[0];
            words.Remove(currentWord);
        }

        return currentWord;
    }
}
