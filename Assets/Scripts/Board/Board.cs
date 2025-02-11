using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject ShineObject;
    public void Shine(bool value)
    {
        ShineObject.SetActive(value);
    }
}
