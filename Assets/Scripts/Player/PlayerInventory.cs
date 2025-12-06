using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Lista de tipos de llaves
    [SerializeField] private List<Key.KeyTipe> keysList;
    private Key.KeyTipe currentKey;
    private void GameInput_OnInventoryLeft(object sender, System.EventArgs e){
        Debug.Log("Inventario L");
        Debug.Log("Current key: " + currentKey);
    }
    private void GameInput_OnInventoryRight(object sender, System.EventArgs e){
        Debug.Log("Inventario R");
        Debug.Log("Current key: " + currentKey);
    }
    private void Start(){
        // Inicializo el inventario
        keysList = new List<Key.KeyTipe>();
        keysList.Add(Key.KeyTipe.Null);
        // Inicializo la llave actual
        currentKey = Key.KeyTipe.Null;
        // Me suscribo al evento inventario izquierda
        GameInput.Instance.OnInventoryLeft += GameInput_OnInventoryLeft;
        // Me suscribo al evento inventario izquierda
        GameInput.Instance.OnInventoryRight += GameInput_OnInventoryRight;
    }

}
