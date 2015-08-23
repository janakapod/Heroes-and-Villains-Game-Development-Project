using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public void OnDrop(PointerEventData eventdata){
		Draggble d = eventdata.pointerDrag.GetComponent<Draggble> ();

		if (d != null) {
			d.parentToReturnTo = this.transform;
		}
	}

	public void OnPointerEnter(PointerEventData eventData){
	
	}

	public void OnPointerExit(PointerEventData eventData){
		
	}
}
