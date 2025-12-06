using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI inventoryText;

    private void GameInput_OnInventoryLeft(object sender, System.EventArgs e){
        inventoryText.text = "Inventario: " + PlayerInventory.Instance.currentKey;
    }
    private void GameInput_OnInventoryRight(object sender, System.EventArgs e){
        inventoryText.text = "Inventario: " + PlayerInventory.Instance.currentKey;
    }
    private void Awake(){
        inventoryText = GetComponent<TextMeshProUGUI>();
    }
    private void Start(){
        // LLave inicial
        inventoryText.text = "Inventario: " + PlayerInventory.Instance.currentKey;
        // Evento inventario izquierda
        GameInput.Instance.OnInventoryLeft += GameInput_OnInventoryLeft;
        // Evento inventario derecha
        GameInput.Instance.OnInventoryRight += GameInput_OnInventoryRight;
    }
}
