using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    /*
        Null,
        UnoCuatro,
        DosCinco,
        DosSeis,
        Principals,
        Library,
        Outside,
    */
    private TextMeshProUGUI inventoryText;
    private void PlayerInventory_OnCurrentKeyModification(object sender, System.EventArgs e){
        string actualKey = PlayerInventory.Instance.GetCurrentKey().ToString();
        inventoryText.text = actualKey switch
        {
            "UnoCuatro" => "Inventory: " + "1-4",
            "DosCinco" => "Inventory: " + "2-5",
            "DosSeis" => "Inventory: " + "2-6",
            "Principals" => "Inventory: " + "Principals",
            "Library" => "Inventory: " + "Library",
            "Outside" => "Inventory: " + "Outside",
            _ => "Inventory: " + actualKey,
        };
    }
    private void Awake(){
        inventoryText = GetComponent<TextMeshProUGUI>();
    }
    private void Start(){
        // LLave inicial
        inventoryText.text = "Inventory: " + PlayerInventory.Instance.GetCurrentKey();
        // Evento modificacion del la llave actual
        PlayerInventory.Instance.OnCurrentKeyModification += PlayerInventory_OnCurrentKeyModification;
    }
}
