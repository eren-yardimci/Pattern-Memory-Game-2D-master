using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// MenuButton s�n�f�, men� butonlar�n�n renklerini dinamik olarak de�i�tirmeyi sa�lar.
public class MenuButton : MonoBehaviour
{
    // Men� butonlar�n� i�eren dizi
    public Button[] menuButtons;
    // G�kku�a�� renklerini i�eren dizi
    private Color[] rainbowColors = { Color.red, Color.magenta, Color.blue, Color.cyan, Color.green, Color.yellow };
    // Ge�erli buton indeksi
    private int currentButtonIndex = 0;

    // Ba�lang��ta �a�r�lan metod
    void Start()
    {
        // Buton renklerini de�i�tiren coroutine'i ba�lat
        StartCoroutine(ChangeButtonColors());
    }

    // Buton renklerini de�i�tiren coroutine
    IEnumerator ChangeButtonColors()
    {
        while (true)
        {
            // Ge�erli butonun orijinal renklerini al
            ColorBlock originalColors = menuButtons[currentButtonIndex].colors;
            ColorBlock newColors = originalColors;

            // Yeni renkleri ayarla (g�kku�a�� renklerinden)
            newColors.normalColor = new Color(rainbowColors[currentButtonIndex % rainbowColors.Length].r, rainbowColors[currentButtonIndex % rainbowColors.Length].g, rainbowColors[currentButtonIndex % rainbowColors.Length].b, 0.8f);
            menuButtons[currentButtonIndex].colors = newColors;

            // 1 saniye bekle
            yield return new WaitForSeconds(1);

            // Butonun renklerini orijinal haline d�nd�r
            menuButtons[currentButtonIndex].colors = originalColors;
            // Sonraki butona ge�
            currentButtonIndex = (currentButtonIndex + 1) % menuButtons.Length;
        }
    }
}
