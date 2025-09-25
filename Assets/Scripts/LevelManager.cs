using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject startPoint;
    public List<GameObject> l1 = new();
    public List<GameObject> l2 = new();
    public List<GameObject> l3 = new();
    public List<GameObject> l4 = new();

    public List<GameObject> switchLevel = new();

    private List<List<GameObject>> levels = new();

    private int level = 0;
    private int suplevel = 0;

    void Start()
    {
        levels[0] = l1;
        levels[1] = l2;
        levels[2] = l3;
        levels[3] = l4;
    }

    public void NewLevel()
    {
        GameObject levelObj = Instantiate(levels[level][suplevel], new Vector3(0, 0, 0), Quaternion.identity);
    }
}