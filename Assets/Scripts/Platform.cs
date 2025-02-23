using UnityEngine;
using DG.Tweening;
using System;

public class Platform : MonoBehaviour
{
    public bool IsEmpty {get; private set; } = true;
    public Collectable Collectable => _collectable;
    [SerializeField] private Transform placeHolder;
    private Collectable _collectable;

    public void SetCollectable(Collectable collectable)
    {
        IsEmpty= false;
        _collectable = collectable;
        collectable.transform.DOJump(placeHolder.position, collectable.JumpForce, 1, 1)   
            .OnComplete(collectable.OnReachPlatform);
    }

   
    
}
