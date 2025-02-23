using System;
using DG.Tweening;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public static event Action<Collectable> OnCollectableClicked;
    public float JumpForce => jumpForce;
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
    public void Release()
    {
        
    }

    public void OnReachPlatform()
    {
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
    }
}
