using UnityEngine;
using TMPro;

public class DisplayPositionTMP : MonoBehaviour
{
    public GameObject targetObject; // Le GameObject contrôlé (mug)
    public TextMeshPro textMeshPro; // Texte pour afficher la position

    [Header("Position Initiale")]
    public Vector3 initialPosition = new Vector3(0, 0.5f, 1); // Position initiale définie

    private bool hasSetInitialPosition = false; // Vérifie si la position initiale a été appliquée

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target Object n'est pas assigné !");
        }
    }

    void Update()
    {
        // Vérifier si les références sont définies
        if (targetObject != null && textMeshPro != null)
        {
            if (!hasSetInitialPosition)
            {
                // Appliquer la position initiale une seule fois
                targetObject.transform.position += initialPosition;
                hasSetInitialPosition = true; // Marquer comme initialisé
            }

            // Récupérer et afficher la position actuelle mise à jour par Motive Tracker
            Vector3 position = targetObject.transform.position;
            textMeshPro.text = $"Position : X = {position.x:F2}, Y = {position.y:F2}, Z = {position.z:F2}";
        }
    }
}
