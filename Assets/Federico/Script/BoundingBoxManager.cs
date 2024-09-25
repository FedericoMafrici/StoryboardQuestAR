using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.OpenXR.Features.Meta;
using System.Collections.Generic;

public class BoundingBoxManager : MonoBehaviour
{
    // Material da assegnare alle bounding box
    public Material boundingBoxMaterial;

    // Lista delle bounding box rilevate
    private List<ARBoundingBox> boundingBoxes = new List<ARBoundingBox>();
    [SerializeField] private ARBoundingBoxManager bbManager; // Databa
    void Start()
    {
    
            if (LoaderUtility
                    .GetActiveLoader()?
                    .GetLoadedSubsystem<XRBoundingBoxSubsystem>() != null)
            {
                // XRBoundingBoxSubsystem was loaded. The platform supports bounding box detection.
            }
            else
            {
                Debug.LogError("il meta quest non supporta la detection delle bounding box molto strano");
            }
        
        
        // Registrazione dell'evento di rilevamento delle bounding box
      //  ARBoundingBoxManager.boundingBoxDetected += OnBoundingBoxDetected;
      if (bbManager == null)
      {
          Debug.LogError("cannot find bounding box manager ");
          
      }
      bbManager.trackablesChanged.AddListener(OnTrackablesChanged);
    }

    void OnDestroy()
    {
        // Rimozione dell'evento
    //    ARBoundingBoxManager.boundingBoxDetected -= OnBoundingBoxDetected;
    }

    // Funzione chiamata quando viene rilevata una bounding box
    private void OnBoundingBoxDetected(ARBoundingBox box)
    {
        boundingBoxes.Add(box);
        ApplyMaterialToBoundingBox(box);
    }

    // Applica il materiale alla bounding box
    private void ApplyMaterialToBoundingBox(ARBoundingBox box)
    {
        var renderer = box.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = boundingBoxMaterial;
        }
    }
    public void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARBoundingBox> changes)
    {
        foreach (var boundingBox in changes.added)
        {
        OnBoundingBoxDetected(boundingBox);
        }

        foreach (var boundingBox in changes.updated)
        {
            // handle updated bounding boxes
        }

        foreach (var boundingBox in changes.removed)
        {
            // handle removed bounding boxes
        }
    }
    void Update()
    {
        // Opzionale: aggiornamenti e logiche aggiuntive
    }
}
