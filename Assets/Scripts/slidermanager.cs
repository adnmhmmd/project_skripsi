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

    // Tempat untuk menyimpan nilai slider
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

        // Perbarui total setelah slider berubah
        CalculateTotal();
    }

    // Fungsi untuk menghitung nilai total
    void CalculateTotal()
    {
        // Menghitung total nilai dari semua slider
        float total = value1 + value2 + value3 + value4 + value5;

        // Menampilkan total nilai di UI Text
        totalText.text = "Total: " + Mathf.RoundToInt(total).ToString();  // Menampilkan hasil total yang dibulatkan
    }
}
