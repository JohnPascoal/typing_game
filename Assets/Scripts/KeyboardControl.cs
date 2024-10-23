using UnityEngine;

public class KeyboardControl : MonoBehaviour
{
    [SerializeField] private GameObject[] keys;

    private void Update() => KeyPressed();

    //
    // Summary:
    //     Initialize animation after hit key.
    private void KeyPressed()
    {
        keys[0].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.Q));
        keys[1].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.W));
        keys[2].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.E));
        keys[3].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.R));
        keys[4].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.T));
        keys[5].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.Y));
        keys[6].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.U));
        keys[7].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.I));
        keys[8].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.O));
        keys[9].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.P));
        keys[10].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.A));
        keys[11].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.S));
        keys[12].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.D));
        keys[13].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.F));
        keys[14].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.G));
        keys[15].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.H));
        keys[16].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.J));
        keys[17].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.K));
        keys[18].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.L));
        keys[19].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.Z));
        keys[20].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.X));
        keys[21].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.C));
        keys[22].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.V));
        keys[23].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.B));
        keys[24].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.N));
        keys[25].GetComponent<Animator>().SetBool("IsPressed", Input.GetKey(KeyCode.M));
    }
}
