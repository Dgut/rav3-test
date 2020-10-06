using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Backpack : MonoBehaviour
{
    public Transform[] slots;

    public BackpackUI uiPrefab;
    private BackpackUI ui;

    public float putDuration = 0.5f;

    public Vector3 pullImpulse;
    public float pullRandomMagnitude;

    [Serializable]
    public class BackpackEvent : UnityEvent<BackpackItem>
    {
    }
    public BackpackEvent onItemPut;
    public BackpackEvent onItemPull;

    private void Start()
    {
        // creating ui for backpack
        ui = Instantiate(uiPrefab, FindObjectOfType<Canvas>().transform, false);
        ui.backpack = this;
    }

    public void Put(BackpackItem item)
    {
        // disabling user interaction
        item.enabled = false;

        // placing item
        var transform = item.transform;
        var rotation = transform.rotation;

        TweenFactory.Tween(item, transform.position, slots[item.type].position, putDuration, TweenScaleFunctions.QuadraticEaseInOut, (t) =>
        {
            transform.position = t.CurrentValue;
            transform.rotation = Quaternion.Lerp(rotation, slots[item.type].rotation, t.CurrentProgress);
        }, null);

        // adding item's ui
        ui.Add(item);

        onItemPut?.Invoke(item);
    }

    public void Pull(BackpackItem item)
    {
        // enabling user interaction
        item.enabled = true;

        item.Push(transform.rotation * (pullImpulse + UnityEngine.Random.insideUnitSphere * pullRandomMagnitude));

        onItemPull?.Invoke(item);
    }

    private void OnMouseDown()
    {
        // showing ui
        ui.gameObject.SetActive(true);
    }

    private void OnMouseUp()
    {
        // hiding ui
        ui.Remove();
        ui.gameObject.SetActive(false);
    }
}
