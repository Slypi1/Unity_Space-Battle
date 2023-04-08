using UnityEngine;
using UnityEngine.UI;

public class SlotShip : MonoBehaviour
{
    public void AddSprite(Sprite sprite)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Image>().sprite = sprite;
    }
}
