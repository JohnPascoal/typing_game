using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WordsRepository : MonoBehaviour
{
    public static WordsRepository Instance { get; private set; }
    private readonly List<string> repository = new()
    {
        "ELZAC","ALIOUNE","SATUTA","ELIAS","ARQUEL","ELY","JULIA","JORDAO"
    };

    public List<string> Repository
    {
        get { return repository; }
    }

    private void Awake()
    {
        Instance = this;
        ChangeListOrder(Repository);
    }

    // 
    // Summary:
    //     Get next word in repository.
    public string GetWord()
    {
        var newWord = string.Empty;
        if (repository.Count != 0)
        {
            newWord = repository.Last();
        }
        return newWord;
    }

    // 
    // Summary:
    //     Remove the used word from repository.
    public void RemoveWord()
    {
        repository.Remove(repository.Last());
    }

    // 
    // Summary:
    //     Shuffle the order of list.
    //
    // Parameters:
    //   newListOrder:
    //     List to shuffle the order. 
    private void ChangeListOrder(List<string> newListOrder)
    {
        for (int i = 0; i < repository.Count; i++)
        {
            var random = Random.Range(i, repository.Count);
            (newListOrder[random], newListOrder[i]) = (newListOrder[i], newListOrder[random]); //tuples
        }
    }
}
