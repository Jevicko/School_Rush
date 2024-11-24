/*
using System.Collections; // Untuk IEnumerator
using UnityEngine;

public class CharacterTransition : MonoBehaviour
{
    public GameObject sleepingCharacter; // Karakter di kasur
    public GameObject mainCharacter; // Karakter asli pemain
    public GameObject blinkPanel; // Panel untuk efek berkedip
    public Camera mainCamera; // Kamera utama
    public Camera sleepingCamera; // Kamera melihat karakter di kasur
    public AudioSource blinkAudio; // AudioSource untuk suara berkedip

    public float blinkSpeed = 1f; // Kecepatan berkedip (lebih lambat dari sebelumnya)
    public int blinkCount = 3; // Jumlah kedipan
    public float blackScreenDelay = 2f; // Waktu panel hitam tetap muncul sebelum beralih ke kamera utama

    private void Start()
    {
        // Mulai dengan kamera tidur aktif
        mainCamera.enabled = false;
        sleepingCamera.enabled = true;

        // Pastikan audio tidak aktif di awal
        if (blinkAudio != null)
        {
            blinkAudio.Stop();
        }

        // Mulai efek berkedip
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        // Referensi ke panel
        var panelImage = blinkPanel.GetComponent<UnityEngine.UI.Image>();

        for (int i = 0; i < blinkCount; i++)
        {
            // Fade In (Hitam penuh)
            yield return StartCoroutine(FadeAlpha(panelImage, 1f, blinkSpeed, false)); // Audio berhenti saat hitam
            // Fade Out (Transparan)
            yield return StartCoroutine(FadeAlpha(panelImage, 0f, blinkSpeed, true)); // Audio aktif saat transparan
        }

        // Pastikan panel tetap hitam setelah berkedip untuk delay
        yield return new WaitForSeconds(blackScreenDelay);

        // Pastikan panel tetap hitam setelah delay
        yield return StartCoroutine(FadeAlpha(panelImage, 1f, 0f, false)); // Tetap hitam tanpa perubahan alpha

        // Pindah ke kamera utama setelah delay
        sleepingCamera.enabled = false;
        mainCamera.enabled = true;

        // Ubah karakter
        sleepingCharacter.SetActive(false);
        mainCharacter.SetActive(true);

        // Nonaktifkan panel setelah transisi
        blinkPanel.SetActive(false);

        // Pastikan audio dihentikan setelah perpindahan ke kamera utama
        if (blinkAudio != null && blinkAudio.isPlaying)
        {
            blinkAudio.Stop();
        }
    }

    private IEnumerator FadeAlpha(UnityEngine.UI.Image panelImage, float targetAlpha, float duration, bool playAudio)
    {
        float startAlpha = panelImage.color.a;
        float time = 0;

        // Memutar audio saat panel menjadi transparan
        if (playAudio && blinkAudio != null && !blinkAudio.isPlaying)
        {
            blinkAudio.Play();
        }
        else if (!playAudio && blinkAudio != null && blinkAudio.isPlaying)
        {
            blinkAudio.Stop();
        }

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);
            yield return null;
        }

        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, targetAlpha);
    }
}*/
/*
using System.Collections;
using UnityEngine;

public class CharacterTransition : MonoBehaviour
{
    public GameObject sleepingCharacter; // Karakter di kasur
    public GameObject mainCharacter; // Karakter asli pemain
    public GameObject blinkPanel; // Panel untuk efek berkedip
    public Camera mainCamera; // Kamera utama
    public Camera sleepingCamera; // Kamera melihat karakter di kasur
    public AudioSource blinkAudio; // AudioSource untuk suara berkedip
    public BoxCollider bedCollider; // BoxCollider dari kasur
    public Vector3 sleepingOffset; // Offset posisi karakter tidur relatif terhadap kasur
    public Vector3 sleepingRotation; // Rotasi karakter tidur (euler angles)

    public float blinkSpeed = 1f; // Kecepatan berkedip (lebih lambat dari sebelumnya)
    public int blinkCount = 3; // Jumlah kedipan
    public float blackScreenDelay = 2f; // Waktu panel hitam tetap muncul sebelum beralih ke kamera utama

    private void Start()
    {
        // Mulai dengan kamera tidur aktif
        mainCamera.enabled = false;
        sleepingCamera.enabled = true;

        // Pastikan audio tidak aktif di awal
        if (blinkAudio != null)
        {
            blinkAudio.Stop();
        }

        // Mulai efek berkedip
        StartCoroutine(BlinkRoutine());
    }

    private void Update()
    {
        // Pastikan posisi karakter tidur selalu diatur
        if (sleepingCharacter != null && bedCollider != null)
        {
            // Dapatkan posisi pusat dari BoxCollider
            Vector3 bedCenter = bedCollider.bounds.center;

            // Terapkan offset untuk menentukan posisi karakter
            Vector3 adjustedPosition = bedCenter + sleepingOffset;

            // Set posisi dan rotasi karakter tidur
            sleepingCharacter.transform.position = adjustedPosition;
            sleepingCharacter.transform.rotation = Quaternion.Euler(sleepingRotation);
        }
    }

    private IEnumerator BlinkRoutine()
    {
        // Referensi ke panel
        var panelImage = blinkPanel.GetComponent<UnityEngine.UI.Image>();

        for (int i = 0; i < blinkCount; i++)
        {
            // Fade In (Hitam penuh)
            yield return StartCoroutine(FadeAlpha(panelImage, 1f, blinkSpeed, false)); // Audio berhenti saat hitam
            // Fade Out (Transparan)
            yield return StartCoroutine(FadeAlpha(panelImage, 0f, blinkSpeed, true)); // Audio aktif saat transparan
        }

        // Pastikan panel tetap hitam setelah berkedip untuk delay
        yield return new WaitForSeconds(blackScreenDelay);

        // Pastikan panel tetap hitam setelah delay
        yield return StartCoroutine(FadeAlpha(panelImage, 1f, 0f, false)); // Tetap hitam tanpa perubahan alpha

        // Pindah ke kamera utama setelah delay
        sleepingCamera.enabled = false;
        mainCamera.enabled = true;

        // Ubah karakter
        sleepingCharacter.SetActive(false);
        mainCharacter.SetActive(true);

        // Nonaktifkan panel setelah transisi
        blinkPanel.SetActive(false);

        // Pastikan audio dihentikan setelah perpindahan ke kamera utama
        if (blinkAudio != null && blinkAudio.isPlaying)
        {
            blinkAudio.Stop();
        }
    }

    private IEnumerator FadeAlpha(UnityEngine.UI.Image panelImage, float targetAlpha, float duration, bool playAudio)
    {
        float startAlpha = panelImage.color.a;
        float time = 0;

        // Memutar audio saat panel menjadi transparan
        if (playAudio && blinkAudio != null && !blinkAudio.isPlaying)
        {
            blinkAudio.Play();
        }
        else if (!playAudio && blinkAudio != null && blinkAudio.isPlaying)
        {
            blinkAudio.Stop();
        }

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);
            yield return null;
        }

        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, targetAlpha);
    }
}*/
/*
using System.Collections;
using UnityEngine;

public class CharacterTransition : MonoBehaviour
{
    public GameObject sleepingCharacter; // Karakter di kasur
    public GameObject mainCharacter; // Karakter asli pemain
    public GameObject blinkPanel; // Panel untuk efek berkedip
    public GameObject prologPanel; // Panel hitam untuk prolog
    public UnityEngine.UI.Text prologText; // Teks untuk prolog
    public Camera mainCamera; // Kamera utama
    public Camera sleepingCamera; // Kamera melihat karakter di kasur
    public AudioSource blinkAudio; // AudioSource untuk suara berkedip
    public BoxCollider bedCollider; // BoxCollider dari kasur
    public Vector3 sleepingOffset; // Offset posisi karakter tidur relatif terhadap kasur
    public Vector3 sleepingRotation; // Rotasi karakter tidur (euler angles)

    public float prologDuration = 3f; // Durasi prolog sebelum berkedip
    public float blinkSpeed = 1f; // Kecepatan berkedip
    public int blinkCount = 3; // Jumlah kedipan
    public float blackScreenDelay = 2f; // Waktu panel hitam tetap muncul sebelum beralih ke kamera utama

    private void Start()
    {
        // Mulai dengan kamera tidur aktif
        mainCamera.enabled = false;
        sleepingCamera.enabled = true;

        // Pastikan audio tidak aktif di awal
        if (blinkAudio != null)
        {
            blinkAudio.Stop();
        }

        // Mulai dengan menampilkan prolog
        StartCoroutine(PrologRoutine());
    }

    private void Update()
    {
        // Pastikan posisi karakter tidur selalu diatur
        if (sleepingCharacter != null && bedCollider != null)
        {
            // Dapatkan posisi pusat dari BoxCollider
            Vector3 bedCenter = bedCollider.bounds.center;

            // Terapkan offset untuk menentukan posisi karakter
            Vector3 adjustedPosition = bedCenter + sleepingOffset;

            // Set posisi dan rotasi karakter tidur
            sleepingCharacter.transform.position = adjustedPosition;
            sleepingCharacter.transform.rotation = Quaternion.Euler(sleepingRotation);
        }
    }

    private IEnumerator PrologRoutine()
    {
        // Tampilkan panel prolog
        prologPanel.SetActive(true);

        // Set teks prolog (dapat diubah sesuai kebutuhan)
        prologText.text = "Once upon a time, in a deep sleep...";

        // Tunggu selama durasi prolog
        yield return new WaitForSeconds(prologDuration);

        // Sembunyikan panel prolog
        prologPanel.SetActive(false);

        // Mulai efek berkedip
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        // Referensi ke panel
        var panelImage = blinkPanel.GetComponent<UnityEngine.UI.Image>();

        for (int i = 0; i < blinkCount; i++)
        {
            // Fade In (Hitam penuh)
            yield return StartCoroutine(FadeAlpha(panelImage, 1f, blinkSpeed, false)); // Audio berhenti saat hitam
            // Fade Out (Transparan)
            yield return StartCoroutine(FadeAlpha(panelImage, 0f, blinkSpeed, true)); // Audio aktif saat transparan
        }

        // Pastikan panel tetap hitam setelah berkedip untuk delay
        yield return new WaitForSeconds(blackScreenDelay);

        // Pastikan panel tetap hitam setelah delay
        yield return StartCoroutine(FadeAlpha(panelImage, 1f, 0f, false)); // Tetap hitam tanpa perubahan alpha

        // Pindah ke kamera utama setelah delay
        sleepingCamera.enabled = false;
        mainCamera.enabled = true;

        // Ubah karakter
        sleepingCharacter.SetActive(false);
        mainCharacter.SetActive(true);

        // Nonaktifkan panel setelah transisi
        blinkPanel.SetActive(false);

        // Pastikan audio dihentikan setelah perpindahan ke kamera utama
        if (blinkAudio != null && blinkAudio.isPlaying)
        {
            blinkAudio.Stop();
        }
    }

    private IEnumerator FadeAlpha(UnityEngine.UI.Image panelImage, float targetAlpha, float duration, bool playAudio)
    {
        float startAlpha = panelImage.color.a;
        float time = 0;

        // Memutar audio saat panel menjadi transparan
        if (playAudio && blinkAudio != null && !blinkAudio.isPlaying)
        {
            blinkAudio.Play();
        }
        else if (!playAudio && blinkAudio != null && blinkAudio.isPlaying)
        {
            blinkAudio.Stop();
        }

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);
            yield return null;
        }

        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, targetAlpha);
    }
}*/
/*
using System.Collections;
using UnityEngine;

public class CharacterTransition : MonoBehaviour
{
    public GameObject sleepingCharacter; // Karakter di kasur
    public GameObject mainCharacter; // Karakter asli pemain
    public GameObject blinkPanel; // Panel untuk efek berkedip
    public GameObject prologPanel; // Panel hitam untuk prolog
    public UnityEngine.UI.Text prologText; // Teks untuk prolog
    public Camera mainCamera; // Kamera utama
    public Camera sleepingCamera; // Kamera melihat karakter di kasur
    public AudioSource blinkAudio; // AudioSource untuk suara berkedip
    public AudioSource typingAudio; // AudioSource untuk suara ketikan
    public BoxCollider bedCollider; // BoxCollider dari kasur
    public Vector3 sleepingOffset; // Offset posisi karakter tidur relatif terhadap kasur
    public Vector3 sleepingRotation; // Rotasi karakter tidur (euler angles)

    public float prologDuration = 3f; // Durasi prolog setelah mengetik selesai
    public float typingSpeed = 0.05f; // Kecepatan mengetik (waktu jeda antara karakter)
    public float blinkSpeed = 1f; // Kecepatan berkedip
    public int blinkCount = 3; // Jumlah kedipan
    public float blackScreenDelay = 2f; // Waktu panel hitam tetap muncul sebelum beralih ke kamera utama

    private void Start()
    {
        // Mulai dengan kamera tidur aktif
        mainCamera.enabled = false;
        sleepingCamera.enabled = true;

        // Pastikan audio tidak aktif di awal
        if (blinkAudio != null)
        {
            blinkAudio.Stop();
        }

        // Mulai dengan menampilkan prolog
        StartCoroutine(PrologRoutine());
    }

    private void Update()
    {
        // Pastikan posisi karakter tidur selalu diatur
        if (sleepingCharacter != null && bedCollider != null)
        {
            // Dapatkan posisi pusat dari BoxCollider
            Vector3 bedCenter = bedCollider.bounds.center;

            // Terapkan offset untuk menentukan posisi karakter
            Vector3 adjustedPosition = bedCenter + sleepingOffset;

            // Set posisi dan rotasi karakter tidur
            sleepingCharacter.transform.position = adjustedPosition;
            sleepingCharacter.transform.rotation = Quaternion.Euler(sleepingRotation);
        }
    }

    private IEnumerator PrologRoutine()
    {
        // Tampilkan panel prolog
        prologPanel.SetActive(true);

        // Set teks prolog (ubah sesuai kebutuhan)
        string fullText = "The alarm goes off. The morning light begins to appear.The school day is here time.to wake up and get ready...";
        prologText.text = ""; // Mulai dengan teks kosong

        // Tampilkan teks dengan efek mengetik
        yield return StartCoroutine(TypeText(prologText, fullText, typingSpeed));

        // Tunggu selama durasi prolog setelah mengetik selesai
        yield return new WaitForSeconds(prologDuration);

        // Sembunyikan panel prolog
        prologPanel.SetActive(false);

        // Mulai efek berkedip
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator TypeText(UnityEngine.UI.Text textComponent, string textToType, float typingSpeed)
    {
        for (int i = 0; i < textToType.Length; i++)
        {
            textComponent.text += textToType[i]; // Tambahkan satu karakter ke teks

            // Mainkan suara ketikan
            if (typingAudio != null)
            {
                typingAudio.Play();
            }

            yield return new WaitForSeconds(typingSpeed); // Tunggu sebelum menambahkan karakter berikutnya
        }
    }

    private IEnumerator BlinkRoutine()
    {
        // Referensi ke panel
        var panelImage = blinkPanel.GetComponent<UnityEngine.UI.Image>();

        for (int i = 0; i < blinkCount; i++)
        {
            // Fade In (Hitam penuh)
            yield return StartCoroutine(FadeAlpha(panelImage, 1f, blinkSpeed, false)); // Audio berhenti saat hitam
            // Fade Out (Transparan)
            yield return StartCoroutine(FadeAlpha(panelImage, 0f, blinkSpeed, true)); // Audio aktif saat transparan
        }

        // Pastikan panel tetap hitam setelah berkedip untuk delay
        yield return new WaitForSeconds(blackScreenDelay);

        // Pastikan panel tetap hitam setelah delay
        yield return StartCoroutine(FadeAlpha(panelImage, 1f, 0f, false)); // Tetap hitam tanpa perubahan alpha

        // Pindah ke kamera utama setelah delay
        sleepingCamera.enabled = false;
        mainCamera.enabled = true;

        // Ubah karakter
        sleepingCharacter.SetActive(false);
        mainCharacter.SetActive(true);

        // Nonaktifkan panel setelah transisi
        blinkPanel.SetActive(false);

        // Pastikan audio dihentikan setelah perpindahan ke kamera utama
        if (blinkAudio != null && blinkAudio.isPlaying)
        {
            blinkAudio.Stop();
        }
    }

    private IEnumerator FadeAlpha(UnityEngine.UI.Image panelImage, float targetAlpha, float duration, bool playAudio)
    {
        float startAlpha = panelImage.color.a;
        float time = 0;

        // Memutar audio saat panel menjadi transparan
        if (playAudio && blinkAudio != null && !blinkAudio.isPlaying)
        {
            blinkAudio.Play();
        }
        else if (!playAudio && blinkAudio != null && blinkAudio.isPlaying)
        {
            blinkAudio.Stop();
        }

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);
            yield return null;
        }

        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, targetAlpha);
    }
}*/

using System.Collections;
using UnityEngine;

public class CharacterTransition : MonoBehaviour
{
    public GameObject sleepingCharacter; // Karakter di kasur
    public GameObject mainCharacter; // Karakter asli pemain
    public GameObject blinkPanel; // Panel untuk efek berkedip
    public GameObject prologPanel; // Panel hitam untuk prolog
    public UnityEngine.UI.Text prologText; // Teks untuk prolog
    public Camera mainCamera; // Kamera utama
    public Camera sleepingCamera; // Kamera melihat karakter di kasur
    public AudioSource blinkAudio; // AudioSource untuk suara berkedip
    public AudioSource typingAudio; // AudioSource untuk suara ketikan
    public BoxCollider bedCollider; // BoxCollider dari kasur
    public Vector3 sleepingOffset; // Offset posisi karakter tidur relatif terhadap kasur
    public Vector3 sleepingRotation; // Rotasi karakter tidur (euler angles)

    public float prologDuration = 3f; // Durasi prolog setelah mengetik selesai
    public float typingSpeed = 0.05f; // Kecepatan mengetik (waktu jeda antara karakter)
    public float blinkSpeed = 1f; // Kecepatan berkedip
    public int blinkCount = 3; // Jumlah kedipan
    public float blackScreenDelay = 2f; // Waktu panel hitam tetap muncul sebelum beralih ke kamera utama

    private void Start()
    {
        // Mulai dengan kamera tidur aktif
        mainCamera.enabled = false;
        sleepingCamera.enabled = true;

        // Pastikan audio tidak aktif di awal
        if (blinkAudio != null)
        {
            blinkAudio.Stop();
        }

        // Mulai dengan menampilkan prolog
        StartCoroutine(PrologRoutine());
    }

    private void Update()
    {
        // Pastikan posisi karakter tidur selalu diatur
        if (sleepingCharacter != null && bedCollider != null)
        {
            // Dapatkan posisi pusat dari BoxCollider
            Vector3 bedCenter = bedCollider.bounds.center;

            // Terapkan offset untuk menentukan posisi karakter
            Vector3 adjustedPosition = bedCenter + sleepingOffset;

            // Set posisi dan rotasi karakter tidur
            sleepingCharacter.transform.position = adjustedPosition;
            sleepingCharacter.transform.rotation = Quaternion.Euler(sleepingRotation);
        }
    }

    private IEnumerator PrologRoutine()
    {
        // Tampilkan panel prolog
        prologPanel.SetActive(true);

        // Set teks prolog (ubah sesuai kebutuhan)
        string fullText = "The alarm goes off. The morning light begins to appear.The school day is here time.to wake up and get ready...";
        prologText.text = ""; // Mulai dengan teks kosong

        // Tampilkan teks dengan efek mengetik
        yield return StartCoroutine(TypeText(prologText, fullText, typingSpeed));

        // Tunggu selama durasi prolog setelah mengetik selesai
        yield return new WaitForSeconds(prologDuration);

        // Sembunyikan panel prolog
        prologPanel.SetActive(false);

        // Mulai efek berkedip
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator TypeText(UnityEngine.UI.Text textComponent, string textToType, float typingSpeed)
    {
        for (int i = 0; i < textToType.Length; i++)
        {
            textComponent.text += textToType[i]; // Tambahkan satu karakter ke teks

            // Mainkan suara ketikan jika belum selesai
            if (typingAudio != null && !typingAudio.isPlaying)
            {
                typingAudio.Play();
            }

            yield return new WaitForSeconds(typingSpeed); // Tunggu sebelum menambahkan karakter berikutnya
        }

        // Hentikan suara ketikan setelah teks selesai
        if (typingAudio != null && typingAudio.isPlaying)
        {
            typingAudio.Stop();
        }
    }

    private IEnumerator BlinkRoutine()
    {
        // Referensi ke panel
        var panelImage = blinkPanel.GetComponent<UnityEngine.UI.Image>();

        for (int i = 0; i < blinkCount; i++)
        {
            // Fade In (Hitam penuh)
            yield return StartCoroutine(FadeAlpha(panelImage, 1f, blinkSpeed, false)); // Audio berhenti saat hitam
            // Fade Out (Transparan)
            yield return StartCoroutine(FadeAlpha(panelImage, 0f, blinkSpeed, true)); // Audio aktif saat transparan
        }

        // Pastikan panel tetap hitam setelah berkedip untuk delay
        yield return new WaitForSeconds(blackScreenDelay);

        // Pastikan panel tetap hitam setelah delay
        yield return StartCoroutine(FadeAlpha(panelImage, 1f, 0f, false)); // Tetap hitam tanpa perubahan alpha

        // Pindah ke kamera utama setelah delay
        sleepingCamera.enabled = false;
        mainCamera.enabled = true;

        // Ubah karakter
        sleepingCharacter.SetActive(false);
        mainCharacter.SetActive(true);

        // Nonaktifkan panel setelah transisi
        blinkPanel.SetActive(false);

        // Pastikan audio dihentikan setelah perpindahan ke kamera utama
        if (blinkAudio != null && blinkAudio.isPlaying)
        {
            blinkAudio.Stop();
        }
    }

    private IEnumerator FadeAlpha(UnityEngine.UI.Image panelImage, float targetAlpha, float duration, bool playAudio)
    {
        float startAlpha = panelImage.color.a;
        float time = 0;

        // Memutar audio saat panel menjadi transparan
        if (playAudio && blinkAudio != null && !blinkAudio.isPlaying)
        {
            blinkAudio.Play();
        }
        else if (!playAudio && blinkAudio != null && blinkAudio.isPlaying)
        {
            blinkAudio.Stop();
        }

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);
            yield return null;
        }

        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, targetAlpha);
    }
}
