using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wood : MonoBehaviour
{
    private List<HingeJoint2D> joints;
    private void Start()
    {
        joints = GetComponents<HingeJoint2D>().ToList();
        SetupScrewConnection();
        SetupSprites();
    }

    public void SetupScrewConnection()
    {
        foreach(HingeJoint2D joint in joints)
        {
            Screw screw = joint.connectedBody.gameObject.GetComponent<Screw>();
            screw.Connections.Add(joint);
        }
    }
    private void SetupSprites()
    {
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        WoodHole[] holes = GetComponentsInChildren<WoodHole>();

        foreach(WoodHole hole in holes)
        {
            SpriteRenderer spriteRenderer = hole.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = r.sortingOrder + 1;
        }
    }
}
