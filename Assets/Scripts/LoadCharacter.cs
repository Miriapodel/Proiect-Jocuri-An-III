using UnityEngine;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Array of character prefabs to instantiate from
    public Transform spawnPoint;          // The point where the character will be instantiated
    public TMP_Text label;                // Optional - Daca vom da nume in viitor jucatorilor

    void Start()
    {
        // Get the saved character index from PlayerPrefs, default to 0 if none exists
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter", 0);
        Debug.Log("Loaded character id " + selectedCharacter);

        // Ensure the selectedCharacter index is within bounds
        if (selectedCharacter >= 0 && selectedCharacter < characterPrefabs.Length)
        {
            // Instantiate the selected character prefab at the spawn point
            GameObject prefab = characterPrefabs[selectedCharacter];
            GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            clone.transform.localScale = Vector3.one; // Reset scale to (1, 1, 1)

            // Force the root GameObject to be active
            clone.SetActive(true);

            // Ensure all Renderer components are enabled
            EnableRenderers(clone);

            // Optional - Set the label to the character's name if label is not null
            if (label != null)
            {
                label.text = prefab.name;
            }

            // Assign Life Objects, if necessary
            AssignLifeObjects(clone);
        }
        else
        {
            Debug.LogWarning("Selected character index out of bounds.");
        }
    }

    void EnableRenderers(GameObject character)
    {
        // Enable all MeshRenderer and SkinnedMeshRenderer components
        var meshRenderers = character.GetComponentsInChildren<MeshRenderer>();
        var skinnedMeshRenderers = character.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var renderer in meshRenderers)
        {
            renderer.enabled = true;
        }

        foreach (var renderer in skinnedMeshRenderers)
        {
            renderer.enabled = true;
        }
    }

    void AssignLifeObjects(GameObject character)
    {
        // Assuming Life Objects are named "Life1", "Life2", "Life3" in the scene
        GameObject life1 = GameObject.Find("Life1");
        GameObject life2 = GameObject.Find("Life2");
        GameObject life3 = GameObject.Find("Life3");

        // Assuming the character has a component that contains the LifeObjects array
        var characterScript = character.GetComponent<CollisionWithObstacles>(); // Replace with actual script name if different
        if (characterScript != null)
        {
            characterScript.lifeObjects = new GameObject[] { life1, life2, life3 };
            Debug.Log("LifeObjects assigned to character.");
        }
        else
        {
            Debug.LogWarning("CharacterScript not found on the instantiated player.");
        }
    }
}
