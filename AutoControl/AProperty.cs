using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoControlLib
{
    abstract class AProperty<T> : IProperty
    {
        #region Constructors
        public AProperty(string name, PropertyGetter<T> getter, PropertySetter<T> setter)
        {
            Dirty = false;
            Name = name;
            Getter = getter;
            Setter = setter;
        }

        public AProperty(object obj, string displayName)
        {
            Dirty = false;
            Name = displayName;
            PropertyGetter<T> getter;
            PropertySetter<T> setter;
            PropertyDelegateInspector<T>.GetDelegates(obj, displayName, out getter, out setter);
            Getter = getter;
            Setter = setter;
        }
        #endregion

        #region IProperty
        public event PropertyEditEvent PropertyEdited = delegate { };

        public bool Dirty
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public abstract Control CreateEditor();

        public void Commit()
        {
            if (Dirty)
            {
                Setter(Value);
                Dirty = false;
            }
        }

        public void Reset()
        {
            if (Dirty)
            {
                Frozen = true;
                Value = Getter();
                Frozen = false;
                Dirty = false;
            }
        }
        #endregion

        #region Properties
        protected abstract T Value
        {
            get;
            set;
        }

        protected PropertyGetter<T> Getter
        {
            get;
            set;
        }

        protected PropertySetter<T> Setter
        {
            get;
            set;
        }

        private bool Frozen
        {
            get;
            set;
        }
        #endregion

        #region Helpers
        protected void onEdit()
        {
            if (!Frozen)
            {
                Dirty = true;
                PropertyEdited(this);
            }
        }
        #endregion
    }
}
