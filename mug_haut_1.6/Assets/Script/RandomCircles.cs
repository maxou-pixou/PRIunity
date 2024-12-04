using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomCircles : MonoBehaviour
{
    public int numberOfCircles = 10;  // Nombre de cercles � g�n�rer
    public GameObject circlePrefab;  // Pr�fab pour le cercle (assurez-vous d'en avoir un configur� avec un Sprite circulaire)
    public Vector2 areaSize = new Vector2(10f, 10f);  // Dimensions de la zone o� les cercles seront g�n�r�s

    void Start()
    {
        GenerateCircles();
    }
    void OnDrawGizmos()
    {
        // D�finir la couleur de la zone
        Gizmos.color = Color.green;

        // Dessiner une bo�te ou un rectangle repr�sentant l'areaSize
        Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, 0f, areaSize.y));
    }

    void GenerateCircles()
    {
        for (int i = 0; i < numberOfCircles; i++)
        {
            float randomX = Random.Range(-areaSize.x / 2, areaSize.x / 2);
            float randomZ = Random.Range(-areaSize.y / 2, areaSize.y / 2);

            // Ajouter la position du GameObject parent
            Vector3 position = transform.position + new Vector3(randomX, 0, randomZ);
            // Affiche la position globale (X, Y, Z) du GameObject dans la console
            // Afficher la position g�n�r�e dans la console
            Debug.Log($"Cercle {i + 1} - Position g�n�r�e : {position}");
            GameObject circle = Instantiate(circlePrefab, position, Quaternion.Euler(90f, 0f, 0f));


            // Taille du Cercle
            float randomScale = 0.02f;  
            circle.transform.localScale = new Vector3(randomScale, randomScale, 1);

            // Changer la couleur al�atoirement si un SpriteRenderer est pr�sent
            SpriteRenderer renderer = circle.GetComponent<SpriteRenderer>();
            

            // Optionnel : Ajouter le cercle � un parent pour organiser la hi�rarchie
            circle.transform.parent = transform;
        }
    }
}

