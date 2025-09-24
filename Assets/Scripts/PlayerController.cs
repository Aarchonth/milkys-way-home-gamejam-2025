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
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit2D.collider != null)
            {
                GameObject gameObj = hit2D.collider.gameObject;
                if (gameObj.tag == "Advancement")
                {
                    var advancement = GameManager.instance.advance.Find(x => x.Name == gameObj.name);
                    if (advancement != null && advancement.Achieved != true)
                    {
                        advancement.Achieved = true;
                        GameManager.instance.NewAchieved(gameObj.name);
                    }
                }
            }
        }
    }
}
