using System;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float choke;
    public GameObject[] levels;
    public Vector2 ScreenBounds;
    public float scrollSpeed;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        ScreenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in levels)
        {
            LoadChildObjects(obj);
        }
    }

    private void LateUpdate()
    {
        foreach (GameObject obj in levels)
        {
            RepositionChildObjects(obj);
        }
    }

    private void LoadChildObjects(GameObject obj)
    {
        float objectHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y - choke;
        int childrenNeeded = (int)Mathf.Ceil(ScreenBounds.y * 2 / objectHeight);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childrenNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(obj.transform.position.x, objectHeight * i, obj.transform.position.z);
            c.name = obj.name + i;
        }

        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    private void RepositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectHeight = lastChild.GetComponent<SpriteRenderer>().bounds.extents.y - choke;
            if (transform.position.y + ScreenBounds.y > lastChild.transform.position.y + halfObjectHeight)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x, lastChild.transform.position.y + halfObjectHeight * 2, lastChild.transform.position.z);
            }
            else if (transform.position.y - ScreenBounds.y < firstChild.transform.position.y - halfObjectHeight)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x, firstChild.transform.position.y + halfObjectHeight * 2, firstChild.transform.position.z);
            }
        }
    }

    private void Update()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 desiredPosition = transform.position + new Vector3(0, scrollSpeed, 0);
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.3f);
        transform.position = smoothPosition;
    }
}
