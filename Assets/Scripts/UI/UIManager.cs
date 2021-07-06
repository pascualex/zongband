// #nullable enable

// using UnityEngine;

// using Zongband.Utils;

// using ANE = System.ArgumentNullException;

// namespace Zongband.UI
// {
//     public class UIManager : MonoBehaviour
//     {
//         public Tile MouseTile { get; private set; } = Tile.MinusOne;

//         [SerializeField] private Camera? MainCamera;
//         [SerializeField] private CameraController? CameraController;
//         [SerializeField] private TileHighlighter? TileHighlighter;
//         [SerializeField] private PlayerHighlighter? PlayerHighlighter;
//         [SerializeField] private AgentInspector? AgentInspector;

//         private Vector2 MousePosition = new Vector2(-1, -1);

//         public void Refresh()
//         {
//             if (CameraController == null) throw new ANE(nameof(CameraController));
//             if (TileHighlighter == null) throw new ANE(nameof(TileHighlighter));
//             if (PlayerHighlighter == null) throw new ANE(nameof(PlayerHighlighter));
//             if (AgentInspector == null) throw new ANE(nameof(AgentInspector));

//             TileHighlighter.Refresh();
//             PlayerHighlighter.Refresh();
//             AgentInspector.Refresh();

//             CameraController.Refresh();
//             UpdateMouseTile();
//         }

//         public void SetMousePosition(Vector2 mousePosition)
//         {
//             MousePosition = mousePosition;
//             UpdateMouseTile();
//         }

//         public void HandleMouseLeftClick()
//         {

//         }
        
//         public void HandleMouseRightClick()
//         {

//         }

//         public void HandleCtrlMouseLeftClick()
//         {
//             if (AgentInspector == null) throw new ANE(nameof(AgentInspector));

//             AgentInspector.LockAgent();
//         }

//         private void UpdateMouseTile()
//         {
//             if (MainCamera == null) throw new ANE(nameof(MainCamera));

//             var ray = MainCamera.ScreenPointToRay(MousePosition);
//             var plane = new Plane(Vector3.up, Vector3.zero);

//             MouseTile = Tile.MinusOne;
//             if (plane.Raycast(ray, out var distance))
//             {
//                 var position = ray.GetPoint(distance);
//                 MouseTile = new Tile((int)position.x, (int)position.z);
//             }

//             if (TileHighlighter != null) TileHighlighter.MouseTile = MouseTile;
//             if (AgentInspector != null) AgentInspector.MouseTile = MouseTile;
//         }
//     }
// }