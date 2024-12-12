using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText; // UI text for displaying the timer
    private float elapsedTime = 0f; // Timer value

    void Update()
    {
        
            elapsedTime += Time.deltaTime;
            timerText.text = elapsedTime.ToString("F2"); // Format time to 2 decimal places
    }

}