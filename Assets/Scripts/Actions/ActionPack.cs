using UnityEngine;
using System;
using System.Collections.Generic;

namespace Zongband.Actions
{
    public class ActionPack
    {
        private List<MovementAction> movementActions;

        public ActionPack()
        {
            movementActions = new List<MovementAction>();
        }

        public IReadOnlyList<MovementAction> GetMovementActions()
        {
            return movementActions.AsReadOnly();
        }

        public void AddMovementAction(MovementAction movementAction)
        {
            if (movementAction == null) throw new ArgumentNullException();
            movementActions.Add(movementAction);
        }
    }
}