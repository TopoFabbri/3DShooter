using UnityEngine;

namespace Abstracts
{
    public interface ISpawnable
    {
        void SpawnWithSettings(SpawnableSettings settings, Vector3 pos, Quaternion rot);
    }
}
