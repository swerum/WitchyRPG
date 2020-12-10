using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch_Collision : MonoBehaviour
{
    [Tooltip("The Scene this door should be moving us to.")]
    [SerializeField] string sceneName = "Scene";
    [Tooltip("The direction the player should be moving in order to pass through.")]
    [SerializeField] Vector2 entryDirection = Vector2.zero;
    [SerializeField] Vector2 entryPosition = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
            if (rigidbody != null & Mathf.Sign(rigidbody.velocity.x) == Mathf.Sign(entryDirection.x) & Mathf.Sign(rigidbody.velocity.y) == Mathf.Sign(entryDirection.y))
            {
                //switch Scenes
                SceneManager.LoadScene(sceneName);
                PlayerMovement.Instance.transform.position = entryPosition;
                Camera.main.transform.position = entryPosition;
            }
        }
    }
}
