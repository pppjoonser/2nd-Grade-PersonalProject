using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityVFX : MonoBehaviour, IEntityComponent
{
    private Dictionary<string, IPlayableVFX> _playerableDictionary;
    private Entity _entity;
    public void Initialize(Entity entity)
    {
        _entity = entity;
        _playerableDictionary = new Dictionary<string, IPlayableVFX>();
        GetComponentsInChildren<IPlayableVFX>().ToList()
            .ForEach(playable => _playerableDictionary.Add(playable.VFXName, playable));
    }

    public void PlayVFX(string vfxName, Vector3 position, Quaternion rotation)
    {
        IPlayableVFX vfx = _playerableDictionary.GetValueOrDefault(vfxName);
        Debug.Assert(vfx != default(IPlayableVFX), $"{vfxName} is not exist");

        vfx.PlayVFX(position, rotation);
    }
    public void Stop(string vfxName)
    {
        IPlayableVFX vfx = _playerableDictionary.GetValueOrDefault(vfxName);
        Debug.Assert(vfx != default(IPlayableVFX), $"{vfxName} is not exist");

        vfx.StopVFX();
    }
}
