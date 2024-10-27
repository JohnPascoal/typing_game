using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// 
// Summary:
//     Words repository.
public class WordsRepository : MonoBehaviour
{
    public static WordsRepository Instance { get; private set; }
    private readonly List<string> repository = new()
    {
        "ELZAC","ALIOUNE"//,"SATUTA","JULIA","ARQUEL","DARIO","JOEL","JORDAO","LANDO","IMACULADA","EDUVANIA"
    };

    private void Awake()
    {
        Instance = this;
        ChangeListOrder(repository);
    }

    // 
    // Summary:
    //     Return the repository's size.
    public int Size() => repository.Count;

    // 
    // Summary:
    //     Return a word from the repository.
    public string GetItem() => repository.Count > 0 ? repository.First() : string.Empty;

    // 
    // Summary:
    //     Remove the used word from repository.
    public void RemoveItem() => repository.Remove(repository.First());

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
            (newListOrder[random], newListOrder[i]) = (newListOrder[i], newListOrder[random]);
        }
    }
}
