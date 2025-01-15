using UnityEngine;
using TMPro;

public class DisplayPositionTMP : MonoBehaviour
{
    public GameObject targetObject; // Le GameObject contr�l� (mug)
    public TextMeshPro textMeshPro; // Texte pour afficher la position

    [Header("Position Initiale")]
    public Vector3 initialPosition = new Vector3(0, 0.5f, 1); // Position initiale d�finie

    private bool hasSetInitialPosition = false; // V�rifie si la position initiale a �t� appliqu�e

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target Object n'est pas assign� !");
        }
    }

    void Update()
    {
        // V�rifier si les r�f�rences sont d�finies
        if (targetObject != null && textMeshPro != null)
        {
            if (!hasSetInitialPosition)
            {
                // Appliquer la position initiale une seule fois
                targetObject.transform.position += initialPosition;
                hasSetInitialPosition = true; // Marquer comme initialis�
            }

            // R�cup�rer et afficher la position actuelle mise � jour par Motive Tracker
            Vector3 position = targetObject.transform.position;
            textMeshPro.text = $"Position : X = {position.x:F2}, Y = {position.y:F2}, Z = {position.z:F2}";
        }
    }
}
