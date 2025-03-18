using DG.Tweening;
using UnityEngine;

public class CameraShake : SingletonMonoBehavior<CameraShake>  // Used SingletonMonoBehavior
{
    public void ShakeCamera(float duration, float strength)
    {
        transform.DOShakePosition(duration, strength);
        transform.DOShakeRotation(duration, strength);
    }
}
