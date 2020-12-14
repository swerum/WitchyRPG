using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange_Click : ClickableItem
{
    [SerializeField] string sceneName = "Scene";
    [SerializeField] Vector2 entryPosition = Vector2.zero;

    /// <summary>
    /// Go to specified scene.
    /// </summary>
    public override void LeftClick()
    {
        SceneManager.LoadScene(sceneName);
        PlayerMovement.Instance.transform.position = entryPosition;
        Camera.main.transform.position = entryPosition;
    }
}
