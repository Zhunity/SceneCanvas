using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventExtension 
{
	public static bool IsLeftMouseButton(this Event e)
	{
		return e.isMouse && e.button == 0;
	}

	public static bool IsRightMouseButton(this Event e)
	{
		return e.isMouse && e.button == 1;
	}

	public static bool IsMiddleMouseButton(this Event e)
	{
		return e.isMouse && e.button == 2;
	}
}
