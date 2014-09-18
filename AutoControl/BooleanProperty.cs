using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoControlLib
{
    class BooleanProperty : AProperty<bool>
    {
        #region Constructors
        public BooleanProperty(string name, PropertyGetter<bool> getter, PropertySetter<bool> setter)
            : base(name, getter, setter)
        {
        }

        public BooleanProperty(object obj, string displayName)
            : base(obj, displayName)
        {
        }
        #endregion

        #region AProperty<bool>
        protected override bool Value
        {
            get { return CheckBox.Checked; }
            set { CheckBox.Checked = value; }
        }

        public override Control CreateEditor()
        {
            CheckBox = new CheckBox();
            CheckBox.Text = "";
            CheckBox.Checked = Getter();
            CheckBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
            return CheckBox;
        }
        #endregion

        #region Properties
        private CheckBox CheckBox
        {
            get;
            set;
        }
        #endregion

        #region Event Handlers
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            onEdit();
        }
        #endregion
    }
}
