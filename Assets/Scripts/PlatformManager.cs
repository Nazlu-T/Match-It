using System.Collections.Generic;
using UnityEngine;


public class PlatformManager : MonoBehaviour
{

    [SerializeField] private List<Platform> platforms;

    private Dictionary<int, int> _collectableIdMap = new Dictionary<int, int>();
    private Dictionary<int, List<Platform>> _platformsMap = new Dictionary<int, List<Platform>>();

    private void OnEnable()
    {

        Collectable.OnCollectableClicked += OnCollectableSelected;


    }

    private void OnDisable()
    {

        Collectable.OnCollectableClicked -= OnCollectableSelected;

    }


    public void OnCollectableSelected(Collectable collectable)
    {
        foreach (Platform platform in platforms)
        {
            if (platform.IsEmpty)
            {
                platform.SetCollectable(collectable);
                CheckPlatforms();
                break;
            }
        }
    }

    public void CheckPlatforms()
    {
        _collectableIdMap.Clear();
        _platformsMap.Clear();

        foreach (Platform platform in platforms)
        {
            if (!platform.IsEmpty)
            {
                int collectableId = platform.Collectable.CollectableDataSo.id;
                if (!_platformsMap.TryAdd(collectableId, new List<Platform> { platform }))
                {
                    _platformsMap[collectableId].Add(platform);
                }

                if (!_collectableIdMap.TryAdd(collectableId, 1))
                {
                    _collectableIdMap[collectableId] += 1;
                    if (_collectableIdMap[collectableId] == 3)
                    {
                        ReleasePlatforms(_platformsMap[collectableId]);

                    }
                }
            }

        }
    }

    private void ReleasePlatforms(List<Platform> platforms)
    {
        foreach (Platform platform in platforms)
        {
            platform.Release();

        }
    }

}
