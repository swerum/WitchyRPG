using UnityEngine;
using UnityEngine.UI;

public class GoblinCommandMenu : MonoBehaviour
{
    Goblin goblin;
    public Goblin Goblin { set { goblin = value; } }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void DoChores() { goblin.GetComponent<DirectedMovement>().StartDoingChores();  }
    public void GiveItemBack() { goblin.Inventory.GiveLastItemBack(); }
    public void AssignFarmableTiles() { }
    public void GiveGoblinItem()
    {
        Item item = PlayerInventory.Instance.GetCurrentItem();
        if (item == null) return;
        if (goblin.Inventory.AddToInventory(item))
            PlayerInventory.Instance.ReduceCurrentItem(1);
    }
}
