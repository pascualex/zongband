#nullable enable

using UnityEngine;
using UnityEngine.UIElements;

using Zongband.Game.Core;
using Zongband.Game.Entities;
using Zongband.Utils;

using ANE = System.ArgumentNullException;

namespace Zongband.UI
{
    public class AgentInspector : MonoBehaviour
    {
        public Tile MouseTile { private get; set; } = Tile.MinusOne;

        [SerializeField] private GameManager? GameManager;
        [SerializeField] private UIDocument? UIDocument;

        private VisualElement? Menu;
        private Label? Name;
        private VisualElement? HealthBar;
        private Label? Health;
        private Label? MaxHealth;
        private Label? TurnCD;
        private Label? HostileTag;
        private Agent? FixedAgent;

        private void Awake()
        {
            if (UIDocument == null) throw new ANE(nameof(UIDocument));
            var UI = UIDocument.rootVisualElement;
            if (UI == null) throw new ANE(nameof(UI));

            Menu = UI.Q<VisualElement>("agent-inspector");
            if (Menu == null) throw new ANE(nameof(Menu));
            Menu.style.display = DisplayStyle.None;

            Name = Menu.Q<Label>("name");
            Health = Menu.Q<Label>("health");
            HealthBar = Menu.Q<VisualElement>("health-bar");
            MaxHealth = Menu.Q<Label>("max-health");
            TurnCD = Menu.Q<Label>("turn-cd");
            HostileTag = Menu.Q<Label>("hostile-tag");
        }

        public void Refresh()
        {
            InspectAgent();
        }

        public void LockAgent()
        {
            if (GameManager == null) throw new ANE(nameof(GameManager));
            if (GameManager.Board == null) throw new ANE(nameof(GameManager.Board));

            FixedAgent = GameManager.Board.GetAgent(MouseTile);
        }

        private void InspectAgent()
        {
            if (GameManager == null) throw new ANE(nameof(GameManager));
            if (GameManager.Board == null) throw new ANE(nameof(GameManager.Board));
            if (Menu == null) throw new ANE(nameof(Menu));
            if (Name == null) throw new ANE(nameof(Menu));
            if (HealthBar == null) throw new ANE(nameof(HealthBar));
            if (Health == null) throw new ANE(nameof(Health));
            if (MaxHealth == null) throw new ANE(nameof(MaxHealth));
            if (TurnCD == null) throw new ANE(nameof(TurnCD));
            if (HostileTag == null) throw new ANE(nameof(HostileTag));

            var inspect = false;
            var agent = FixedAgent;
            if (agent == null || !agent.IsAlive) agent = GameManager.Board.GetAgent(MouseTile);
            if (agent != null)
            {
                Name.text = agent.Name;
                var progress = agent.MaxHealth > 0 ? (float)agent.Health / agent.MaxHealth : 0f;
                progress *= 100;
                HealthBar.style.width = new StyleLength(new Length(progress, LengthUnit.Percent));
                Health.text = agent.Health.ToString();
                MaxHealth.text = agent.MaxHealth.ToString();
                TurnCD.text = agent.TurnCooldown.ToString();
                HostileTag.style.display = agent.IsPlayer ? DisplayStyle.None : DisplayStyle.Flex;
                inspect = true;
            }

            Menu.style.display = inspect ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}