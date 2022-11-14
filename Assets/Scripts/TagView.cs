using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagView : MonoSingleton<TagView>
{
    public List<Canvas> tags = new List<Canvas>();

    private Canvas canvas;

    private void Awake()
    {
        tags = new List<Canvas>(transform.GetComponentsInChildren<Canvas>());
        for (int i = 0; i < tags.Count; ++i)
        {
            tags[i].transform.Find("Image/Text").GetComponent<Text>().text = i.ToString();
        }
    }

    private void Update()
    {
        foreach (Canvas t in tags)
        {
            t.transform.LookAt(Monitor.Instance.CurrCam.transform);
        }
    }

    public void SetMainCam(Camera camera)
    {
        foreach (Canvas t in tags)
        {
            t.worldCamera = camera;
        }
    }

    public void SetInfo(string[] info)
    {
        int index = int.Parse(info[0]);
        string data = info[1];
        if (tags != null)
            tags[index].transform.Find("Image/Text").GetComponent<Text>().text = data;
    }
}
