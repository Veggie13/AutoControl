using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoControlLib
{
    class StringProperty : AProperty<string>
    {
        #region Constructors
        public StringProperty(string name, PropertyGetter<string> getter, PropertySetter<string> setter)
            : base(name, getter, setter)
        {
        }

        public StringProperty(object obj, string displayName)
            : base(obj, displayName)
        {
        }
        #endregion

        #region AProperty<string>
        protected override string Value
        {
            get { return TextBox.Text; }
            set { TextBox.Text = value; }
        }

        public override Control CreateEditor()
        {
            TextBox = new TextBox();
            TextBox.Text = Getter();
            TextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            return TextBox;
        }
        #endregion

        #region Properties
        private TextBox TextBox
        {
            get;
            set;
        }
        #endregion

        #region Event Handlers
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            onEdit();
        }
        #endregion
    }
}
