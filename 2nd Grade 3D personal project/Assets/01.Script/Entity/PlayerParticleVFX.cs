using UnityEngine;

public class PlayerParticleVFX : MonoBehaviour
{
    [field: SerializeField] public string VFXName { get; private set; }
    [SerializeField] private bool isOnPosition;
    [SerializeField] private ParticleSystem particle;

    public void PlayVFX(Vector3 position, Quaternion rotation)
    {
        if (isOnPosition == false)
            transform.SetPositionAndRotation(position, rotation);

        particle.Play(true);
    }

    public void StopVFX()
    {
        particle.Stop();
    }
    public void OnValidate()
    {
        if (string.IsNullOrEmpty(VFXName) == false)
            gameObject.name = VFXName;
    }
}
