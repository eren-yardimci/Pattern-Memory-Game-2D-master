using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// MenuButton sýnýfý, menü butonlarýnýn renklerini dinamik olarak deðiþtirmeyi saðlar.
public class MenuButton : MonoBehaviour
{
    // Menü butonlarýný içeren dizi
    public Button[] menuButtons;
    // Gökkuþaðý renklerini içeren dizi
    private Color[] rainbowColors = { Color.red, Color.magenta, Color.blue, Color.cyan, Color.green, Color.yellow };
    // Geçerli buton indeksi
    private int currentButtonIndex = 0;

    // Baþlangýçta çaðrýlan metod
    void Start()
    {
        // Buton renklerini deðiþtiren coroutine'i baþlat
        StartCoroutine(ChangeButtonColors());
    }

    // Buton renklerini deðiþtiren coroutine
    IEnumerator ChangeButtonColors()
    {
        while (true)
        {
            // Geçerli butonun orijinal renklerini al
            ColorBlock originalColors = menuButtons[currentButtonIndex].colors;
            ColorBlock newColors = originalColors;

            // Yeni renkleri ayarla (gökkuþaðý renklerinden)
            newColors.normalColor = new Color(rainbowColors[currentButtonIndex % rainbowColors.Length].r, rainbowColors[currentButtonIndex % rainbowColors.Length].g, rainbowColors[currentButtonIndex % rainbowColors.Length].b, 0.8f);
            menuButtons[currentButtonIndex].colors = newColors;

            // 1 saniye bekle
            yield return new WaitForSeconds(1);

            // Butonun renklerini orijinal haline döndür
            menuButtons[currentButtonIndex].colors = originalColors;
            // Sonraki butona geç
            currentButtonIndex = (currentButtonIndex + 1) % menuButtons.Length;
        }
    }
}
