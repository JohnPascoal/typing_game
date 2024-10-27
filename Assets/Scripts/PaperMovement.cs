using UnityEngine;

// 
// Summary:
//     Handle the object's movements in scene.
public class PaperMovement : MonoBehaviour
{
    public static PaperMovement Instance;
    [SerializeField] private GameObject pointRight, pointLeft;
    [SerializeField] private float moveForceV = 0.5f;
    private bool isMoveRight;
    private float time; // Time to change the current position and rotation

    private void Awake() => Instance = this;

    private void Start() => isMoveRight = Random.Range(0, 3) == 1;

    private void Update()
    {
        time += Time.deltaTime;

        if (!LevelManager.Instance.IsPausedGame)
        {
            MovementVertical();
            MovementHorizontal();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
            PaperTyping.Instance.IsEnableTyping = true;
    }

    // 
    // Summary:
    //     Handle the object's vertical movement in scene.
    private void MovementVertical() => transform.position += new Vector3(0, -1 * moveForceV * Time.deltaTime, 0f);

    // 
    // Summary:
    //     Handle the object's horizontal movement and rotation in scene.
    private void MovementHorizontal()
    {
        if (isMoveRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointRight.transform.position, 1.1f * Time.deltaTime);
            transform.Rotate(0f, 0f, Random.Range(0f, .5f));
            if (time >= 0.8f)
            {
                isMoveRight = false;
                time = 0;
            }
        }

        if (!isMoveRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointLeft.transform.position, 1.1f * Time.deltaTime);
            transform.Rotate(0f, 0f, Random.Range(0f, .5f) * -1);
            if (time >= 0.8f)
            {
                isMoveRight = true;
                time = 0;
            }
        }
    }
}
