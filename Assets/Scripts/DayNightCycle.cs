using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public KeyCode keyCodeL;
    public KeyCode keyCodeR;

    private float secsPerDay = 30f;

    private float rotationSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotationSpeed = 360f / secsPerDay;
    }

    // Update is called once per frame
    void Update()
    {
        float input = 0f;

        if (Input.GetKey(keyCodeL))
        {
            input -= 1f;
        }
        else if (Input.GetKey(keyCodeR))
        {
            input += 1f;
        }

        if (input != 0f) transform.Rotate(Vector3.right * rotationSpeed * input * Time.deltaTime);
    }
}
