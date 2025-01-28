using UnityEngine;

public class BombController : MonoBehaviour {
    public int maxBombCount = 5;
    public int currBombCount;
    public GameObject bombPrefab;
    public Grid grid;
    public AudioClip placeBombSFX;
    private AudioSource audioSource;
    void Start() {
        currBombCount = maxBombCount;
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && currBombCount > 0) {
            Debug.Log("Bomb Count = " + currBombCount);
            InstantiateBomb();
            audioSource.PlayOneShot(placeBombSFX, 1f);
        }
    }

    void InstantiateBomb() {
        Vector3Int cellPos = grid.WorldToCell(transform.position);
        Vector3 spawnPos = grid.CellToWorld(cellPos);
        Instantiate(bombPrefab, spawnPos, Quaternion.identity);
        currBombCount--;
    }
}
