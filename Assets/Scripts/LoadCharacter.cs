using UnityEngine;
using TMPro;

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
        // Asumand ca obiectele de viata sunt numite "Life1", "Life2", "Life3" in scena
        GameObject life1 = GameObject.Find("Life1");
        GameObject life2 = GameObject.Find("Life2");
        GameObject life3 = GameObject.Find("Life3");

        // Asumand ca caracterul are un component care contine array-ul LifeObjects
        var characterScript = character.GetComponent<CollisionWithObstacles>();
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
