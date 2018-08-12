using UnityEngine;

public class OChair : MonoBehaviour {

    public ChairObject chairObject;
    public Vector2 location;

    private TableGrid grid;
    

	// Use this for initialization
	void Start () {

        GetComponent<SpriteRenderer>().sprite = chairObject.sprite;
        grid = GetComponentInParent<TableGrid>();

        transform.position = grid.GetWorldPositionOfGrid(location, chairObject.middleGridx, chairObject.middleGridY);    
    }
}
