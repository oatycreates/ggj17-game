using UnityEngine;
using System.Collections;

/*public class CloudScrollV3 : MonoBehaviour{
	public float scrollSpeed;
	public float tileSizeZ;
	private Vector3 startPosition;

	void Start(){
		startPosition = transform.position;
	}

	void Update(){
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}*/
public class CloudScrollV3 : MonoBehaviour{
	public float scrollSpeed;
	private Vector2 savedOffset;

	void Start(){
		savedOffset = GetComponent<Renderer> ().sharedMaterial.GetTextureOffset ("_MainTex");
	}

	void Update(){
		float y = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (y, savedOffset.x);
		GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}

	void OnDisable(){
		GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}



/*public class CloudScrollV3 : MonoBehaviour {
	public float scrollSpeed;
	public float tileSizeZ;
	private Vector2 savedOffset;
	private Vector3 startPosition;

	void Start () {
		startPosition = transform.position;
		savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset ("_MainTex");
	}

	void Update(){
		float x = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ * 4);
		x = x / tileSizeZ;
		x = Mathf.Floor (x);
		x = x / 4;
		Vector2 offset = new Vector2 (x, savedOffset.y);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.back * newPosition;
	}

	void OnDisable(){
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}*/