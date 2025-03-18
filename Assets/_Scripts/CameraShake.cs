using DG.Tweening;
using UnityEngine;

public class CameraShake : SingletonMonoBehavior<CameraShake>  // 既存の SingletonMonoBehavior を利用
{
    public void ShakeCamera(float duration, float strength)
    {
        transform.DOShakePosition(duration, strength);
        transform.DOShakeRotation(duration, strength);
    }
}
