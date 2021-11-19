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

    public void RestartGame()
    {
        Stage.StageCount = 0;
        Stage.eventtriggers = new bool[Stage.TotalEventsAmount];
        Stage.currentActivatedTriggers = new List<int>();
        SceneManager.LoadScene(0);
    }

    public void GotoNextStage()
    {
        Stage.StageCount++;
        SceneManager.LoadScene(Stage.StageCount + 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        fade.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(scenename);
    }
}
