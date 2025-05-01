using UnityEngine;
using DG.Tweening;

public class Blocker : MonoBehaviour
{
    [SerializeField] private float absorbDuration = 1f;
    [SerializeField] private float rotationSpeed = 360f; // градусов за всё время

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void Clear()
    {
        transform.DORotate(new Vector3(0, 0, rotationSpeed), absorbDuration, RotateMode.FastBeyond360)
                 .SetEase(Ease.InCubic);

        transform.DOScale(Vector3.zero, absorbDuration).SetEase(Ease.InCubic);
    }

    public void Restore()
    {
        transform.DORotate(Vector3.zero, absorbDuration);
        transform.DOScale(originalScale, absorbDuration);
    }
}
