using UnityEngine;
using UnityEngine.UI;

public class GoblinCommandMenu : MonoBehaviour
{
    Goblin goblin;
    public Goblin Goblin { set { goblin = value; } }
    [SerializeField] GameObject tileAssignerPrefab = null;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void DoChores()
    {
        goblin.GetComponent<DirectedMovement>().StartDoingChores();
        gameObject.SetActive(false);
    }

    public void GiveItemBack()
    {
        goblin.Inventory.GiveLastItemBack();
        gameObject.SetActive(false);
    }

    public void GiveGoblinItem()
    {
        Item item = PlayerInventory.Instance.GetCurrentItem();
        if (item == null) return;
        if (goblin.Inventory.AddToInventory(item))
            PlayerInventory.Instance.ReduceCurrentItem(1);
        gameObject.SetActive(false);
    }

    public void AssignFarmableTiles()
    {
        Instantiate(tileAssignerPrefab).GetComponent<TileAssigner>().Goblin = goblin;
        gameObject.SetActive(false);
    }
}
