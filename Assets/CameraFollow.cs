using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Основные настройки")]
    public Transform target;          // Цель (игрок)
    public Vector2 offset = new Vector2(0f, 1f); // Смещение камеры (X, Y)
    public float smoothTime = 0.3f;   // Время плавного перемещения

    [Header("Настройки инерции")]
    public float lookAheadFactor = 0.5f; // Коэффициент опережения
    public float lookAheadSmooth = 5f;   // Плавность опережения
    public float maxLookAhead = 2f;      // Максимальное опережение

    [Header("Обзор лабиринта")]
    public float overviewSize = 14f;    // Размер камеры в режиме обзора
    public float overviewSmoothTime = 0.5f; // Время плавного перехода в режим обзора

    private Vector3 velocity = Vector3.zero;
    private Vector3 currentOffset;
    private Vector2 lookAheadPos;
    private Vector2 lookAheadVelocity;
    private float lastXPos;
    private Camera cam;
    private float defaultSize;
    private bool isOverview = false;
    private Vector3 overviewPosition;
    private float overviewZoomVelocity;

    void Start()
    {
        cam = GetComponent<Camera>();
        defaultSize = cam.orthographicSize;

        if (target != null)
        {
            lastXPos = target.position.x;
            currentOffset = new Vector3(offset.x, offset.y, -10f);
        }
    }

    void LateUpdate()
    {
        if (isOverview)
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                overviewPosition,
                ref velocity,
                overviewSmoothTime
            );

            cam.orthographicSize = Mathf.SmoothDamp(
                cam.orthographicSize,
                overviewSize,
                ref overviewZoomVelocity,
                overviewSmoothTime
            );
        }
        else
        {
            if (target == null) return;

            cam.orthographicSize = Mathf.SmoothDamp(
                cam.orthographicSize,
                defaultSize,
                ref overviewZoomVelocity,
                overviewSmoothTime
            );

            float xMoveDelta = target.position.x - lastXPos;
            lastXPos = target.position.x;

            bool isMoving = Mathf.Abs(xMoveDelta) > 0.1f;
            float targetLookAhead = isMoving ? Mathf.Sign(xMoveDelta) * lookAheadFactor : 0f;
            lookAheadPos = Vector2.SmoothDamp(
                lookAheadPos,
                new Vector2(targetLookAhead, 0),
                ref lookAheadVelocity,
                lookAheadSmooth * Time.deltaTime
            );
            lookAheadPos.x = Mathf.Clamp(lookAheadPos.x, -maxLookAhead, maxLookAhead);

            Vector3 targetPosition = target.position + currentOffset +
                                   new Vector3(lookAheadPos.x, lookAheadPos.y, 0);

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref velocity,
                smoothTime
            );
        }
    }

    public void ToggleOverview(Vector3 mazeCenter, float mazeWidth, float mazeHeight)
    {
        isOverview = !isOverview;

        if (isOverview)
        {
            overviewPosition = new Vector3(
                mazeCenter.x,
                mazeCenter.y,
                -10f
            );

            overviewSize = Mathf.Max(mazeWidth, mazeHeight) * 0.5f + 10f;
        }
    }
}