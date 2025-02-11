using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : MonoBehaviour
{
    [Header ("Core")]
    public List<HingeJoint2D> Connections;
    public BoardHole ConnectedHole;
    public bool Locked;
    public Rigidbody2D rb2D;

    [Header("Open-Close Animation")]
    public SpriteRenderer CloseScrewRenderer;
    public SpriteRenderer OpenScrewRenderer;
    public Transform OpenScrewTransform;
    public float ScrewAnimationSpeed;

    [Header("Hole Generation")]
    public WoodHole WoodHolePrefab;
    public BoardHole BoardHolePrefab;
    public LayerMask WoodLayer;

    [Header("Shine Settings")]
    public GameObject ShineObject;

    private Collider2D col;
    private Vector3 initialOpenScrewPos;
    private float moveDistance = 0.3f;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        initialOpenScrewPos = OpenScrewTransform.localPosition;

        GenerateHoles();
    }
    public void GenerateHoles()
    {
        Vector3 castPos = new Vector3(transform.position.x, transform.position.y, -1);
        RaycastHit2D[] hits = Physics2D.RaycastAll(castPos, Vector3.zero, 100f, WoodLayer);
        Vector3 boardHolePos = new Vector3(transform.position.x, transform.position.y, 1);
        BoardHole boardHole = Instantiate(BoardHolePrefab, boardHolePos, Quaternion.identity);
        ConnectedHole = boardHole;
        boardHole.IsEmpty = false;

        foreach (RaycastHit2D hit in hits)
        {
            Wood wood = hit.collider.GetComponent<Wood>();
            WoodHole woodHole=  Instantiate(WoodHolePrefab, transform.position, Quaternion.identity);
            Vector3 localScale = woodHole.transform.localScale;
            Vector3 newScale = new Vector3(localScale.x / wood.transform.localScale.x, localScale.y / wood.transform.localScale.y, 1);
            woodHole.transform.SetParent(wood.transform, true);
            woodHole.transform.localRotation = Quaternion.Euler(Vector3.zero);
            woodHole.transform.localScale = newScale;
            HingeJoint2D joint = wood.gameObject.AddComponent<HingeJoint2D>();
            joint.autoConfigureConnectedAnchor = false;
            joint.anchor = woodHole.transform.localPosition;
            joint.connectedAnchor = Vector2.zero;
            joint.connectedBody = rb2D;
            woodHole.Joint = joint;
        }
    }
    public void UnBind()
    {
        col.isTrigger = false;

        foreach (HingeJoint2D joint in Connections)
        {
            joint.enabled = false;
        }

        Connections.Clear();
    }
    public void Shine(bool value)
    {
        ShineObject.SetActive(value);
    }
    public void Open()
    {
        StartCoroutine(CoOpen());
    }
    public void Close()
    {
        StartCoroutine(CoClose());
    }
    public IEnumerator CoOpen()
    {
        CloseScrewRenderer.enabled = false;
        OpenScrewRenderer.enabled = true;
        yield return StartCoroutine(CoOpenAnim());
    }
    public IEnumerator CoClose()
    {
        yield return StartCoroutine(CoCloseAnim());
        CloseScrewRenderer.enabled = true;
        OpenScrewRenderer.enabled = false;
    }
    private IEnumerator CoOpenAnim()
    {
        OpenScrewTransform.localPosition = initialOpenScrewPos;
        Vector3 targetPos = initialOpenScrewPos + new Vector3(0, moveDistance, 0);
        iTween.MoveTo(OpenScrewTransform.gameObject, iTween.Hash("position", targetPos, "time", ScrewAnimationSpeed, "islocal", true, "easetype", iTween.EaseType.linear));
        yield return new WaitForSeconds(ScrewAnimationSpeed);
        //float t = 0;
        //OpenScrewTransform.position = initialOpenScrewPos;
        //Vector3 targetPos = initialOpenScrewPos + new Vector3(0, moveDistance, 0);

        //while (t < 1)
        //{
        //    OpenScrewTransform.localPosition = Vector3.Lerp(initialOpenScrewPos, targetPos, t);
        //    t += Time.deltaTime * ScrewSpeed;
        //    yield return null;
        //}

        //OpenScrewTransform.localPosition = targetPos;
    }
    private IEnumerator CoCloseAnim()
    {
        OpenScrewTransform.localPosition = initialOpenScrewPos + new Vector3(0, moveDistance, 0);
        Vector3 initialPos = OpenScrewTransform.localPosition;
        Vector3 targetPos = initialPos + new Vector3(0, -moveDistance, 0);
        iTween.MoveTo(OpenScrewTransform.gameObject, iTween.Hash("position", targetPos, "time", ScrewAnimationSpeed, "islocal", true, "easetype", iTween.EaseType.linear));
        yield return new WaitForSeconds(ScrewAnimationSpeed);
        //float t = 0;
        //OpenScrewTransform.localPosition = initialOpenScrewPos + new Vector3(0, moveDistance, 0);
        //Vector3 initialPos = OpenScrewTransform.localPosition;
        //Vector3 targetPos = initialPos + new Vector3(0, -moveDistance, 0);

        //while (t < 1)
        //{
        //    OpenScrewTransform.localPosition = Vector3.Lerp(initialPos, targetPos, t);
        //    t += Time.deltaTime * ScrewSpeed;
        //    yield return null;
        //}

        //OpenScrewTransform.localPosition = targetPos;
    }

}
