using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject targetAsset;
    public KeyCode toggleKey;
    public bool initState;
    public float distanceFromCamera = 2f;

    private Camera mainCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetAsset.SetActive(initState);
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            targetAsset.SetActive(!targetAsset.activeSelf);
        }

        if (targetAsset.activeSelf)
        {
            targetAsset.transform.position = mainCamera.transform.position + 
                                             mainCamera.transform.forward * distanceFromCamera;
            targetAsset.transform.rotation = mainCamera.transform.rotation;
        }
    }
}
