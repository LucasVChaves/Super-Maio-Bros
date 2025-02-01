using UnityEngine;

public class BombController : MonoBehaviour {
    public int maxBombCount = 5;
    public int currBombCount;
    public GameObject bombPrefab;
    public GameObject bombPreviewPrefab;
    public Grid grid;
    public AudioClip placeBombSFX;
    private AudioSource audioSource;
    private GameObject bombPreview;
    void Start() {
        currBombCount = maxBombCount;
        audioSource = GetComponent<AudioSource>();
        bombPreview = Instantiate(bombPreviewPrefab);
        bombPreview.SetActive(true);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && currBombCount > 0) {
            InstantiateBomb();
            Debug.Log("Bomb Count = " + currBombCount);
            audioSource.PlayOneShot(placeBombSFX, 1f);
        }
        UpdateBombPreview();
    }

    void InstantiateBomb() {
        Vector3Int cellPos = grid.WorldToCell(transform.position);
        Vector3 spawnPos = grid.GetCellCenterWorld(cellPos);
        Instantiate(bombPrefab, spawnPos, Quaternion.identity);
        currBombCount--;
    }

    void UpdateBombPreview() {
        if (bombPreview == null) return;
        if (currBombCount <= 0) return;

        Vector3Int cellPos = grid.WorldToCell(transform.position);
        Vector3 previewPos = grid.GetCellCenterWorld(cellPos);
        bombPreview.transform.position = previewPos;
    }
}
