namespace CodeHunt.API.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos utilizado para registrar nuevos usuarios
    /// en la plataforma. Contiene los datos básicos requeridos para la creación de cuenta.
    /// </summary>
    public class RegisterDTO
    {
        /// <summary>
        /// Nombre completo del usuario que se está registrando.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico único del usuario. Será usado como identificador principal.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña en texto plano. Se cifrará antes de almacenarla en la base de datos.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
