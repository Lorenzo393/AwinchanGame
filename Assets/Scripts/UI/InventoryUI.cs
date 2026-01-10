using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI inventoryText;
    private void PlayerInventory_OnCurrentKeyModification(object sender, System.EventArgs e){
        inventoryText.text = "Inventario: " + PlayerInventory.Instance.GetCurrentKey();
    }
    private void Awake(){
        inventoryText = GetComponent<TextMeshProUGUI>();
    }
    private void Start(){
        // LLave inicial
        inventoryText.text = "Inventario: " + PlayerInventory.Instance.GetCurrentKey();
        // Evento modificacion del la llave actual
        PlayerInventory.Instance.OnCurrentKeyModification += PlayerInventory_OnCurrentKeyModification;
    }
}
