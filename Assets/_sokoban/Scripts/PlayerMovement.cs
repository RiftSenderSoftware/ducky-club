using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public float moveDuration = 0.3f;
    public Vector2Int gridPosition;
    public SpriteRenderer playerSpriteRender;
    public LayerMask obstacleLayer;
    public LayerMask boxLayer;

    public GameObject ripplePrefab; // Префаб ряби
    public Transform rippleSpawnPoint; // Точка за уточкой


    void Start()
    {
        gridPosition = Vector2Int.RoundToInt(transform.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) Move(Vector2Int.up);
        if (Input.GetKeyDown(KeyCode.S)) Move(Vector2Int.down);
        if (Input.GetKeyDown(KeyCode.A)) 
        { 
            Move(Vector2Int.left);
            playerSpriteRender.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
            playerSpriteRender.flipX = false;
        }
    }

    void Move(Vector2Int direction)
    {
        Vector2Int newPosition = gridPosition + direction;

        Collider2D boxCollider = Physics2D.OverlapPoint(newPosition, boxLayer);
        if (boxCollider != null)
        {
            BoxMovement box = boxCollider.GetComponent<BoxMovement>();
            if (box != null)
            {
                if (!box.Move(direction))
                {
                    return;
                }

                gridPosition = newPosition;
                Vector3 targetPosition = new Vector3(gridPosition.x, gridPosition.y, 0);
                VisualMove(targetPosition);
            }
        }
        else if (!IsBlocked(newPosition))
        {
            gridPosition = newPosition;
            Vector3 targetPosition = new Vector3(gridPosition.x, gridPosition.y, 0);
            VisualMove(targetPosition);
        }
    }
    public void VisualMove(Vector3 targetPosition)
    {
        transform.DOMove(targetPosition, moveDuration).SetEase(Ease.Linear);

        float angleZ = 0f;
        float deltaX = targetPosition.x - transform.position.x;

        if (Mathf.Abs(deltaX) > 0.01f)
        {
            angleZ = -deltaX * 10f;

            transform.DORotate(new Vector3(0, 0, angleZ), moveDuration / 2f)
                     .SetEase(Ease.OutQuad)
                     .SetLoops(2, LoopType.Yoyo)
                     .OnKill(() => transform.rotation = Quaternion.identity);
        }

        // Спавним рябь
        if (ripplePrefab != null && rippleSpawnPoint != null)
        {
            Instantiate(ripplePrefab, rippleSpawnPoint.position, Quaternion.identity);
        }
    }



    bool IsBlocked(Vector2Int targetPosition)
    {
        Collider2D hit = Physics2D.OverlapPoint(targetPosition, obstacleLayer);
        return hit != null;
    }
}