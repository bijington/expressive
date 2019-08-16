using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Expressive.Playground
{
    public class VariableDescriptor : INotifyPropertyChanged
    {
        #region Fields

        private string _name;
        private VariableType _type;
        private string _value;

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set { this.SetField(ref _name, value); }
        }

        public VariableType Type
        {
            get { return _type; }
            set { this.SetField(ref _type, value); }
        }

        internal object TypedValue
        {
            get
            {
                switch (_type)
                {
                    case VariableType.Number:
                        return string.IsNullOrWhiteSpace(_value) ? (double?) null : Convert.ToDouble(_value);
                    case VariableType.String:
                        return _value as string;
                    case VariableType.Date:
                        return Convert.ToDateTime(_value);
                    case VariableType.Boolean:
                        return Convert.ToBoolean(_value);
                    case VariableType.Null:
                        return null;
					case VariableType.Expression:
						return new Expression(_value as string);
                    case VariableType.None:
                    default:
                        return null;
                }
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                this.SetField(ref _value, value);
                
                // Let's see if we can auto detect the type.
                if (_type == VariableType.None)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        DateTime date = DateTime.MinValue;
                        int intValue = 0;
                        long longValue = 0;
                        double doubleValue = 0d;
                        decimal decimalValue = 0M;
                        bool boolValue = false;

                        if (int.TryParse(value, out intValue))
                        {
                            this.Type = VariableType.Number;
                        }
                        else if (long.TryParse(value, out longValue))
                        {
                            this.Type = VariableType.Number;
                        }
                        else if (double.TryParse(value, out doubleValue))
                        {
                            this.Type = VariableType.Number;
                        }
                        else if (decimal.TryParse(value, out decimalValue))
                        {
                            this.Type = VariableType.Number;
                        }
                        else if (bool.TryParse(value, out boolValue))
                        {
                            this.Type = VariableType.Boolean;
                        }
                        else if (DateTime.TryParse(value, out date))
                        {
                            this.Type = VariableType.Date;
                        }
                        else
                        {
                            this.Type = VariableType.String;
                        }
                    }
                }
            }
        }

        #endregion

        internal static VariableDescriptor FromXml(XElement xml)
        {
            var returnVal = new VariableDescriptor();

            returnVal.Name = xml.Attribute("Name")?.Value;
            returnVal.Type = xml.SafeExtractAttributeValue("Type", i => (VariableType)int.Parse(i));
            returnVal.Value = xml.Attribute("Value")?.Value;

            return returnVal;
        }

        internal XElement ToXml()
        {
            return new XElement("Variable",
                                new XAttribute("Name", this.Name ?? string.Empty),
                                new XAttribute("Value", this.Value ?? string.Empty),
                                new XAttribute("Type", (int)this.Type));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetField<T>(ref T field, T valueField, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, valueField)) { return false; }
            field = valueField;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public enum VariableType
    {
        None = 0,
        Number = 1,
        String = 2,
        Date = 3,
        Boolean = 4,
        Null = 5,
    }
}
