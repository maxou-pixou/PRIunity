using System.Collections;
using UnityEngine;

public class Meeting : MonoBehaviour
{
    private RandomCircles randomCirclesScript; // R�f�rence au script RandomCircles
    public GameObject Circle;

    void Start()
    {
        // Trouver le script RandomCircles dans le parent ou ailleurs dans la hi�rarchie
        randomCirclesScript = FindObjectOfType<RandomCircles>();

        if (randomCirclesScript == null)
        {
            Debug.LogError("Le script RandomCircles est introuvable !");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // V�rifier si l'objet entrant a le tag "GameController"
        if (collision.gameObject.CompareTag("GameController"))
        {
            Debug.Log("Collision d�tect�e avec GameController !");

        }
    }
}

