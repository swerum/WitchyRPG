using UnityEngine;
using WitchyRPG.DialogueSystem;

public enum ItemAction { Nothing, Plow, Seed, Harvest, Water, DeafenSound, PlaceObject};

public class MouseClickHandler : MonoBehaviour
{
    #region variables and initializing
    //public
    [Tooltip("How close do you have to be to your mouse click to place an item there.")]
    [SerializeField] float distanceToPlaceItem = 4;
    [SerializeField] GameObject cursorObject = null;
    ItemAction clickAction = ItemAction.Nothing;
    public ItemAction ClickAction
    {
        set { clickAction = value; }
        get { return clickAction; }
    }
    PlantInfo plant = null;
    public PlantInfo Plant
    {
        set { plant = value; }
        get { return plant; }
    }

    //private
    Camera main;
    ClickableItem clickableItem;
    private void Start() { main = Camera.main; }
    #endregion

    void Update()
    {
        //move mouse tile
        Vector2 mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        cursorObject.transform.position = mousePos;
        mousePos = Utility.SnapToGrid(mousePos);
        transform.position = mousePos;
        //click tile and do action

        if (Input.GetMouseButtonDown(0))
        {
            if (PlayerTalk.Instance.TextBox != null) { PlayerTalk.Instance.TextBox.PlayNextText(); }
            else if (clickableItem != null) clickableItem.LeftClick();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (ShouldPlaceObject()) { PlaceObject(); }
            else if (clickableItem != null) clickableItem.RightClick();
        }
    }

    private bool ShouldPlaceObject()
    {
        float dist = Utility.GetDistance(transform, PlayerMovement.Instance.transform);
        bool canPlace = dist < distanceToPlaceItem & clickAction == ItemAction.PlaceObject;
        bool nothingThere = clickableItem == null || clickableItem.CanPlaceObjectOnThis;
        return canPlace & nothingThere;

    }
    private void PlaceObject()
    {
        GameObject objectToBeInstantiated = PlayerInventory.Instance.GetCurrentItem().item.placableObjectPrefab;
        Instantiate(objectToBeInstantiated).transform.position = transform.position;
        PlayerInventory.Instance.ReduceCurrentItem(1);
    }

    #region keep track of ClickableItem
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ClickableItem clickable = collision.gameObject.GetComponent<ClickableItem>();
        if (clickable != null) { clickableItem = clickable; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ClickableItem clickable = collision.gameObject.GetComponent<ClickableItem>();
        if (clickable == clickableItem) { clickableItem = null; }
    }
    #endregion

    #region singleton
    private static MouseClickHandler _instance;

    public static MouseClickHandler Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
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
