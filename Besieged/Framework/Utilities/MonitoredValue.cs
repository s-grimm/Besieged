using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utilities
{
    public class MonitoredValue<T>
    {
        public delegate void ValueChangedHandler(T from, T to);
        public event ValueChangedHandler ValueChanged;

        private T m_Value;
        public T Value
        {
            get { return m_Value; }
            set
            {
                if (ValueChanged != null) // if invocation list is not empty, fire the event
                {
                    var temp = m_Value;
                    m_Value = value;
                    ValueChanged(temp, value);
                }
                m_Value = value;
            }
        }

        public MonitoredValue() { }

        public MonitoredValue(T initialValue)
        {
            m_Value = initialValue;
        }
    }
}
