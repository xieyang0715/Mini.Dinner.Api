using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Model
{
    [Serializable]
    public struct Uuid
    {
        private string _value;

        private string Value
        {
            get
            {
                if (string.IsNullOrEmpty(_value))
                {
                    return Guid.Empty.ToString("N");
                }

                return _value;
            }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// <see cref="T:System.Uuid"/> 结构的只读实例，其值均为零。
        /// </summary>
        public static Uuid Empty
        {
            get { return new Uuid(Guid.Empty.ToString("N")); }
        }

        /// <summary>
        /// 初始化 <see cref="T:System.Uuid"/> 结构的新实例。
        /// </summary>
        /// 
        /// <returns>
        /// 一个新的 Uuid 对象。
        /// </returns>
        public static Uuid NewUuid()
        {
            return new Uuid(Guid.NewGuid().ToString("N"));
        }

        /// <summary>
        /// 返回 <see cref="T:System.String"/> 的此实例；不执行实际转换。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// 使用指定字符串所表示的值初始化 <see cref="T:System.Uuid"/> 类的新实例。
        /// </summary>
        /// <param name="value"></param>
        public Uuid(string value)
            : this()
        {
            if (value == null)
            {
                throw new ArgumentNullException("");
            }
            Value = value;
        }



        public bool Equals(Uuid other)
        {
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Uuid && Equals((Uuid)obj);
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }

        public static implicit operator string(Uuid value)
        {
            return value.ToString();
        }

        public static explicit operator Uuid(string value)
        {
            return new Uuid(value);
        }

        public static bool operator ==(Uuid a, Uuid b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(Uuid a, Uuid b)
        {
            return a.Value != b.Value;
        }

        /// <summary>
        /// 将 UUID 的字符串表示形式转换为等效的 <see cref="T:System.Uuid"/> 结构。
        /// </summary>
        /// 
        /// <returns>
        /// 如果分析操作成功，则为 true；否则为 false。
        /// </returns>
        public static bool TryParse(string g, out Uuid result)
        {
            result = Uuid.Empty;

            if (g == null)
            {
                return false;
            }
            var uuidString = g.Trim();

            if (uuidString.Length != 32)
            {
                return false;
            }

            for (var i = 0; i < uuidString.Length; i++)
            {
                var ch = uuidString[i];
                if (ch >= '0' && ch <= '9')
                {
                    continue;
                }

                var upperCaseCh = char.ToUpper(ch, CultureInfo.InvariantCulture);
                if (upperCaseCh >= 'A' && upperCaseCh <= 'F')
                {
                    continue;
                }

                return false;
            }

            result = new Uuid(g);

            return true;
        }
    }
}
