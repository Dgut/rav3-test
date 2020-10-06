using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackItemUI : MonoBehaviour
{
    [HideInInspector]
    public BackpackItem item;

    public Text title;
    public Text weight;

    public void Start()
    {
        title.text = item.title;
        weight.text = "Вес: " + item.weight;
    }
}
