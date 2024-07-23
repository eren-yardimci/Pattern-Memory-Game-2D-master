using UnityEngine;
using UnityEngine.UI;

// ButtonController s�n�f�, butonlar�n kontrol�n� sa�lar.
public class ButtonController : MonoBehaviour
{
    // Butonun aktif olup olmad���n� belirten de�i�ken
    public bool isActive = false;

    // �zel buton ve oyun y�neticisi referanslar�
    private Button button;
    private GameManager gameManager;

    // Ba�lang��ta �a�r�lan metod
    void Start()
    {
        // Button bile�enini al
        button = GetComponent<Button>();
        // Butona t�klama dinleyicisi ekle
        button.onClick.AddListener(OnClick);

        // GameManager bile�enini bul
        gameManager = FindObjectOfType<GameManager>();

        // Buton renklerini ayarla
        ColorBlock colors = button.colors;
        colors.highlightedColor = gameManager.hoverColor; // Vurgulama rengini ayarla
        button.colors = colors;
    }

    // Butona t�kland���nda �a�r�lan metod
    void OnClick()
    {
        // E�er buton aktifse, s�ralamay� kontrol et
        if (isActive)
        {
            gameManager.CheckSequence(transform.GetSiblingIndex());
        }
        else
        {
            gameManager.GameOver(); // Buton aktif de�ilse, oyunu bitir
        }
    }
}
