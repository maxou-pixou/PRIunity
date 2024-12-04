using UnityEngine;

public class MugOffsetController : MonoBehaviour
{
    public GameObject mugObject; // R�f�rence � l'objet du mug
    public Vector3 offset; // Offset � appliquer

    private Vector3 initialPosition; // Position initiale du mug dans le monde r�el

    void Start()
    {
        if (mugObject == null)
        {
            Debug.LogError("L'objet Mug n'est pas assign� !");
            return;
        }

        // Enregistrer la position initiale du mug
        initialPosition = mugObject.transform.position;
    }

    void Update()
    {
        if (mugObject != null)
        {
            // Appliquer l'offset � la position initiale
            mugObject.transform.position = initialPosition + offset;
        }
    }

    // M�thode publique pour modifier l'offset via un autre script ou une interface
    public void SetOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }
}
