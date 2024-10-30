using UnityEngine;
using UnityEngine.UI;

public class MaterialToggleController : MonoBehaviour
{
    [System.Serializable]
    public class MaterialSlot
    {
        public MeshRenderer meshRenderer;  // Assign the mesh here
        public int materialSlotIndex;      // Specify the material slot index (0 for the first material, etc.)
        public Material materialOn;        // Material to assign when toggle is ON
        public Material materialOff;       // Material to assign when toggle is OFF
    }

    public MaterialSlot[] materialSlots;   // Array of materials to switch
    public Toggle toggle;                  // Assign the Toggle component from the UI

    private void Start()
    {
        if (toggle != null)
        {
            // Set initial state based on the toggle's current value
            UpdateMaterials(toggle.isOn);
            // Subscribe to the Toggle's onValueChanged event
            toggle.onValueChanged.AddListener(UpdateMaterials);
        }
    }

    private void UpdateMaterials(bool isOn)
    {
        foreach (var slot in materialSlots)
        {
            if (slot.meshRenderer != null && slot.materialSlotIndex >= 0 && slot.materialSlotIndex < slot.meshRenderer.materials.Length)
            {
                // Get all materials from the meshRenderer
                Material[] materials = slot.meshRenderer.materials;

                // Replace the material in the specified slot
                materials[slot.materialSlotIndex] = isOn ? slot.materialOn : slot.materialOff;

                // Apply the updated materials array back to the meshRenderer
                slot.meshRenderer.materials = materials;
            }
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the Toggle's event to prevent memory leaks
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(UpdateMaterials);
        }
    }
}
