using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnFromTileAssigner : MonoBehaviour
{
    [SerializeField] Vector2 position = new Vector2(-350, 220);

    TileAssigner tileAssigner = null;
    public TileAssigner TileAssigner { set { tileAssigner = value; } }

    Goblin goblin;
    public Goblin Goblin { set { goblin = value; } }

    Camera main;

    private void Start()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        transform.SetParent(canvas.transform);
        GetComponent<RectTransform>().anchoredPosition = position;
        main = Camera.main;
    }

    /// <summary>
    /// Destroys self, unfreezes character, camera follows player, mouseClickHandler turns on again
    /// </summary>
    public void ReturnToGame()
    {
        //Debug.Log(tileAssigner);
        tileAssigner.DestroySelf();
        PlayerMovement.Instance.UnFreeze();
        main.GetComponent<CameraMovement>().Movement = CameraMovement.MovementType.Following;
        MouseClickHandler.Instance.gameObject.SetActive(true);
    }
}
