using UnityEngine;

public class CrateLoot : MonoBehaviour {
    public int chances = 6;
    public int diamondBias = 1;
    public int bombBias = 2;
    public int heartBias = 3;
    public GameObject diamondObj;
    public GameObject bombObj;
    public GameObject heartObj;

    public void rollLoot() {
        int roll = Random.Range(0, chances);
        Debug.Log("Roll: " + roll);

        if (roll < diamondBias) {
            Instantiate(diamondObj, transform.position, Quaternion.identity);
        } else if (roll < bombBias) {
            Instantiate(bombObj, transform.position, Quaternion.identity);
        } else if (roll < heartBias) {
            Instantiate(heartObj, transform.position, Quaternion.identity);
        } else {
            Debug.Log("Dropped nothing");
        }
    }
}
