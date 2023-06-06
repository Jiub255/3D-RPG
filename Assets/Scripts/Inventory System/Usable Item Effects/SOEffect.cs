using UnityEngine;

public abstract class SOEffect : ScriptableObject
{
	// Have other Effect subclasses which send out events. Then whichever script(s) need to subscribe can. 
	public abstract void ExecuteEffect(SOUsableItem item);
}