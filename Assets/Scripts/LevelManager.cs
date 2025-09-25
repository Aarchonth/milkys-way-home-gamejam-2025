using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject startPoint;
    public List<GameObject> l1 = new();
    public List<GameObject> l2 = new();
    public List<GameObject> l3 = new();
    public List<GameObject> l4 = new();

    public List<GameObject> switchLevel = new();

    private List<GameObject> currentLevel = new();
    private List<List<GameObject>> levels = new();

    private int level = 0;
    private int suplevel = 0;

    // 1h + 3h

    void Start()
    {
        levels.Add(l1);
        levels.Add(l2);
        levels.Add(l3);
        levels.Add(l4);
        currentLevel.Add(startPoint);
        NewLevel();
    }

    public void NewLevel()
    {
        if (suplevel == -1)
        {
            GameObject prefab = switchLevel[level - 1];
            GameObject levelObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);

            Transform startT = levelObj.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "Start");
            GameObject lastObj = currentLevel.LastOrDefault();
            Transform endT = lastObj != null
                ? lastObj.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "Ende")
                : null;

            if (startT == null || endT == null)
            {
                Debug.LogError($"Anker-Transforms fehlen. Start gefunden: {startT != null}, End gefunden: {endT != null}");
                Destroy(levelObj);
                return;
            }

            Vector3 offset = levelObj.transform.position - startT.position;
            levelObj.transform.position = endT.position + offset;
            currentLevel.Add(levelObj);
            suplevel = 0;
        }
        else
        {

            if (level < 0 || level >= levels.Count)
            {
                Debug.LogWarning("Keine weiteren Levels verfügbar.");
                return;
            }

            if (suplevel < 0 || suplevel >= levels[level].Count)
            {
                Debug.LogWarning("Ungültiger Sublevel-Index. Setze auf 0 zurück.");
                suplevel = 0;
            }

            GameObject prefab = levels[level][suplevel];
            GameObject levelObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);

            Transform startT = levelObj.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "Start");
            GameObject lastObj = currentLevel.LastOrDefault();
            Transform endT = lastObj != null
                ? lastObj.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "Ende")
                : null;

            if (startT == null || endT == null)
            {
                Debug.LogError($"Anker-Transforms fehlen. Start gefunden: {startT != null}, End gefunden: {endT != null}");
                Destroy(levelObj);
                return;
            }

            Vector3 offset = levelObj.transform.position - startT.position;
            levelObj.transform.position = endT.position + offset;
            currentLevel.Add(levelObj);

            if (levels[level].Count - 1 > suplevel)
            {
                suplevel++;
            }
            else
            {
                suplevel = -1;
                level++;
                GameManager.instance.state++;
            }
        }

        GameManager.instance.SetLastPoint(currentLevel[currentLevel.Count - 2].transform, this);

        if (currentLevel.Count > 5)
        {
            Debug.Log("Lösche altes Level");
            Destroy(currentLevel[0]);
            currentLevel.RemoveAt(0);
        }
    }
}