using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageEnd : MonoBehaviour
{
    public string scenename;
    public Stage stage;
    [SerializeField] private GameObject fade;
    public void GotoStage(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        stage.UpdateEventTriggers();
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        fade.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        GotoStage(scenename);
    }
}
