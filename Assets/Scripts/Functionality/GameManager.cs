using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject cameraPrefab = null;
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject mouseClickManager = null;
    Camera mainCamera;

    void Start()
    {
        //check if the items we're creating already exist
        if (mainCamera != null || PlayerMovement.Instance != null) return;
        Instantiate(mouseClickManager);
        //place camera and player in the middle of the room
        Vector2 startPosition = GameObject.FindGameObjectWithTag("RoomBorders").GetComponent<LevelBounds>().Bounds.center;
        Transform p = Instantiate(player).transform;
        p.position = startPosition;
        CameraMovement main = Instantiate(cameraPrefab).GetComponent<CameraMovement>();
        main.transform.position = startPosition;
        main.Follow = p;
        mainCamera = main.GetComponent<Camera>();
        //Note: You may want to create a class RoomInfo with startPosition and such or else handle this via the saving code
    }


    #region singleton
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null & _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion
}
