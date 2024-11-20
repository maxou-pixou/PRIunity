using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    void Start()
    {
        // Accéder au matériau attaché au MeshRenderer du cube
        Renderer cubeRenderer = GetComponent<Renderer>();

        // Définir la couleur en utilisant des valeurs RGB (de 0 à 255)
        float r = 30f / 255f; 
        float g = 139f / 255f;
        float b = 32f / 255f;  

        // Appliquer la couleur
        cubeRenderer.material.color = new Color(r, g, b);
    }
}
