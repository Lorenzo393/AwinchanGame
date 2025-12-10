using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public static StaminaBar Instance;

    [SerializeField] private Image staminaFill;

    private void Awake(){
        Instance = this;
    }

    public void UpdateStamina(float current, float max){
        staminaFill.fillAmount = current / max;
    }
}
