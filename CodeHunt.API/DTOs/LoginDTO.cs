namespace CodeHunt.API.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos utilizado para enviar
    /// las credenciales del usuario al momento de iniciar sesión.
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Correo electrónico del usuario. Se usará como identificador único.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña en texto plano que será verificada en el backend
        /// (posteriormente se comparará con el hash en la base de datos).
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
