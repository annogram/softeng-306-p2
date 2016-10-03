using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts {
    /// <summary>
    ///  This interface should be implemented by everu element that will use a button press
    /// </summary>
    interface IButtonPress {
        /// <summary>
        ///  This function will be called by a button when it has been triggered
        /// </summary>
        /// <returns>Will return true if the opperation suceeded</returns>
        bool Trigger();

        /// <summary>
        /// This function will be called by a button when it has been untriggered
        /// </summary>
        /// <returns>True if the function succeeded to run, false if not</returns>
        bool UnTrigger();
    }
}
