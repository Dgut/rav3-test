using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackpackUI : MonoBehaviour
{
    public RectTransform[] titles;

    public Backpack backpack;

    public void Add(BackpackItem item)
    {
        var ui = Instantiate(item.uiPrefab, transform, false);
        ui.item = item;
        // place below title
        ui.transform.SetSiblingIndex(titles[item.type].GetSiblingIndex() + 1);
    }

    public void Remove()
    {
        // trying to remove element under cursor
        var inputModule = (CustomStandaloneInputModule)EventSystem.current.currentInputModule;
        var ui = inputModule.GetPointerData().pointerEnter?.GetComponent<BackpackItemUI>();
        if (ui)
        {
            backpack.Pull(ui.item);

            ui.item = null;
            Destroy(ui.gameObject);
        }
    }
}
