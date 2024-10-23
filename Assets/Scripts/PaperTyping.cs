using TMPro;
using UnityEngine;

// 
// Summary:
//     Handle the behavior of object while the user hits any keys.
public class PaperTyping : MonoBehaviour
{
    public static PaperTyping Instance { get; set; }
    public TextMeshProUGUI tmpText;
    [SerializeField] private AudioClip audioKeyPressed;
    private string remainWord = string.Empty;
    private string word = string.Empty;
    private bool isDirectionRight;

    public string Word
    {
        get
        {
            word = WordsRepository.Instance.GetItem();
            return word;
        }
    }

    private void Awake() => Instance = this;

    private void Start()
    {
        SeteWord();
    }

    private void Update() => CheckingInput();

    // 
    // Summary:
    //     Initialize the next word from the repository.
    public void SeteWord()
    {
        WordsRepository.Instance.RemoveItem();
        SetRemainWord(word);
    }

    // 
    // Summary:
    //     Update the current word.
    //
    // Parameters:
    //   currentWord:
    private void SetRemainWord(string currentWord)
    {
        remainWord = currentWord;
        tmpText.text = remainWord;
    }

    // 
    // Summary:
    //     Controling keyboard input.
    //
    // Parameters:
    //   letter:
    //     Get the keyboard input. 
    private void InputLetter(string letter)
    {
        if (IsCorrectLetter(letter))
        {
            GetComponent<AudioSource>().PlayOneShot(audioKeyPressed);
            LevelManager.Instance.SetScore(10);
            InstantiateLetter();
            RemoveLetter();
            if (IsFinished())
                Destroy(gameObject, 0.3f);
        }
    }

    // 
    // Summary:
    //     Listening if the user hits any key.
    private void CheckingInput()
    {
        if (Input.anyKeyDown)
        {
            var letter = Input.inputString;
            if (letter.Length == 1)
                InputLetter(letter.ToUpper());
        }
    }

    // 
    // Summary:
    //     Returns true if keyboard input is the same of the current word's index 0.
    private bool IsCorrectLetter(string letter) => remainWord.IndexOf(letter) == 0;

    // 
    // Summary:
    //     Remove the index 0 of the current word.
    private void RemoveLetter()
    {
        var newWord = remainWord.Remove(0, 1);
        SetRemainWord(newWord);
    }

    // 
    // Summary:
    //     Returns true if all inputs is the same of the current word's letters.
    private bool IsFinished() => remainWord.Length == 0;

    // 
    // Summary:
    //     Instantiate a object and apply a force.
    private void InstantiateLetter()
    {
        PaperMovement.Instance = gameObject.GetComponent<PaperMovement>();
        GameObject rg = Instantiate(PaperMovement.Instance.Letters[0], gameObject.transform.position, gameObject.transform.rotation);
        rg.GetComponent<Rigidbody2D>().AddForce((isDirectionRight ? Vector2.left : Vector2.right) * 5f, ForceMode2D.Impulse);
        PaperMovement.Instance.Letters.RemoveAt(0);
        isDirectionRight = !isDirectionRight;
        Destroy(rg, 5f);
    }
}
