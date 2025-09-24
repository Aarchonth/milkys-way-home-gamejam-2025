using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Advancement/Advancement")]
public class Advancement : ScriptableObject
{
    public int AdvanceID;
    public string Name;
    public string Description;
    [HideInInspector]
    public bool Achieved;
    public Sprite Img;

    public Advancement(int advanceID, string name, string description, bool achieved, Sprite img)
    {
        AdvanceID = advanceID;
        Name = name;
        Description = description;
        Achieved = achieved;
        Img = img;
    }

}
