using UnityEngine;
using UnityEngine.UI;

// ButtonController sýnýfý, butonlarýn kontrolünü saðlar.
public class ButtonController : MonoBehaviour
{
    // Butonun aktif olup olmadýðýný belirten deðiþken
    public bool isActive = false;

    // Özel buton ve oyun yöneticisi referanslarý
    private Button button;
    private GameManager gameManager;

    // Baþlangýçta çaðrýlan metod
    void Start()
    {
        // Button bileþenini al
        button = GetComponent<Button>();
        // Butona týklama dinleyicisi ekle
        button.onClick.AddListener(OnClick);

        // GameManager bileþenini bul
        gameManager = FindObjectOfType<GameManager>();

        // Buton renklerini ayarla
        ColorBlock colors = button.colors;
        colors.highlightedColor = gameManager.hoverColor; // Vurgulama rengini ayarla
        button.colors = colors;
    }

    // Butona týklandýðýnda çaðrýlan metod
    void OnClick()
    {
        // Eðer buton aktifse, sýralamayý kontrol et
        if (isActive)
        {
            gameManager.CheckSequence(transform.GetSiblingIndex());
        }
        else
        {
            gameManager.GameOver(); // Buton aktif deðilse, oyunu bitir
        }
    }
}
