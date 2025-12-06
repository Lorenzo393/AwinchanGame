using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyTipe keyTipe;   
    public enum KeyTipe{
        Null,
        DosCinco,
        UnoCuatro,
        Process,
        Principals,
        Library,
        Outside,
    }
}
