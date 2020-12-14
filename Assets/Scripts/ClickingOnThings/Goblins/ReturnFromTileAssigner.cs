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

    private void Start()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        transform.SetParent(canvas.transform);
        GetComponent<RectTransform>().anchoredPosition = position;
        //transform.position = position;
    }

    public void ReturnToGame()
    {
        //Debug.Log(tileAssigner);
        tileAssigner.DestroySelf();
        PlayerMovement.Instance.UnFreeze();
        Camera.main.GetComponent<CameraMovement>().Movement = CameraMovement.MovementType.Following;
        MouseClickHandler.Instance.gameObject.SetActive(true);
    }
}
