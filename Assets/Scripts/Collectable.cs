using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public static event Action<Collectable> OnCollectableClicked;
    public event Action OnReleased;
    public float JumpForce => jumpForce;
    public bool IsAnimating { get; private set; }

    public CollectableDataSo CollectableDataSo => data;
    [SerializeField] private float jumpForce = 3;

    [SerializeField] private CollectableDataSo data;
    [SerializeField] private OnCollectableClickedEvent onCollectableClickedEvent;


    private Collider _collider;
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (OnCollectableClicked != null)
        {
            OnCollectableClicked.Invoke(this);
        }

    }
    public void Release(List<Collectable> collectables)
    {
        StartCoroutine(WaitForAnimation(collectables));

    }

    public void OnReachPlatform()
    {
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        IsAnimating = false;
    }

    public void SetPlatform(Platform platform)
    {
        IsAnimating = true;
        transform.DOJump(platform.PlaceHolder.position, JumpForce, 1, 1)
             .OnComplete(OnReachPlatform);
    }

    private IEnumerator WaitForAnimation(List<Collectable> collectables)
    {
        yield return new WaitWhile(() => WaitForCollectableAnims(collectables));
        OnReleased?.Invoke();
        Destroy(gameObject);
    }


    private bool WaitForCollectableAnims(List<Collectable> collectables)
    {
        foreach (Collectable collectable in collectables)
        {
            if (collectable.IsAnimating)
                return true;
        }

        return false;
    }

}
