using UnityEngine;
using DG.Tweening;

public class BoxMovement : MonoBehaviour
{
    public float moveDuration = 0.3f;
    public Vector2Int gridPosition;
    public LayerMask obstacleLayer; // Слой стен и других неподвижных препятствий
    public LayerMask boxLayer;      // Слой ящиков (добавьте это в Unity Inspector)

    void Start()
    {
        gridPosition = Vector2Int.RoundToInt(transform.position);
    }

    public bool Move(Vector2Int direction)
    {
        Vector2Int newPosition = gridPosition + direction;

        if (!IsBlocked(newPosition))
        {
            gridPosition = newPosition;
            Vector3 targetPosition = new Vector3(gridPosition.x, gridPosition.y, 0);
            transform.DOMove(targetPosition, moveDuration); // Плавность для синхронизации
            return true; // Успешно сдвинули ящик
        }
        return false; // Ящик не может двигаться
    }

    public bool IsBlocked(Vector2Int targetPosition)
    {
        // Проверка на стены
        Collider2D wallHit = Physics2D.OverlapPoint(targetPosition, obstacleLayer);
        // Проверка на другие ящики
        Collider2D boxHit = Physics2D.OverlapPoint(targetPosition, boxLayer);

        // Если есть стена или другой ящик (кроме себя самого), путь заблокирован
        return wallHit != null || (boxHit != null && boxHit.gameObject != gameObject);
    }
}