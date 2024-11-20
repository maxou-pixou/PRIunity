using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    void Start()
    {
        // Acc�der au mat�riau attach� au MeshRenderer du cube
        Renderer cubeRenderer = GetComponent<Renderer>();

        // D�finir la couleur en utilisant des valeurs RGB (de 0 � 255)
        float r = 30f / 255f; 
        float g = 139f / 255f;
        float b = 32f / 255f;  

        // Appliquer la couleur
        cubeRenderer.material.color = new Color(r, g, b);
    }
}
