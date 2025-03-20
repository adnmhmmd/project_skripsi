using UnityEngine;
using UnityEngine.UI;

public class VegetationRecommendation : MonoBehaviour
{
    // Referensi untuk slider-slider di UI
    public Slider slider1;
    public Slider slider2;
    public Slider slider3;
    public Slider slider4;
    public Slider slider5;

    // Referensi untuk menampilkan hasil total
    public Text totalText;
    public Text recommendationText;

    // Bobot kriteria
    private float[] weights = { 0.456666667f, 0.266666667f, 0.156666667f, 0.09f, 0.04f };

    // Matriks Keputusan (Alternatif vegetasi dan kriteria Penurunan Suhu, Laju Evapotranspirasi, Kepadatan Daun, Tingkat Perawatan, Estetika)
    private float[,] alternatives = new float[8, 5] {
        { 4, 3, 5, 3, 4 }, // Switenia macrophylla
        { 3, 3, 4, 4, 5 }, // Tabebuia rosea
        { 5, 4, 5, 3, 5 }, // Samanea saman
        { 3, 3, 4, 3, 4 }, // Filicium decipiens
        { 2, 5, 2, 4, 2 }, // Dalbergia oliveri
        { 3, 3, 4, 2, 4 }, // Pterocarpus indicus
        { 3, 2, 2, 3, 4 }, // Tillia
        { 2, 2, 1, 4, 2 }  // Corylus
    };

    private string[] alternativeNames = {
        "Switenia macrophylla", "Tabebuia rosea", "Samanea saman", "Filicium decipiens",
        "Dalbergia oliveri", "Pterocarpus indicus", "Tillia", "Corylus"
    };

    private float value1, value2, value3, value4, value5;

    // Start is called before the first frame update
    void Start()
    {
        // Inisialisasi nilai slider
        slider1.onValueChanged.AddListener(OnSliderChange);
        slider2.onValueChanged.AddListener(OnSliderChange);
        slider3.onValueChanged.AddListener(OnSliderChange);
        slider4.onValueChanged.AddListener(OnSliderChange);
        slider5.onValueChanged.AddListener(OnSliderChange);

        // Update nilai slider awal dan total
        UpdateSliderValues();
    }

    // Update is called once per frame
    void Update()
    {
        // Kalkulasi nilai total
        CalculateTotal();
    }

    // Fungsi yang dipanggil ketika nilai slider berubah
    void OnSliderChange(float value)
    {
        // Update nilai slider masing-masing
        UpdateSliderValues();
    }

    // Fungsi untuk memperbarui nilai slider
    void UpdateSliderValues()
    {
        value1 = slider1.value;
        value2 = slider2.value;
        value3 = slider3.value;
        value4 = slider4.value;
        value5 = slider5.value;

        // Perbarui alternatif dengan nilai slider
        alternatives[0, 0] = value1;
        alternatives[0, 1] = value2;
        alternatives[0, 2] = value3;
        alternatives[0, 3] = value4;
        alternatives[0, 4] = value5;

        // Perbarui total setelah slider berubah
        CalculateTotal();
    }

    // Fungsi untuk menghitung nilai total dan mitigasi menggunakan metode VIKOR
    void CalculateTotal()
    {
        // Normalisasi Matriks Keputusan
        float[,] normalized = new float[8, 5];
        for (int j = 0; j < 5; j++)
        {
            float max = Mathf.NegativeInfinity;
            float min = Mathf.Infinity;

            // Menemukan nilai max dan min untuk setiap kriteria (C1, C2, C3, C4, C5)
            for (int i = 0; i < 8; i++)
            {
                if (alternatives[i, j] > max) max = alternatives[i, j];
                if (alternatives[i, j] < min) min = alternatives[i, j];
            }

            // Normalisasi Matriks Keputusan (Benefit or Cost)
            for (int i = 0; i < 8; i++)
            {
                normalized[i, j] = (alternatives[i, j] - min) / (max - min);  // Benefit criteria (max for benefit, min for cost)
            }
        }

        // Menghitung nilai Utility (S) dan Regret (R) untuk setiap alternatif
        float[] sValues = new float[8];
        float[] rValues = new float[8];
        for (int i = 0; i < 8; i++)
        {
            float sSum = 0;
            float rSum = 0;
            for (int j = 0; j < 5; j++)
            {
                sSum += weights[j] * normalized[i, j];
                rSum += weights[j] * (1 - normalized[i, j]);  // Regret is opposite of utility
            }
            sValues[i] = sSum;
            rValues[i] = rSum;
        }

        // Menghitung nilai Q untuk setiap alternatif
        float[] qValues = new float[8];
        float v = 0.5f; // Bobot untuk kompromi antara S dan R
        float minS = Mathf.Min(sValues);
        float maxS = Mathf.Max(sValues);
        float minR = Mathf.Min(rValues);
        float maxR = Mathf.Max(rValues);

        for (int i = 0; i < 8; i++)
        {
            qValues[i] = v * ((sValues[i] - minS) / (maxS - minS)) + (1 - v) * ((rValues[i] - minR) / (maxR - minR));
        }

        // Menentukan alternatif terbaik berdasarkan nilai Q
        int bestIndex = 0;
        float minQ = qValues[0];
        for (int i = 1; i < 8; i++)
        {
            if (qValues[i] < minQ)
            {
                minQ = qValues[i];
                bestIndex = i;
            }
        }

        // Menampilkan rekomendasi di UI Text
        recommendationText.text = "Rekomendasi: " + alternativeNames[bestIndex];
    }
}
