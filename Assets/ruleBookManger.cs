using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruleBookManger : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> pages = new List<GameObject>();
    public int page = 1;

    public void Start()
    {
        foreach (var page in pages)
        {
            page.gameObject.SetActive(false);
        }
        pages[0].SetActive(true);
    }
    public void leftChangePage()
    {
        pages[page - 1].gameObject.SetActive(false);
        if (page > 1)
        {
            page -= 1;
        }
        pages[page - 1].gameObject.SetActive(true);
    }
    public void rightChangePage()
    {
        pages[page-1].gameObject.SetActive(false);
        if(page< pages.Count)
        {
            page += 1;
        }
        pages[page - 1].gameObject.SetActive(true);
    }

}
