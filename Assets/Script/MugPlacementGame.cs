using UnityEngine;
using UnityEngine.Profiling;

public class MugPlacementGame : MonoBehaviour
{
    [Header("Settings")]
    public Transform mugTransform; // Transform du mug capté par Motive
    public GameObject circlePrefab; // Préfab du cercle à afficher
    public float placementThreshold = 0.1f; // Distance maximale pour considérer que le mug est "placé"
    public float validationTime = 2f; // Temps (en secondes) à rester dans la zone pour valider

    [Header("Circle Positions")]
    public Vector3[] circlePositions; // Liste des positions des cercles
    private int currentCircleIndex = 0; // Index du cercle actuel

    private GameObject currentCircle; // Référence au cercle actuel
    private bool isValidating = false; // Indique si la validation est en cours
    private Coroutine validationCoroutine; // Référence à la coroutine de validation

    // Variables pour la latence
    private float lastRealWorldUpdateTime; // Dernier temps de mise à jour (temps réel)
    private float lastVirtualUpdateTime;   // Dernier temps de mise à jour (temps Unity)
    private float latency;                 // Latence mesurée

    void Start()
    {
        if (circlePositions.Length == 0)
        {
            Debug.LogError("Aucune position de cercle définie !");
            return;
        }

        // Génère le premier cercle
        GenerateCircleAtCurrentIndex();
    }

    void Update()
    {
        // Simuler une mise à jour des données de Motive
        SimulateRealWorldUpdate();

        if (currentCircle != null && mugTransform != null)
        {
            Profiler.BeginSample("MugPlacement Update"); // Début du profilage

            // Vérifie la distance entre le mug et le cercle
            float distance = Vector3.Distance(mugTransform.position, currentCircle.transform.position);

            if (distance <= placementThreshold)
            {
                if (!isValidating)
                {
                    Debug.Log("Mug placé correctement, début de la validation !");
                    ChangeCircleColor(Color.green); // Change la couleur en vert
                    validationCoroutine = StartCoroutine(ValidatePlacement()); // Démarre la validation
                }
            }
            else
            {
                if (isValidating)
                {
                    Debug.Log("Mug hors du cercle, annulation de la validation !");
                    StopCoroutine(validationCoroutine); // Annule la validation en cours
                    isValidating = false;
                    ChangeCircleColor(Color.red); // Change la couleur en rouge
                }
            }

            Profiler.EndSample(); // Fin du profilage
        }
    }

    void SimulateRealWorldUpdate()
    {
        // Simule une mise à jour des données de Motive
        if (Input.GetKeyDown(KeyCode.Space)) // Simule un événement externe
        {
            lastRealWorldUpdateTime = Time.time; // Enregistre le temps réel (simulé)
            Debug.Log("Données Motive reçues (temps réel) : " + lastRealWorldUpdateTime);

            // Mettez à jour la position du mug en conséquence (simulé ici)
            mugTransform.position = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

            // Enregistre le temps Unity pour calculer la latence
            lastVirtualUpdateTime = Time.time;
            latency = lastVirtualUpdateTime - lastRealWorldUpdateTime;
            Debug.Log("Latence mesurée : " + latency + " secondes");
        }
    }

    void GenerateCircleAtCurrentIndex()
    {
        Vector3 circlePosition = circlePositions[currentCircleIndex];
        currentCircle = Instantiate(circlePrefab, circlePosition, Quaternion.Euler(-90f, 0f, 0f));
        ChangeCircleColor(Color.red);
        Debug.Log($"Cercle généré à : {circlePosition} avec rotation -90° autour de X");
    }

    void ChangeCircleColor(Color color)
    {
        SpriteRenderer renderer = currentCircle.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = color;
        }
    }

    System.Collections.IEnumerator ValidatePlacement()
    {
        isValidating = true;
        yield return new WaitForSeconds(validationTime);

        float distance = Vector3.Distance(mugTransform.position, currentCircle.transform.position);
        if (distance <= placementThreshold)
        {
            Debug.Log("Validation réussie, suppression du cercle.");
            Destroy(currentCircle);
            currentCircleIndex++;
            if (currentCircleIndex < circlePositions.Length)
            {
                GenerateCircleAtCurrentIndex();
            }
            else
            {
                Debug.Log("Jeu terminé !");
            }
        }
        isValidating = false;
    }
}
