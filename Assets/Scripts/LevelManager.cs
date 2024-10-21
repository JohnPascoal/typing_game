using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private float time;
    [SerializeField] private GameObject paperObject;
    [SerializeField] private GameObject lettersObject;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject LettersObject
    {
        get { return lettersObject; }
    }

    private void Start()
    {
        Insta();
    }
    
    void Update()
    {
        time = Time.deltaTime;
    }

    private void Insta()
    {
        Instantiate(paperObject, new Vector2(-5.24f, 7.98f), paperObject.transform.rotation);
        Instantiate(paperObject, new Vector2(0.48f, 7.98f), paperObject.transform.rotation);
    }
}
