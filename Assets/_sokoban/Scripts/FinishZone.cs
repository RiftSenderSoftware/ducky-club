using UnityEngine;
using UnityEngine.Events;

public class FinishZone : MonoBehaviour
{
    private WinLoader winLoader;

    private void Awake()
    {
        winLoader = FindAnyObjectByType<WinLoader>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            winLoader.PlayerWin();
        }
    }

}
