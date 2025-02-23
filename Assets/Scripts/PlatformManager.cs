using System.Collections.Generic;
using UnityEngine;


public class PlatformManager : MonoBehaviour
{

    [SerializeField] private List<Platform> platforms;

    private Dictionary<int, int> _collectableIdMap = new Dictionary<int, int>();
    [SerializeField] private OnCollectableClickedListener onCollectableClickedListener;



    private void OnEnable()
    {
        //onCollectableClickedListener.Response += OnCollectableSelected;
        Collectable.OnCollectableClicked += OnCollectableSelected;


    }

    private void OnDisable()
    {
        //onCollectableClickedListener.Response -= OnCollectableSelected;
        Collectable.OnCollectableClicked -= OnCollectableSelected;

    }


    public void OnCollectableSelected(Collectable collectable)
    {
        foreach (Platform platform in platforms)
        {
            if (platform.IsEmpty)
            {
                platform.SetCollectable(collectable);
                CheckedPlatforms();
                break;
            }
        }
    }

    public void CheckedPlatforms()
    {
        _collectableIdMap.Clear();

        foreach (Platform platform in platforms)
        {
            if (platform.IsEmpty)
            {
                if (_collectableIdMap.Count < 3)
                {
                    break;
                }
            }
            else
            {
                int collectableId = platform.Collectable.CollectableDataSo.id;
                if (!_collectableIdMap.TryAdd(collectableId, 1))
                {
                    _collectableIdMap[collectableId] += 1;
                    if (_collectableIdMap[collectableId] == 3)
                    {
                        Debug.Log(collectableId);
                    }
                }
            }
        }
    }

}
