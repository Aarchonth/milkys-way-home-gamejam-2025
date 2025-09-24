using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AdvanceUI : MonoBehaviour
{
    public GameObject advanceObj;

    public void MenuAdv(List<Advancement> adv)
    {
        GameObject hold = GameObject.Find("AdvUI");
        RectTransform[] childs = hold.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform child in childs)
        {
            if (child.name == "Name")
            {
                child.GetComponent<TMP_Text>().text = "Advancements: " + UpdateAdvance(adv);
            }
        }
    }

    public void NewAdvancement(List<Advancement> adv, int index)
    {
        GameObject advObj = Instantiate(advanceObj);
        RectTransform[] childs = advObj.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform child in childs)
        {
            switch (child.name)
            {
                case "Name":
                    child.GetComponent<TMP_Text>().text = adv[index].Name;
                    break;
                case "Image":
                    child.GetComponent<Image>().sprite = adv[index].Img;
                    break;
                case "Description":
                    child.GetComponent<TMP_Text>().text = adv[index].Description;
                    break;
                case "Count":
                    child.GetComponent<TMP_Text>().text = UpdateAdvance(adv);
                    break;
                default:
                    Debug.Log("Nicht benutzt: " + child.name);
                    break;
            }
        }
        StartCoroutine(AdvancementCountdown(advObj));
    }

    private IEnumerator AdvancementCountdown(GameObject obj)
    {
        yield return new WaitForSeconds(10f);
        Destroy(obj);
    }

    public string UpdateAdvance(List<Advancement> adv)
    {
        int count = 0;
        foreach (Advancement advancement in adv)
        {
            if (advancement.Achieved)
            {
                count++;
            }
        }
        return count.ToString() + "/" + adv.Count.ToString();
    }
}
