using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// GameManager sınıfı, oyunun genel yönetimini sağlar.
public class GameManager : MonoBehaviour
{
    // Oyun butonlarını içeren dizi
    public Button[] buttons;
    // Oyunun başlangıcındaki gecikme süresi
    public float startDelay = 1f;
    // Butonun vurgulanma süresi
    public float highlightDuration = 0.5f;
    // Kazanma sayacı
    private int winCount = 0;
    // Butonların sırasını tutan liste
    private List<int> sequence = new List<int>();

    // Oyun bitiş UI'si
    public GameObject gameOverUI;

    // Bir sonraki seviyenin adı
    public string nextLevelName;
    // Skor
    public int Score;

    // Butonların üzerine gelindiğinde kullanılacak renk
    public Color hoverColor;

    // Oyunun başlangıcında çağrılan metod
    void Start()
    {
        StartCoroutine(StartGame());
        // PlayerPrefs'ten Score'u alın
        Score = PlayerPrefs.GetInt("Score", 0);
    }

    // Her karede çağrılan Update metodu
    void Update()
    {
        // Escape tuşuna basıldığında oyunu bitir
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver();
        }
    }

    // Oyunu başlatan coroutine
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(startDelay);

        // Kazanma sayısına göre buton sayısını belirle
        int buttonCount = Mathf.Min(winCount + 1, buttons.Length - 1);

        // Butonları rastgele seç ve sıraya ekle
        for (int i = 0; i < buttonCount; i++)
        {
            int randomButtonIndex = Random.Range(0, buttons.Length);
            sequence.Add(randomButtonIndex);
            ButtonController buttonController = buttons[randomButtonIndex].GetComponent<ButtonController>();
            Debug.Log("Buton" + randomButtonIndex + " aktif");
            buttonController.isActive = true;

            StartCoroutine(HighlightButton(buttons[randomButtonIndex]));
            yield return new WaitForSeconds(highlightDuration + 0.3f);
        }
    }

    // Butonu vurgulayan coroutine
    IEnumerator HighlightButton(Button button)
    {
        Color originalColor = button.image.color;
        Color highlightColor = Color.green;

        button.image.color = highlightColor;

        yield return new WaitForSeconds(highlightDuration);

        button.image.color = originalColor;
    }

    // Oyunu kazandığında çağrılan metod
    public void GameWon()
    {
        winCount++;
        Score++;
        PlayerPrefs.SetInt("Score", Score);
        if (winCount >= buttons.Length - 1)
        {
            SceneManager.LoadScene(nextLevelName); // Bir sonraki seviyeye geç
        }
        else
        {
            StartCoroutine(StartGame());
        }
    }

    // Oyunu kaybettiğinde çağrılan metod
    public void GameOver()
    {
        gameOverUI.SetActive(true); // Oyun bitiş UI'sini göster
        //SceneManager.LoadScene("Level1"); // Yoruma alınmış, istenirse aktif hale getirilebilir
    }

    // Buton sırasını kontrol eden metod
    public void CheckSequence(int buttonIndex)
    {
        if (sequence.Count == 0 || sequence[0] != buttonIndex)
        {
            GameOver();
            return;
        }

        sequence.RemoveAt(0);
        if (sequence.Count == 0)
        {
            GameWon(); // Sıra doğruysa oyunu kazan
        }
    }

    // Oyunu yeniden başlatan metod
    public void Retry()
    {
        Score = 0;
        PlayerPrefs.SetInt("Score", Score);
        SceneManager.LoadScene("Level1");
    }

    // Ana menüye dönen metod
    public void Menu()
    {
        Score = 0;
        PlayerPrefs.SetInt("Score", Score);
        SceneManager.LoadScene("MainMenu");
    }
}
