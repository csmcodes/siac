using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class Enums
    {
        public enum GeneroCuenta
        {
            ACTIVO = 1,
            PASIVO = 2,
            PATRIMONIO = 3,
            INGRESOS = 4,
            COSTOS = 5,
            GASTOS = 6,
            GASTOSNO = 7,
            OTROS = 8,


        }

        public static string GetGeneroCuenta(int? genero)
        {
            string retorno = "";
            if (genero == (int)GeneroCuenta.ACTIVO)
                retorno = GeneroCuenta.ACTIVO.ToString();
            if (genero == (int)GeneroCuenta.PASIVO)
                retorno = GeneroCuenta.PASIVO.ToString();
            if (genero == (int)GeneroCuenta.PATRIMONIO)
                retorno = GeneroCuenta.PATRIMONIO.ToString();
            if (genero == (int)GeneroCuenta.INGRESOS)
                retorno = GeneroCuenta.INGRESOS.ToString();
            if (genero == (int)GeneroCuenta.COSTOS)
                retorno = GeneroCuenta.COSTOS.ToString();
            if (genero == (int)GeneroCuenta.GASTOS)
                retorno = GeneroCuenta.GASTOS.ToString();

            return retorno;
        }

        public enum EstadoRegistro
        {
            INACTIVO = 0,
            ACTIVO = 1,
            ANULADO = 9
        }

        public static string GetEstadoRegistro(int estado)
        {
            string retorno = "";
            if (estado == (int)EstadoRegistro.ACTIVO)
                retorno = EstadoRegistro.ACTIVO.ToString();
            if (estado == (int)EstadoRegistro.INACTIVO)
                retorno = EstadoRegistro.INACTIVO.ToString();
            if (estado == (int)EstadoRegistro.ANULADO)
                retorno = EstadoRegistro.ANULADO.ToString();
            return retorno;
        }
        public enum TipoBalance
        {
            ACUMULADO = 1,
            MENSUAL = 2


        }

        public static string GetTipoBalance(int estado)
        {
            string retorno = "";
            if (estado == (int)TipoBalance.ACUMULADO)
                retorno = TipoBalance.ACUMULADO.ToString();
            if (estado == (int)TipoBalance.MENSUAL)
                retorno = TipoBalance.MENSUAL.ToString();

            return retorno;
        }

        public enum EstadoElectronico
        {
            ENVIADO = 2,
            RECIBIDO = 3,
            DEVUELTO = 4,
            AUTORIZADO = 5,
            NOAUTORIZADO = 6,
            ANULADO = 7
            

        }

        public static string GetEstadoElectronico(int? estado)
        {
            string retorno = "";
            if (estado == (int)EstadoElectronico.ENVIADO)
                retorno = EstadoElectronico.ENVIADO.ToString();
            if (estado == (int)EstadoElectronico.RECIBIDO)
                retorno = EstadoElectronico.RECIBIDO.ToString();
            if (estado == (int)EstadoElectronico.DEVUELTO)
                retorno = EstadoElectronico.DEVUELTO.ToString();
            if (estado == (int)EstadoElectronico.AUTORIZADO)
                retorno = EstadoElectronico.AUTORIZADO.ToString();
            if (estado == (int)EstadoElectronico.NOAUTORIZADO)
                retorno = EstadoElectronico.NOAUTORIZADO.ToString();
            if (estado == (int)EstadoElectronico.ANULADO)
                retorno = EstadoElectronico.ANULADO.ToString();
            return retorno;
        }

        public enum AplicaRetencion
        {
            SUBTOTAL0,
            SUBTOTALIVA,
            IVA


        }
    }
}
