using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUIController : MonoBehaviour
{
    private static readonly Color DARK_COLOR = new Color32(60, 60, 60, 100);

    public List<Image> Icons;

    public void UpdateUI(int lives)
    {
        for (int index = 0; index < Icons.Count; index++)
        {
            if (index + 1 > lives)
            {
                Icons[index].color = DARK_COLOR;
            }
        }
    }
}
