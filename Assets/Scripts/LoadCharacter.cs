using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Array cu caractere de tip prefab cu care vom lucra
    public Transform spawnPoint;          // Spawnpoint pentru caracter
    public TMP_Text label;                // Optional - Daca vom da nume in viitor jucatorilor

    void Start()
    {
        // Luam indexul caracterului salvat din PlayerPrefs, daca nu exista, default la 0
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter", 0);
        Debug.Log("Loaded character id " + selectedCharacter);

        // Ne asiguram ca indexul selectat este in limite
        if (selectedCharacter >= 0 && selectedCharacter < characterPrefabs.Length)
        {
            // Instantiem caracterul selectat la spawnpoint
            GameObject prefab = characterPrefabs[selectedCharacter];
            GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            clone.transform.localScale = Vector3.one; // Reset scale to (1, 1, 1)

            // Fortam GameObject root sa fie activ
            clone.SetActive(true);

            // Ne asiguram ca toate componentele Renderer sunt activate
            EnableRenderers(clone);

            // Optional - Setam numele caracterului in interfata (daca exista)
            if (label != null)
            {
                label.text = prefab.name;
            }

            // Assignam LifeObjects la caracter
            AssignLifeObjects(clone);
        }
        else
        {
            Debug.LogWarning("Selected character index out of bounds.");
        }
    }

    void EnableRenderers(GameObject character)
    {
        // Activeaza toate componentele MeshRenderer si SkinnedMeshRenderer
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

        Image life1 = GameObject.Find("life1").GetComponent<Image>();
        Image life2 = GameObject.Find("life2").GetComponent<Image>();
        Image life3 = GameObject.Find("life3").GetComponent<Image>();

        // Verificam daca componentele Image au fost gasite
        if (life1 != null && life2 != null && life3 != null)
        {
            var characterScript = character.GetComponent<CollisionWithObstacles>();

            if (characterScript != null)
            {
                // Asignam iconitele de viata
                characterScript.lifeIcons = new Image[] { life1, life2, life3 };
                Debug.Log("LifeIcons assigned to character.");
            }
            else
            {
                Debug.LogWarning("CharacterScript not found on the instantiated player.");
            }
        }
        else
        {
            Debug.LogError("Life images not found in the scene. Please ensure life1, life2, and life3 are present.");
        }
    }

}
