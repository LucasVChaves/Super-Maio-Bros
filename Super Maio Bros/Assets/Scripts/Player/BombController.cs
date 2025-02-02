using System.Collections;
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
    private bool isRecharging = false; // 🔹 Variável para evitar múltiplas recargas ao mesmo tempo

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

        if (!isRecharging) { 
            StartCoroutine(RechargeBombs()); // 🔹 Inicia a recarga apenas se ainda não estiver em andamento
        }
    }

    void UpdateBombPreview() {
        if (bombPreview == null) return;
        if (currBombCount <= 0) return;

        Vector3Int cellPos = grid.WorldToCell(transform.position);
        Vector3 previewPos = grid.GetCellCenterWorld(cellPos);
        bombPreview.transform.position = previewPos;
    }

    // 🔹 Corrotina para recarregar todas as bombas após 7 segundos
    IEnumerator RechargeBombs() {
        isRecharging = true; // Marca que a recarga está ativa
        Debug.Log("Recarregamento iniciado. Bombas serão restauradas em 7 segundos...");
        yield return new WaitForSeconds(7f); // Aguarda 7 segundos

        currBombCount = maxBombCount; // Recarrega todas as bombas
        Debug.Log("Todas as bombas foram recarregadas!");
        isRecharging = false; // Permite um novo recarregamento no futuro
    }
}
