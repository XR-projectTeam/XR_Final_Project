using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
	public Slider healthSlider;

	public void UpdateHealthUI(int current, int max)
	{
		if (healthSlider != null)
		{
			healthSlider.maxValue = max;
			healthSlider.value = current;
		}
	}
}