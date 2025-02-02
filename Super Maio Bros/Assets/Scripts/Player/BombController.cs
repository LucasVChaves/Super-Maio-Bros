using System.Collections;
using UnityEngine;
using TMPro;

public class BombController : MonoBehaviour {
    public int maxBombCount = 5;
    public int currBombCount;
    public GameObject bombPrefab;
    public GameObject bombPreviewPrefab;
    public Grid grid;
    public AudioClip placeBombSFX;
    public TMP_Text uiBombCount;
    private AudioSource audioSource;
    private GameObject bombPreview;
    private bool isRecharging = false;

    void Start() {
        currBombCount = maxBombCount;
        audioSource = GetComponent<AudioSource>();
        bombPreview = Instantiate(bombPreviewPrefab);
        bombPreview.SetActive(true);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && currBombCount > 0) {
            InstantiateBomb();
            audioSource.PlayOneShot(placeBombSFX, 1f);
        }
        UpdateBombPreview();
        UpdateBombUI();
    }

    void InstantiateBomb() {
        Vector3Int cellPos = grid.WorldToCell(transform.position);
        Vector3 spawnPos = grid.GetCellCenterWorld(cellPos);
        Instantiate(bombPrefab, spawnPos, Quaternion.identity);
        currBombCount--;

        if (!isRecharging) { 
            StartCoroutine(RechargeBombs());
        }
    }

    void UpdateBombPreview() {
        if (bombPreview == null) return;
        if (currBombCount <= 0) return;

        Vector3Int cellPos = grid.WorldToCell(transform.position);
        Vector3 previewPos = grid.GetCellCenterWorld(cellPos);
        bombPreview.transform.position = previewPos;
    }

    void UpdateBombUI() {
        uiBombCount.text = currBombCount.ToString() + " I " + maxBombCount.ToString();
    }

    IEnumerator RechargeBombs() {
        isRecharging = true;
        yield return new WaitForSeconds(7f);

        currBombCount = maxBombCount;
        isRecharging = false;
    }
}
