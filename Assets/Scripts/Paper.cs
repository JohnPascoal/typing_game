using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public static Paper Instance { get; set; }
    [SerializeField] private GameObject point, point2;
    public TextMeshProUGUI txtBackText;
    private readonly List<GameObject> letters = new();
    [SerializeField] private float moveForceV = 1f;
    private bool isMoveRight;
    private float time;

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
        isMoveRight = Random.Range(0, 3) == 1;

        txtBackText.text = TypingControl.Instance.Word;        

        foreach (var letter in txtBackText.text)
        {
            letters.Add(LevelManager.Instance.LettersObject.transform.Find(letter.ToString()).gameObject);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        MovementVertical();
        MovementHorizontal();
    }

    private void MovementVertical() => transform.position += new Vector3(0, -1 * moveForceV * Time.deltaTime, 0f);

    private void MovementHorizontal()
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            gameObject.GetComponent<TypingControl>().enabled = true;
        }
    }

}
