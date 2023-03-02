namespace NeoBancoDemo.Business
{
    public class MovimientoBusiness
    {

        public static decimal RealizarOperacionCredito(decimal saldoCuenta, decimal valorMovimiento)
        {
            if (saldoCuenta-valorMovimiento >=0)
            {
                return saldoCuenta - valorMovimiento;
            }
            else
            {
                throw new ArgumentException("Saldo no disponible");
            }
            
        }

        public static decimal RealizarOperacionDebito(decimal saldoCuenta, decimal valorMovimiento)
        {
            return saldoCuenta + valorMovimiento;
        }
    }
}