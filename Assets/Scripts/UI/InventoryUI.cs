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
        // inventoryText.text = actualKey switch
        // {
        //     "UnoCuatro" => "1-4",
        //     "DosCinco" => "2-5",
        //     "DosSeis" => "2-6",
        //     "Principals" => "Principals",
        //     "Library" => "Library",
        //     "Outside" => "Outside",
        //     _ => "" + actualKey,
        // };

        switch(actualKey){
            case "UnoCuatro":
                inventoryText.text = "1-4";
                inventoryImage.sprite = UnoCuatro;
                break;
            case "DosCinco":
                inventoryText.text = "2-5";
                inventoryImage.sprite = DosCinco;
                break;
            case "DosSeis":
                inventoryText.text = "2-6";
                inventoryImage.sprite = DosSeis;
                break;
            case "Principals":
                inventoryText.text = "Principals";
                inventoryImage.sprite = Principals;
                break;
            case "Library":
                inventoryText.text = "Library";
                inventoryImage.sprite = Library;
                break;
            case "Outside":
                inventoryText.text = "Outside";
                inventoryImage.sprite = Outside;
                break;
            default:
                inventoryText.text = "Hand";
                inventoryImage.sprite = Hand;
                break;
        }
    }


    private void Start(){
        // LLave inicial
        inventoryText.text = "" + PlayerInventory.Instance.GetCurrentKey();
        // Evento modificacion del la llave actual
        PlayerInventory.Instance.OnCurrentKeyModification += PlayerInventory_OnCurrentKeyModification;
    }
}
