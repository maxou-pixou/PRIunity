using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCircles : MonoBehaviour
{

    public GameObject circlePrefab;       // Pr�fab du cercle
    public List<Vector3> circlePositions; // Liste des positions des cercles
    private int currentCircleIndex = 0;   // Index du cercle � appara�tre ensuite
    private int numberOfCircles;          // Nombre total de positions de cercles
    void Start()
    {
        // R�cup�rer le nombre de positions de cercles
        numberOfCircles = circlePositions.Count;

        Debug.Log($"Nombre total de cercles � g�n�rer : {numberOfCircles}");
        // G�n�rer le premier cercle
        SpawnCircle(currentCircleIndex);
    }

    void SpawnCircle(int index)
    {
        // V�rifie si l'index est dans la plage de la liste
        if (index >= circlePositions.Count)
        {
            Debug.Log("Tous les cercles ont �t� g�n�r�s : ");
            return;
        }

        // Calcule la position r�elle du cercle par rapport � l'objet parent
        Vector3 position = transform.position + circlePositions[index];

        // Instancie le cercle avec la rotation par d�faut du pr�fab
        GameObject circle = Instantiate(circlePrefab, position, Quaternion.Euler(90f, 0f, 0f));

        // Ajuste la taille (si n�cessaire)
        float circleScale = 0.02f;
        circle.transform.localScale = new Vector3(circleScale, circleScale, 1);

        // Configure la hi�rarchie et le tag
        circle.transform.parent = transform;
        circle.tag = "Circle";

        Debug.Log($"Cercle {index + 1} g�n�r� � la position {position}");
    }


    void OnCollisionEnter(Collision collision)
    {
        // V�rifie si l'objet en collision est un cercle et que le Mug est en contact
        if (collision.gameObject.CompareTag("Circle"))
        {
            Debug.Log("Le Mug est entr� en collision avec un cercle.");

            // V�rifie si d'autres cercles restent � afficher
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
                Debug.Log("Tous les cercles ont �t� affich�s.");
            }
        }
    }

}
