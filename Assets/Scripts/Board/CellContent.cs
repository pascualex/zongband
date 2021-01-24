using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CellContent {

    void Move(Vector2 to, float scale);

    void Remove();
}
