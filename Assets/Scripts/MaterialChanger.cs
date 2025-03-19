using UnityEngine;
using UnityEngine.UI;

public class MaterialChanger : MonoBehaviour
{
    public Material material; // Lista de materiales en el inspector
    public float flexModulus;
    public GameObject barObject;

    private Renderer objectRenderer; // Asigna el Renderer del objeto

    void Start()
    {
        objectRenderer = barObject.GetComponent<Renderer>();
    }

    public void ChangeMaterial()
    {
        objectRenderer.material = material; // Aplica el nuevo material
        barObject.GetComponent<Flex>().E = flexModulus;
    }
}
