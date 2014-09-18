using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoControlLib
{
    class IntegerProperty : AProperty<int>
    {
        #region Constructors
        public IntegerProperty(string name, PropertyGetter<int> getter, PropertySetter<int> setter)
            : base(name, getter, setter)
        {
        }

        public IntegerProperty(object obj, string displayName)
            : base(obj, displayName)
        {
        }
        #endregion

        #region AProperty<int>
        protected override int Value
        {
            get { return (int)SpinBox.Value; }
            set { SpinBox.Value = value; }
        }

        public override Control CreateEditor()
        {
            SpinBox = new NumericUpDown();
            SpinBox.Minimum = int.MinValue;
            SpinBox.Maximum = int.MaxValue;
            SpinBox.Value = Getter();
            SpinBox.ValueChanged += new EventHandler(SpinBox_ValueChanged);
            return SpinBox;
        }
        #endregion

        #region Properties
        private NumericUpDown SpinBox
        {
            get;
            set;
        }
        #endregion

        #region Event Handlers
        private void SpinBox_ValueChanged(object sender, EventArgs e)
        {
            onEdit();
        }
        #endregion
    }
}
