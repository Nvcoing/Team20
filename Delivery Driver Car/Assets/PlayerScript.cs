using TMPro;
using UnityEngine;
//using static Unity.Burst.Intrinsics.X86.Avx;
//using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerScript : MonoBehaviour
{
    public int yourCoin = 1000;
    public TMP_Text currentCoin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateCoinsUI();
    }

    // Ham thanh toan xe
    public bool SpeedCar(int price)
    {
        if (yourCoin >= price)
        {
            yourCoin -= price;
            Debug.Log("Mua xe thanh cong!");
            UpdateCoinsUI();
            return true;
        }
        else
        {
            Debug.Log("Khong du tien!");
            return false;
        }
    }

        // Hàm cập nhật giao diện số tiền
        private void UpdateCoinsUI()
        {
            currentCoin.text = yourCoin + ""; 
        }

}
