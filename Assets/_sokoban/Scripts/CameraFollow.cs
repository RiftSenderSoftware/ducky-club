using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // Цель (игрок)
    [SerializeField] private float deadZoneWidth = 2f; // Ширина мёртвой зоны
    [SerializeField] private float deadZoneHeight = 1f; // Высота мёртвой зоны
    [SerializeField] private float smoothSpeed = 0.125f; // Скорость сглаживания
    [SerializeField] private bool useMapBounds = false; // Использовать границы карты
    [SerializeField] private Vector2 minBounds; // Минимальные X, Y границы карты
    [SerializeField] private Vector2 maxBounds; // Максимальные X, Y границы карты
    [SerializeField] private Vector2 offsetFromCenter; // Смещение камеры относительно центра (X, Y)

    private Vector3 offset; // Смещение камеры относительно игрока (включая Z)

    private void Awake()
    {
        if (target == null)
        {
            Debug.LogError("CameraFollow: Target not assigned!");
            enabled = false;
            return;
        }

        // Сохраняем начальное смещение (включая Z-позицию камеры)
        offset = transform.position - target.position;
        // Учитываем пользовательское смещение по X и Y
        offset.x = offsetFromCenter.x;
        offset.y = offsetFromCenter.y;
    }

    private void FixedUpdate()
    {
        // Текущая позиция центра камеры (без учёта Z)
        Vector2 cameraCenter = new Vector2(transform.position.x, transform.position.y);
        // Позиция игрока (без учёта Z)
        Vector2 targetPos = new Vector2(target.position.x, target.position.y);

        // Разница между позицией игрока и центром камеры
        Vector2 delta = targetPos - cameraCenter;

        // Проверяем, вышел ли игрок за границы мёртвой зоны
        Vector2 newCameraPos = cameraCenter;
        bool shouldMove = false;

        // Проверка по X
        if (Mathf.Abs(delta.x) > deadZoneWidth / 2f)
        {
            newCameraPos.x = targetPos.x - Mathf.Sign(delta.x) * (deadZoneWidth / 2f) + offsetFromCenter.x;
            shouldMove = true;
        }

        // Проверка по Y
        if (Mathf.Abs(delta.y) > deadZoneHeight / 2f)
        {
            newCameraPos.y = targetPos.y - Mathf.Sign(delta.y) * (deadZoneHeight / 2f) + offsetFromCenter.y;
            shouldMove = true;
        }

        if (shouldMove)
        {
            // Плавное перемещение камеры
            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position,
                new Vector3(newCameraPos.x, newCameraPos.y, transform.position.z),
                smoothSpeed
            );

            // Ограничение по границам карты, если включено
            if (useMapBounds)
            {
                float cameraHalfHeight = Camera.main.orthographicSize;
                float cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;

                smoothedPosition.x = Mathf.Clamp(
                    smoothedPosition.x,
                    minBounds.x + cameraHalfWidth,
                    maxBounds.x - cameraHalfWidth
                );
                smoothedPosition.y = Mathf.Clamp(
                    smoothedPosition.y,
                    minBounds.y + cameraHalfHeight,
                    maxBounds.y - cameraHalfHeight
                );
            }

            transform.position = smoothedPosition;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация прямоугольной мёртвой зоны в редакторе
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(deadZoneWidth, deadZoneHeight, 0f));

        // Визуализация границ карты, если включено
        if (useMapBounds)
        {
            Gizmos.color = Color.red;
            Vector3 boundsSize = new Vector3(
                maxBounds.x - minBounds.x,
                maxBounds.y - minBounds.y,
                0f
            );
            Vector3 boundsCenter = new Vector3(
                (minBounds.x + maxBounds.x) / 2f,
                (minBounds.y + maxBounds.y) / 2f,
                transform.position.z
            );
            Gizmos.DrawWireCube(boundsCenter, boundsSize);
        }
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private System.Collections.IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }
}