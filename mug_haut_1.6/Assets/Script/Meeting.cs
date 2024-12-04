using System.Collections;
using UnityEngine;

public class Meeting : MonoBehaviour
{
    private RandomCircles randomCirclesScript; // Référence au script RandomCircles
    public GameObject Circle;

    void Start()
    {
        // Trouver le script RandomCircles dans le parent ou ailleurs dans la hiérarchie
        randomCirclesScript = FindObjectOfType<RandomCircles>();

        if (randomCirclesScript == null)
        {
            Debug.LogError("Le script RandomCircles est introuvable !");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Vérifier si l'objet entrant a le tag "GameController"
        if (collision.gameObject.CompareTag("GameController"))
        {
            Debug.Log("Collision détectée avec GameController !");

        }
    }
}

