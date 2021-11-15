using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class HealthDrawerScipt : MonoBehaviour
{
    public GameObject heartPrefab;
    List<GameObject> hearts;
    // Start is called before the first frame update
    void Start()
    {
        hearts = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        while (hearts.Count < Health.currentHP){
            GameObject go = Instantiate<GameObject>(heartPrefab, this.transform);
            if (hearts.Count > 0){
                Vector2 pos = go.GetComponent<RectTransform>().anchoredPosition;
                float prevX = hearts[hearts.Count-1].GetComponent<RectTransform>().anchoredPosition.x;
                pos.x = prevX + 70f;
                go.GetComponent<RectTransform>().anchoredPosition = pos;
            }
            hearts.Add(go);
        }
        while (hearts.Count > Health.currentHP){
            Destroy(hearts[hearts.Count-1]);
            hearts.RemoveAt(hearts.Count-1);
        }
    }
}
