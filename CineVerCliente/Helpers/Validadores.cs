using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CineVerCliente.Helpers
{
    public static class Validadores
    {
        public static bool ValidarCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                return false;
            }

            string correoPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            return Regex.IsMatch(correo, correoPattern);
        }

        public static bool ValidarContrasenia(string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(contrasenia))
            {
                return false;
            }

            string contraseniaPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$";
            return Regex.IsMatch(contrasenia, contraseniaPattern);
        }
    }
}
