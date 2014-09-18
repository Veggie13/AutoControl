using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoControlLib
{
    public partial class AutoControl : UserControl
    {
        #region Class Members
        private IProperty[] _properties;
        #endregion

        public AutoControl()
        {
            InitializeComponent();
        }

        #region Events
        public delegate void DirtyChangeEvent(AutoControl ctrl);
        public event DirtyChangeEvent DirtyChanged = delegate { };
        #endregion

        #region Properties
        private bool _autoCommit = true;
        public bool AutoCommit
        {
            get { return _autoCommit; }
            set
            {
                if (!_autoCommit && value)
                {
                    Commit();
                }
                _autoCommit = value;
            }
        }

        private object _dataSource = null;
        public object DataSource
        {
            get { return _dataSource; }
            set
            {
                if (_dataSource == value)
                {
                    return;
                }

                if (_dataSource != null)
                {
                    detach();
                }

                if (value != null)
                {
                    attach(value);
                }

                _dataSource = value;
            }
        }

        private bool _wasDirty = false;
        public bool Dirty
        {
            get { return _properties.Any(p => p.Dirty); }
        }
        #endregion

        #region Public Methods
        public void Commit()
        {
            bool any = false;
            foreach (var prop in _properties)
            {
                if (prop.Dirty)
                {
                    any = true;
                    prop.Commit();
                }
            }

            if (any && _wasDirty)
            {
                DirtyChanged(this);
                _wasDirty = false;
            }
        }

        public void Reset()
        {
            bool any = false;
            foreach (var prop in _properties)
            {
                if (prop.Dirty)
                {
                    any = true;
                    prop.Reset();
                }
            }

            if (any && _wasDirty)
            {
                DirtyChanged(this);
                _wasDirty = false;
            }
        }
        #endregion

        #region Event Handlers
        private void prop_PropertyEdited(IProperty property)
        {
            if (AutoCommit)
            {
                Commit();
            }
            else if (!_wasDirty)
            {
                DirtyChanged(this);
                _wasDirty = true;
            }
        }
        #endregion

        #region Private Helpers
        private void attach(object obj)
        {
            _properties = obj.GetProperties();
            foreach (var prop in _properties)
            {
                prop.PropertyEdited += new PropertyEditEvent(prop_PropertyEdited);
                _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                _mainLayout.RowCount++;
                _mainLayout.Controls.Add(new Label()
                {
                    Text = prop.Name,
                    Anchor = AnchorStyles.Left,
                    TextAlign = ContentAlignment.MiddleLeft
                });
                Control editor = prop.CreateEditor();
                editor.Dock = DockStyle.Fill;
                _mainLayout.Controls.Add(editor);
            }
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 1f));
            _mainLayout.RowCount++;
        }

        private void detach()
        {
            foreach (var prop in _properties)
            {
                prop.PropertyEdited -= prop_PropertyEdited;
            }
            _properties = null;
            _mainLayout.Controls.Clear();
            _mainLayout.RowStyles.Clear();
        }
        #endregion
    }
}
