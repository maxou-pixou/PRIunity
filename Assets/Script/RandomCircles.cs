using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCircles : MonoBehaviour
{

    public GameObject circlePrefab;       // Préfab du cercle
    public List<Vector3> circlePositions; // Liste des positions des cercles
    private int currentCircleIndex = 0;   // Index du cercle à apparaître ensuite
    private int numberOfCircles;          // Nombre total de positions de cercles
    void Start()
    {
        // Récupérer le nombre de positions de cercles
        numberOfCircles = circlePositions.Count;

        Debug.Log($"Nombre total de cercles à générer : {numberOfCircles}");
        // Générer le premier cercle
        SpawnCircle(currentCircleIndex);
    }

    void SpawnCircle(int index)
    {
        // Vérifie si l'index est dans la plage de la liste
        if (index >= circlePositions.Count)
        {
            Debug.Log("Tous les cercles ont été générés : ");
            return;
        }

        // Calcule la position réelle du cercle par rapport à l'objet parent
        Vector3 position = transform.position + circlePositions[index];

        // Instancie le cercle avec la rotation par défaut du préfab
        GameObject circle = Instantiate(circlePrefab, position, Quaternion.Euler(90f, 0f, 0f));

        // Ajuste la taille (si nécessaire)
        float circleScale = 0.02f;
        circle.transform.localScale = new Vector3(circleScale, circleScale, 1);

        // Configure la hiérarchie et le tag
        circle.transform.parent = transform;
        circle.tag = "Circle";

        Debug.Log($"Cercle {index + 1} généré à la position {position}");
    }


    void OnCollisionEnter(Collision collision)
    {
        // Vérifie si l'objet en collision est un cercle et que le Mug est en contact
        if (collision.gameObject.CompareTag("Circle"))
        {
            Debug.Log("Le Mug est entré en collision avec un cercle.");

            // Vérifie si d'autres cercles restent à afficher
            if (currentCircleIndex < numberOfCircles - 1)
            {
                // Supprime le cercle actuel
                Destroy(collision.gameObject);

                // Passe au cercle suivant
                currentCircleIndex++;
                SpawnCircle(currentCircleIndex);
            }
            else
            {
                Debug.Log("Tous les cercles ont été affichés.");
            }
        }
    }

}
