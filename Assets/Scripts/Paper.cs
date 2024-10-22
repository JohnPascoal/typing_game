using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public static Paper Instance { get; set; }
    [SerializeField] private float moveForceV = 1f;
    [SerializeField] private GameObject point, point2;
    private bool isMoveRight;
    private float time;
    public TextMeshProUGUI txtBackText;
    private List<GameObject> letters;

    public List<GameObject> Letters
    {
        get { return letters; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var r = Random.Range(0, 3);
        isMoveRight = r == 1;

        txtBackText.text = Typing.Instance.CurrentWord;
        var textLower = Typing.Instance.CurrentWord;

        letters = new List<GameObject>();

        foreach (var letter in textLower)
        {
            letters.Add(LevelManager.Instance.LettersObject.transform.Find(letter.ToString()).gameObject);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        Movement();
        MovementVertical();
    }

    private void Movement()
    {
        transform.position += new Vector3(0, -1 * moveForceV * Time.deltaTime, 0f);
    }

    private void MovementVertical()
    {
        if (isMoveRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, point.transform.position, 1.1f * Time.deltaTime);
            transform.Rotate(0f, 0f, Random.Range(0f, 0.5f));
            if (time >= 0.8f)
            {
                isMoveRight = false;
                time = 0;
            }
        }

        if (!isMoveRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, point2.transform.position, 1.1f * Time.deltaTime);
            transform.Rotate(0f, 0f, Random.Range(0f, 0.5f) * -1);
            if (time >= 0.8f)
            {
                isMoveRight = true;
                time = 0;
            }
        }
    }

    

}
