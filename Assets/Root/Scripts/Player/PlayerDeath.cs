using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : PlayerStateBehaviour
{
    public override void OnStateSetup()
    {
        // nothing
    }

    public override void OnStateEnter()
    {
        // Begin waiting for scene reset
        StartCoroutine(CoReloadScene());
    }

    public override void OnStateUpdate()
    {
        // nothing
    }

    public override void OnStateExit()
    {
        // nothing
    }

    private IEnumerator CoReloadScene()
    {
        yield return new WaitForSeconds(3);
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex, LoadSceneMode.Single);
    }
}
