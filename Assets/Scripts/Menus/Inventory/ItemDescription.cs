using System;
using TMPro;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    [SerializeField]
    public TMP_Text title;

    [SerializeField]
    public TMP_Text desc;

    private void Awake()
    {
        ResetDesc();
    }

    public void ResetDesc()
    {
        this.title.text = "";
        this.desc.text = "";

    }

    public void SetDesc(string title,string desc)
    {
        this.title.text = title;
        this.desc.text = desc;

    }
}
