using UnityEngine;
using UnityEngine.UI;

public class MaterialChanger : MonoBehaviour
{
    public Material[] materials; // Lista de materiales en el inspector
    public Renderer objectRenderer; // Asigna el Renderer del objeto
    private int currentIndex = 0;

    void Start()
    {
        if (objectRenderer == null)
            objectRenderer = GetComponent<Renderer>();

        if (materials.Length > 0)
            objectRenderer.material = materials[currentIndex]; // Asignar el material inicial
    }

    public void ChangeMaterial()
    {
        currentIndex = (currentIndex + 1) % materials.Length; // Cambia al siguiente material
        objectRenderer.material = materials[currentIndex]; // Aplica el nuevo material
    }
}
