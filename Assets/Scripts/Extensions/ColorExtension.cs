using UnityEngine;

public static class ColorExtension {

	public static Color ReturnNewColorThatsTheSameButWithDifferentAlphaDespacito(this Color c, float value) {
		var newC = new Color(c.r, c.g, c.b, value);

		return newC;
	}
}
