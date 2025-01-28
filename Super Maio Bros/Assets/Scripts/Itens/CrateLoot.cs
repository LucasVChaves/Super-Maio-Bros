using UnityEngine;

public class CrateLoot : MonoBehaviour {
    public int chances = 5;
    public int diamondBias = 1;
    public int bombBias = 2;
    public GameObject diamondObj;
    public GameObject bombObj;

    public void rollLoot() {
        int roll = Random.Range(0, chances);
        Debug.Log("Roll: " + roll);

        if (roll < diamondBias) {
            Instantiate(diamondObj, transform.position, Quaternion.identity);
        } else if (roll < bombBias) {
            Instantiate(bombObj, transform.position, Quaternion.identity);
        } else {
            Debug.Log("Dropped nothing");
        }
    }
}
