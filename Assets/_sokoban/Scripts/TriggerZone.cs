using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public bool isBoxInZone = false;
    [SerializeField] private UnityEvent boxEnterEvent;
    [SerializeField] private UnityEvent boxExitEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box")) 
        {
            isBoxInZone = true;
            boxEnterEvent.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            isBoxInZone = false;
            boxExitEvent.Invoke();
        }
    }
}
