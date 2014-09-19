using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoControlLib
{
    class EnumProperty : AProperty<object>
    {
        #region Class Members
        private Type _enumType;
        #endregion

        #region Constructors
        public EnumProperty(string name, Type enumType, PropertyGetter<object> getter, PropertySetter<object> setter)
            : base(name, getter, setter)
        {
            _enumType = enumType;
        }

        public EnumProperty(object obj, Type enumType, string displayName)
            : base(obj, displayName)
        {
            _enumType = enumType;
        }
        #endregion

        #region AProperty<object>
        protected override object Value
        {
            get { return ComboBox.SelectedValue; }
            set { ComboBox.SelectedValue = value; }
        }

        public override Control CreateEditor()
        {
            var items = Enum.GetValues(_enumType).Cast<object>()
                .Select(v => new {
                    Value = v,
                    Name = _enumType.GetMember(v.ToString()).First()
                        .GetMemberAttribute<DisplayNameAttribute>().DisplayName
                }).ToArray();
            ComboBox = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = items,
                DisplayMember = "Name",
                ValueMember = "Value",
                SelectedValue = Getter()
            };
            ComboBox.SelectedValueChanged += new EventHandler(ComboBox_SelectedValueChanged);
            return ComboBox;
        }
        #endregion

        #region Properties
        private ComboBox ComboBox
        {
            get;
            set;
        }
        #endregion

        #region Event Handlers
        private void ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            onEdit();
        }
        #endregion
    }
}
