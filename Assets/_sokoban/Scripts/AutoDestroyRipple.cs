using UnityEngine;
using DG.Tweening;

public class AutoDestroyRipple : MonoBehaviour
{
    void Start()
    {
        transform.DOScale(1.5f, 0.6f).SetEase(Ease.OutCubic);
        var sr = GetComponent<SpriteRenderer>();
        sr.DOFade(0, 0.6f).OnComplete(() => Destroy(gameObject));
    }
}
