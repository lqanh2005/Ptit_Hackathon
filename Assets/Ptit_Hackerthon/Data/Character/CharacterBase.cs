using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public Joystick joystick;
    public float moveSpeed = 5f;
    private Rigidbody rb;

    public void Init()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity= new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(joystick.Horizontal, 0, joystick.Vertical));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}