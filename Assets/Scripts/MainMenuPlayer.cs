using UnityEngine;

public class MainMenuPlayer : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GameObject.Find("Panel").GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit2D = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit2D.collider != null)
        {
            if (hit2D.collider.gameObject.name == "Advancements")
            {
                animator.SetBool("Hover", true);
                animator.SetBool("Open", true);
                GameObject hold = hit2D.collider.gameObject;
                hold.GetComponent<BoxCollider2D>().size = new Vector2(12.25f, 2);
            }
            else
            {
                if (animator.GetBool("Hover") == true)
                {
                    animator.SetBool("Hover", false);
                    animator.SetBool("Open", false);
                    GameObject hold = hit2D.collider.gameObject;
                    hold.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
                }
            }
        }
        else
        {
            if (animator.GetBool("Hover") == true)
            {
                animator.SetBool("Hover", false);
                animator.SetBool("Open", false);
                GameObject hold = GameObject.Find("Advancements");
                hold.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit2D = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit2D.collider != null)
            {
                if (hit2D.collider.gameObject.name == "Advancements")
                {
                    animator.SetBool("Clicked", true);
                    GameObject hold = hit2D.collider.gameObject;
                    hold.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
                }
            }
        }
    }
}