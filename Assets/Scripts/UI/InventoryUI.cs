using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private TextMeshProUGUI inventoryText;
    [SerializeField] private Image inventoryImage;

    [SerializeField] private Sprite UnoCuatro;
    [SerializeField] private Sprite DosCinco;
    [SerializeField] private Sprite DosSeis;
    [SerializeField] private Sprite Principals;
    [SerializeField] private Sprite Library;
    [SerializeField] private Sprite Outside;
    [SerializeField] private Sprite Hand;

    private void PlayerInventory_OnCurrentKeyModification(object sender, System.EventArgs e){
        string actualKey = PlayerInventory.Instance.GetCurrentKey().ToString();
        inventoryText.text = actualKey switch
        {
            "UnoCuatro" => "1-4",
            "DosCinco" => "2-5",
            "DosSeis" => "2-6",
            "Principals" => "Principals",
            "Library" => "Library",
            "Outside" => "Outside",
            _ => "" + actualKey,
        };
    }


    private void Start(){
        // LLave inicial
        inventoryText.text = "" + PlayerInventory.Instance.GetCurrentKey();
        // Evento modificacion del la llave actual
        PlayerInventory.Instance.OnCurrentKeyModification += PlayerInventory_OnCurrentKeyModification;
    }
}
