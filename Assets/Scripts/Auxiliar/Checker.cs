using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker {
    
    private Checker() { }

    static public bool Range(float value, float max) {
        return Range(value, 0, max);
    }

    static public bool Range(float value, float min, float max) {
        return value >= 0 && value < max;
    }

    static public bool Range(Vector2 value, Vector2 max) {
        return Range(value, Vector2.zero, max);
    }

    static public bool Range(Vector2 value, Vector2 min, Vector2 max) {
        return Range(value.x, min.x, max.x) && Range(value.y, min.y, max.y);
    }
}
