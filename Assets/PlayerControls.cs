using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float Speed = 2;

    private Rigidbody2D componentRigidbody;

    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        componentRigidbody.linearVelocity = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) componentRigidbody.linearVelocity += Vector2.left * Speed;
        if (Input.GetKey(KeyCode.RightArrow)) componentRigidbody.linearVelocity += Vector2.right * Speed;
        if (Input.GetKey(KeyCode.UpArrow)) componentRigidbody.linearVelocity += Vector2.up * Speed;
        if (Input.GetKey(KeyCode.DownArrow)) componentRigidbody.linearVelocity += Vector2.down * Speed;
    }
}