using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Functions
{
    public class Conversiones
    {

        public static string LogicToString(int? valor)
        {
            if (valor.HasValue)
            {
                if (valor.Value == 1)
                    return "SI";
            }
            return "NO";
        }
    
        public static object GetValueByType(object valor, Type type)
        {
            if (type != typeof(string))
            {
                if (valor != null)
                {
                    type = Nullable.GetUnderlyingType(type) ?? type;
                    try
                    {
                        return Convert.ChangeType(valor, type);
                    }
                    catch
                    {
                        return Activator.CreateInstance(type);
                    }
                }
                else
                    return Activator.CreateInstance(type);
            }
            else
                return valor;
        }

        public static int? StringToIntNull(string valor)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                int entero;
                if (int.TryParse(valor, out entero))
                    return entero;
            }
            return null;
        }

        public static int? ObjectToIntNull(object valor)
        {
            if (valor != null)
            {
                int entero;
                if (int.TryParse(valor.ToString(), out entero))
                    return entero;

            }
            return null;

        }

        public static decimal? ObjectToDecimalNull(object valor)
        {
            if (valor != null)
            {
                decimal num;
                if (decimal.TryParse(valor.ToString().Replace(".", ","), out num))
                    return num;

            }
            return null;

        }

        public static DateTime? ObjectToDateTimeNull(object valor)
        {
            if (valor != null)
            {
                DateTime dt;
                if (DateTime.TryParse(valor.ToString(), out dt))
                    return dt;

            }
            return null;

        }
        public static long? ObjectToLongNull(object valor)
        {
            if (valor != null)
            {
                long dec;
                if (long.TryParse(valor.ToString().Replace(".", ","), out dec))
                    return dec;

            }
            return null;

        }
        public static bool? ObjectToBoolNull(object valor)
        {
            if (valor != null)
            {
                bool boo;
                if (bool.TryParse(valor.ToString(), out boo))
                    return boo;

            }
            return null;

        }
        public static String ObjectToString(object valor)
        {
            if (valor != null)
            {
                return valor.ToString();
            }
            return "";

        }
        public static object[] ObjectToObjectArray(object obj)
        {
            if (obj != null)
                return (object[])obj;
            return null;
        }



    }
}
