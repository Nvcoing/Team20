using UnityEngine;

public class DeliveryArrow : MonoBehaviour
{
    public Transform[] packageLocations;  
    public Transform[] deliveryPoints;    
    public GameObject arrowPrefab;        
    private GameObject currentArrow;     
    private Transform currentTarget;      

    void Start()
    {
        FindClosestPackage();  
    }

    void Update()
    {
        if (currentTarget != null)
        {
            UpdateArrowPosition();  
        }
    }

    void FindClosestPackage()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestPackage = null;

        foreach (Transform package in packageLocations)
        {
            float distance = Vector3.Distance(transform.position, package.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPackage = package;
            }
        }

        if (closestPackage != null)
        {
            currentTarget = closestPackage;
            CreateArrow();  
        }
    }

    void FindClosestDeliveryPoint()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestDelivery = null;

        foreach (Transform point in deliveryPoints)
        {
            float distance = Vector3.Distance(transform.position, point.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDelivery = point;
            }
        }

        if (closestDelivery != null)
        {
            currentTarget = closestDelivery;
            UpdateArrowPosition();  
        }
    }

    void CreateArrow()
    {
        if (currentArrow != null)
        {
            Destroy(currentArrow);  
        }

        currentArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        UpdateArrowPosition();  
    }

    void UpdateArrowPosition()
    {
        currentArrow.transform.position = transform.position;
        currentArrow.transform.LookAt(currentTarget);  
    }

    
    public void OnPackagePickedUp()
    {
        FindClosestDeliveryPoint();  
    }

    
    public void OnDeliveryCompleted()
    {
        FindClosestPackage();  
    }
}
