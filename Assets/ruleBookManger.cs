using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ruleBookManger : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> pages = new List<GameObject>();
    public GameObject pageText;
    public int page = 1;
    public int pageSize = 15;

    public void Start()
    {
        foreach (var page in pages)
        {
            page.gameObject.SetActive(false);
        }
        pages[0].SetActive(true);
        pageText.GetComponent<TextMeshProUGUI>().text = page.ToString()+"/"+pageSize.ToString();
    }
    public void leftChangePage()
    {
        ButtonClick.instance.PlayButtonClick();
        pages[page - 1].gameObject.SetActive(false);
        if (page > 1)
        {
            page -= 1;
        }
        pages[page - 1].gameObject.SetActive(true);
        pageText.GetComponent<TextMeshProUGUI>().text = page.ToString() + "/" + pageSize.ToString();
    }
    public void rightChangePage()
    {
        ButtonClick.instance.PlayButtonClick();
        pages[page-1].gameObject.SetActive(false);
        if(page< pages.Count)
        {
            page += 1;
        }
        pages[page - 1].gameObject.SetActive(true);
        pageText.GetComponent<TextMeshProUGUI>().text = page.ToString() + "/" + pageSize.ToString();
    }

}
