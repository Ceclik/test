using UnityEngine;

public class CharacterSpriteSetter : MonoBehaviour
{
	//[SerializeField] private Sprite[] sprites;
	
	public void SetSprite()
	{
		//TODO make random sprite picker
		GetComponent<SpriteRenderer>().color = GenerateRandomColor();
	}

	private Color GenerateRandomColor()
	{
		float r = Random.Range(1,101) / 100.0f;
		float g = Random.Range(1,101) / 100.0f;
		float b = Random.Range(1,101) / 100.0f;
		
		Debug.Log($"Color: r:{r}, g:{g}, b:{b}");
		return new Color(r, g, b, 1);
	}
}
