using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Hago singleton al inventario del jugador
    public static PlayerInventory Instance {get; private set;}
    // Evento para coordinar valor de llave con el ui
    public event EventHandler OnCurrentKeyModification;
    // Lista de tipos de llaves
    [SerializeField] private List<Key.KeyTipe> keysList;
    private Key.KeyTipe currentKey;
    private int listIndex = 0;

    private void GameInput_OnInventoryLeft(object sender, System.EventArgs e){
        if((listIndex - 1) < 0){
            listIndex = (keysList.Count - 1);
        } else{
            listIndex--;
        }
        currentKey = keysList[listIndex];
        OnCurrentKeyModification?.Invoke(this, EventArgs.Empty);
        //Debug.Log("Left Inventory");
    }
    private void GameInput_OnInventoryRight(object sender, System.EventArgs e){
        if((listIndex + 1) > (keysList.Count - 1)){
            listIndex = 0;
        } else{
            listIndex++;
        }
        currentKey = keysList[listIndex];
        OnCurrentKeyModification?.Invoke(this, EventArgs.Empty);
        //Debug.Log("Right Inventory");
    }
    private void Awake(){
        // Inicializo la instancia del objeto jugador
        Instance = this;
        // Inicializo el inventario
        keysList = new List<Key.KeyTipe>();
        AddKey(Key.KeyTipe.Null);
        // Inicializo la llave actual
        currentKey = Key.KeyTipe.Null;
    }
    private void Start(){
        // Me suscribo al evento inventario izquierda
        GameInput.Instance.OnInventoryLeft += GameInput_OnInventoryLeft;
        // Me suscribo al evento inventario izquierda
        GameInput.Instance.OnInventoryRight += GameInput_OnInventoryRight;
    }
    public void AddKey(Key.KeyTipe key){
        keysList.Add(key);
        Debug.Log("Grab: " + key);
    }
    public void RemoveKey(Key.KeyTipe key){
        keysList.Remove(key);
        listIndex = 0;
        currentKey = keysList[listIndex];
        
        OnCurrentKeyModification?.Invoke(this,EventArgs.Empty);
    }
    public Key.KeyTipe GetCurrentKey(){
        return currentKey;
    }
}
