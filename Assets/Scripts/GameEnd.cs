using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Credits());
        }
    }

    IEnumerator Credits()
    {
        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene("Credits");
    }
}
