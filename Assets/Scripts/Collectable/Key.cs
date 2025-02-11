using UnityEngine;

public class Key : MonoBehaviour
{
    public Screw ConnectedScrew;
    public GameObject LockIcon;

    private void Start()
    {
        ConnectedScrew.Locked = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Wood>() != null)
        {
            ConnectedScrew.Locked = false;
            Destroy(LockIcon);
            Destroy(gameObject);
        }
    }
}
