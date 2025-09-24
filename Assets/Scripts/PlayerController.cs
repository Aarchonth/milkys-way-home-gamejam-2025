using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement pmove;

    private void Start()
    {
        pmove = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    GameObject gameObj = hit.collider.gameObject;
                    if (gameObj.tag == "Advancement")
                    {
                        if (GameManager.instance.advance.Find(x => x.Name == gameObj.name).Achieved != true)
                        {
                            GameManager.instance.advance.Find(x => x.Name == gameObj.name).Achieved = true;
                            GameManager.instance.NewAchieved(gameObj.name);
                        }
                    }
                }
            }
        }
    }
}
