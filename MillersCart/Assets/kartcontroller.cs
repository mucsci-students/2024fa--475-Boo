using UnityEngine;

public class KartController : MonoBehaviour
{
    private KartMechanics kartMechanics;

    void Start()
    {
        kartMechanics = GetComponent<KartMechanics>();
    }

    void Update()
    {
        // Get player input
        float throttleInput = Input.GetAxis("Vertical"); // W/S or Up/Down Arrow
        float steeringInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        bool isBraking = Input.GetKey(KeyCode.Space); // Space for brake

        // Send input to the mechanics
        if (isBraking)
        {
            kartMechanics.Brake();
        }
        else
        {
            kartMechanics.Drive(throttleInput);
            kartMechanics.Turn(steeringInput);
        }
    }
}
