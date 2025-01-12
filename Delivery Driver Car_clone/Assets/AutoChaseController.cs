using UnityEngine;

public class SmartEnemyController : MonoBehaviour
{
    public AudioSource musicAudioSfx;
    public AudioClip musicSiren;

    [Header("Target Settings")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float detectionRange = 15f;

    [Header("Movement Settings")]
    [SerializeField] private float normalSpeed = 8f;
    [SerializeField] private float ramSpeed = 12f;
    [SerializeField] private float rotateSpeed = 250f;
    [SerializeField] private float ramDistance = 5f;

    [Header("Obstacle Detection")]
    [SerializeField] private float sensorLength = 3f;
    [SerializeField] private float frontSensorOffset = 0.5f;
    [SerializeField] private float sideSensorAngle = 45f;
    [SerializeField] private LayerMask obstacleLayer; // Layer cho chướng ngại vật

    private Rigidbody2D rb;
    private bool isCharging = false;
    private bool isAvoidingObstacle = false;
    private float avoidanceDirection = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (playerTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerTarget = player.transform;
        }
        musicAudioSfx.clip = musicSiren;
        musicAudioSfx.loop = true;
    }

    void FixedUpdate()
    {
        if (playerTarget == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (!musicAudioSfx.isPlaying)
            {
                musicAudioSfx.Play();
            }
            CheckObstacles();
            ChaseAndAvoid();
        }
        else
        {
            musicAudioSfx.Stop();
        }
    }

    void CheckObstacles()
    {
        isAvoidingObstacle = false;
        Vector2 frontStartPos = (Vector2)transform.position + ((Vector2)transform.up * frontSensorOffset);

        // Tia phía trước
        RaycastHit2D frontHit = Physics2D.Raycast(frontStartPos, transform.up, sensorLength, obstacleLayer);

        // Tia bên trái
        Vector2 leftDirection = Quaternion.Euler(0, 0, sideSensorAngle) * transform.up;
        RaycastHit2D leftHit = Physics2D.Raycast(frontStartPos, leftDirection, sensorLength, obstacleLayer);

        // Tia bên phải
        Vector2 rightDirection = Quaternion.Euler(0, 0, -sideSensorAngle) * transform.up;
        RaycastHit2D rightHit = Physics2D.Raycast(frontStartPos, rightDirection, sensorLength, obstacleLayer);

        // Vẽ các tia raycast để debug
        Debug.DrawRay(frontStartPos, transform.up * sensorLength, Color.green);
        Debug.DrawRay(frontStartPos, leftDirection * sensorLength, Color.yellow);
        Debug.DrawRay(frontStartPos, rightDirection * sensorLength, Color.yellow);

        if (frontHit.collider != null || leftHit.collider != null || rightHit.collider != null)
        {
            isAvoidingObstacle = true;

            if (frontHit.collider != null)
            {
                if (leftHit.collider == null)
                    avoidanceDirection = 1f;
                else if (rightHit.collider == null)
                    avoidanceDirection = -1f;
                else
                    avoidanceDirection = (Random.value > 0.5f) ? 1f : -1f;
            }
            else if (leftHit.collider != null)
                avoidanceDirection = -0.5f;
            else if (rightHit.collider != null)
                avoidanceDirection = 0.5f;
        }
    }

    void ChaseAndAvoid()
    {
        Vector2 directionToPlayer = (playerTarget.position - transform.position).normalized;
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;

        if (isAvoidingObstacle)
        {
            // Xoay để tránh chướng ngại vật
            transform.Rotate(0, 0, avoidanceDirection * rotateSpeed * Time.fixedDeltaTime);

            // Di chuyển với tốc độ thường
            Vector2 movement = (Vector2)transform.up * normalSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            // Xoay về phía người chơi
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.Euler(0, 0, angleToPlayer),
                rotateSpeed * Time.fixedDeltaTime
            );

            // Kiểm tra góc hướng về người chơi
            float facingAngle = Vector2.Angle(transform.up, directionToPlayer);

            if (facingAngle < 30f)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);
                isCharging = distanceToPlayer <= ramDistance;

                // Di chuyển với tốc độ tương ứng
                float currentSpeed = isCharging ? ramSpeed : normalSpeed;
                Vector2 movement = (Vector2)transform.up * currentSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + movement);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isCharging = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ramDistance);
    }
}