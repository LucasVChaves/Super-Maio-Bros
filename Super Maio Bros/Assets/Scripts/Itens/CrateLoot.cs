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
            Debug.Log("Dropped diamond");
            //Instantiate(diamondObj, transform.position, transform.localRotation);
        } else if (roll < bombBias) {
            Debug.Log("Dropped bomb");
            //Instantiate(bombObj, transform.position, transform.localRotation);
        } else {
            Debug.Log("Dropped nothing");
        }
    }
}
