using UnityEngine;
using DG.Tweening;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;

public class Platform : MonoBehaviour
{
    public bool IsEmpty {get; private set; } = true;
    public Transform PlaceHolder=> placeHolder;
    public Collectable Collectable => _collectable;
    [SerializeField] private Transform placeHolder;
    private Collectable _collectable;

    public void SetCollectable(Collectable collectable)
    {
        IsEmpty= false;
        _collectable = collectable;
        _collectable.SetPlatform(this);
        _collectable.OnReleased+= OnCollectableReleased;
    }

    public void Release(List<Platform>platforms)
    {
        if(IsEmpty)
        {
            return;
        }
        List<Collectable> collectables = new List<Collectable>();
        foreach(Platform platform in platforms)
        {
            collectables.Add(platform.Collectable);
        }
        _collectable.Release( collectables);
        IsEmpty=true;
     
    }
    private void OnCollectableReleased()
    {
        Debug.Log("OnCollectable Released");

    }

   
    
}
