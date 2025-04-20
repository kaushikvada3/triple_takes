using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    public GameObject[] props; 
    public RectTransform trashButton; 
    public GameObject currentProp; 

    public void SpawnProp(int choice)
    {
        Vector3 spawnxy = Camera.main.transform.position + Camera.main.transform.forward * 3f + Vector3.down * 0.5f;
        GameObject spawn = Instantiate(props[choice], spawnxy, Quaternion.identity);
        spawn.AddComponent<Draggable>();
       // spawn = spawned; 
    }


}
