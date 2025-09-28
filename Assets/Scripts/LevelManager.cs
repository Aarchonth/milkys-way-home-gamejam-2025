using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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

    [Header("Endless Mode")]
    public bool endlessMode = false;
    private bool reversed = false;
    private bool prevReversed = false;
    void Start()
    {
        levels.Add(l1);
        levels.Add(l2);
        levels.Add(l3);
        levels.Add(l4);
        if (endlessMode)
        {
            foreach (var lvl in levels)
            {
                lvl.Sort((a, b) => Random.Range(-1, 2));
            }
        }
        currentLevel.Add(startPoint);
        NewLevel();
    }

    public void NewLevel()
    {
        if (!endlessMode)
            NormalLevel();
        else
            EndlessLevel();
    }

    private void EndlessLevel()
    {
        if (!reversed)
        {
            // --- Switch zwischen Biomen vorw�rts ---
            if (suplevel == -1)
            {
                int switchIdx = level - 1; // �bergang lX -> lX+1
                if (switchIdx < 0 || switchIdx >= switchLevel.Count)
                {
                    Debug.LogWarning("Kein Switch-Prefab f�r diesen �bergang (vorw�rts).");
                    return;
                }

                GameObject prefab = switchLevel[switchIdx];
                GameObject levelObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);

                PlaceLevel(levelObj, rotate180: false);

                currentLevel.Add(levelObj);
                suplevel = 0;
            }
            else
            {
                // --- Normales Level vorw�rts ---
                if (suplevel >= levels[level].Count)
                {
                    suplevel = -1;
                    level++;
                    GameManager.instance.state++;
                }
                else
                {
                    GameObject prefab = levels[level][suplevel];
                    GameObject levelObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);

                    PlaceLevel(levelObj, rotate180: false);

                    currentLevel.Add(levelObj);
                    suplevel++;
                }

                // Ende erreicht -> Wechsel in R�ckw�rtsmodus
                if (level >= levels.Count)
                {
                    reversed = true;
                    level = levels.Count - 2;  // zur�ck zu l4
                    suplevel = -1;             // Switch wird als n�chstes gesetzt
                    GameManager.instance.state = GameManager.LevelState.Merkur;
                }
            }
        }
        else // --- R�ckw�rtslauf ---
        {
            // --- Switch zwischen Biomen r�ckw�rts ---
            if (suplevel == -1)
            {
                int switchIdx = level; // �bergang lX -> lX-1
                if (switchIdx < 0 || switchIdx >= switchLevel.Count)
                {
                    Debug.LogWarning("Kein Switch-Prefab f�r diesen �bergang (r�ckw�rts).");
                    return;
                }

                GameObject prefab = switchLevel[switchIdx];
                GameObject levelObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);

                PlaceLevel(levelObj, rotate180: true);

                currentLevel.Add(levelObj);
                suplevel = levels[level].Count - 1; // fange bei letztem Sublevel an
            }
            else
            {
                // --- Normales Level r�ckw�rts ---
                if (suplevel == 0)
                {
                    suplevel = -1;
                    level--;
                    GameManager.instance.state--;
                }
                else
                {
                    GameObject prefab = levels[level][suplevel];
                    GameObject levelObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);

                    PlaceLevel(levelObj, rotate180: true);

                    currentLevel.Add(levelObj);
                    suplevel--;
                }

                // ganz am Anfang angekommen -> wieder vorw�rts laufen
                if (level < 0)
                {
                    reversed = false;
                    level = 1;         // zur�ck zu l1
                    suplevel = -1;     // Switch als N�chstes
                    GameManager.instance.state = GameManager.LevelState.Jupiter;
                }
            }
        }

        prevReversed = reversed;

        // Letzten Punkt setzen f�r GameManager
        if (currentLevel.Count >= 2)
            GameManager.instance.SetLastPoint(currentLevel[currentLevel.Count - 2].transform, this);

        // �lteste Level wieder entfernen
        if (currentLevel.Count > 10)
        {
            Destroy(currentLevel[0]);
            currentLevel.RemoveAt(0);
        }
    }

    /// <summary>
    /// Positioniert ein neues Level-Objekt an das letzte Ende und dreht es optional.
    /// </summary>
    private void PlaceLevel(GameObject levelObj, bool rotate180)
    {
        if (rotate180 || prevReversed)
        {
            levelObj.transform.Rotate(0, 0, 180);
        }
        Transform startT = levelObj.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "Start");
        Transform cendT = levelObj.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "Ende");
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
        if (rotate180 || prevReversed)
        {
            startT.transform.localPosition = new Vector3(startT.transform.localPosition.x * -1, startT.transform.localPosition.y * -1, 0);
            cendT.transform.localPosition = new Vector3(cendT.transform.localPosition.x * -1, cendT.transform.localPosition.y * -1, 0);
            if (startT.localPosition == new Vector3(10.3f, -4.9f, startT.position.z))
            {
                startT.localPosition = new Vector3(8.9f, -5.9f, startT.localPosition.z);
                cendT.localPosition = new Vector3(8.9f, 4.9f, cendT.localPosition.z);
            }
        }

        Vector3 offset = levelObj.transform.position - startT.position;
        levelObj.transform.position = endT.position + offset;

        if (!rotate180 && prevReversed)
        {
            levelObj.transform.Rotate(0, 0, 0);
            startT.transform.localPosition = new Vector3(startT.transform.localPosition.x * -1, startT.transform.localPosition.y * -1, 0);
            cendT.transform.localPosition = new Vector3(cendT.transform.localPosition.x * -1, cendT.transform.localPosition.y * -1, 0);
        }
    }

    private void NormalLevel()
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
                Debug.LogWarning("Keine weiteren Levels verf�gbar.");
                return;
            }

            if (suplevel < 0 || suplevel >= levels[level].Count)
            {
                Debug.LogWarning("Ung�ltiger Sublevel-Index. Setze auf 0 zur�ck.");
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

        if (currentLevel.Count > 10)
        {
            Destroy(currentLevel[0]);
            currentLevel.RemoveAt(0);
        }
    }
}