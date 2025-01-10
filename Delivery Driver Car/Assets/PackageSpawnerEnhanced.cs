using UnityEngine;

public class SimplePackageSpawner : MonoBehaviour
{
    [SerializeField] private GameObject packagePrefab; // Prefab Package
    [SerializeField] private Transform roadsParent;    // Object Roads chứa các roads
    [SerializeField] private float spawnInterval = 5f; // Thời gian giữa mỗi lần spawn
    [SerializeField] private int maxPackages = 3;      // Số lượng package tối đa

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        // Đếm ngược thời gian
        timer -= Time.deltaTime;

        // Kiểm tra thời gian và số lượng package hiện tại
        if (timer <= 0 && GameObject.FindGameObjectsWithTag("Package").Length < maxPackages)
        {
            SpawnRandomPackage();
            timer = spawnInterval; // Reset timer
        }
    }

    void SpawnRandomPackage()
    {
        if (roadsParent != null && roadsParent.childCount > 0)
        {
            // Chọn ngẫu nhiên một road
            int randomRoadIndex = Random.Range(0, roadsParent.childCount);
            Transform selectedRoad = roadsParent.GetChild(randomRoadIndex);

            // Lấy kích thước của road được chọn
            Renderer roadRenderer = selectedRoad.GetComponent<Renderer>();
            if (roadRenderer != null)
            {
                // Tính toán vị trí ngẫu nhiên trong phạm vi của road
                Vector3 roadBounds = roadRenderer.bounds.size;
                Vector3 roadPosition = selectedRoad.position;

                float randomX = Random.Range(
                    roadPosition.x - roadBounds.x / 2,
                    roadPosition.x + roadBounds.x / 2
                );
                float randomY = Random.Range(
                    roadPosition.y - roadBounds.y / 2,
                    roadPosition.y + roadBounds.y / 2
                );

                // Tạo package mới
                Vector3 spawnPosition = new Vector3(randomX, randomY, roadPosition.z);
                Instantiate(packagePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}