using UnityEngine;
using System.Collections;

public class Escudos : MonoBehaviour {

    private SpriteRenderer miSprite;
    public float segundosEscudo = 0.15f;

	// Use this for initialization
	void Start ()
    {
        miSprite = GetComponent<SpriteRenderer>();
        StartCoroutine("TimerEscudos");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator TimerEscudos()
    {
        yield return new WaitForSeconds(1.0f);
        miSprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(segundosEscudo);
        miSprite.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(segundosEscudo);
        miSprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(segundosEscudo);
        miSprite.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(segundosEscudo);
        miSprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(segundosEscudo);
        miSprite.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(segundosEscudo);
        gameObject.SetActive(false);
    }
}
