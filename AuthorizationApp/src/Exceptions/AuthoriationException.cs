namespace AuthorizationApp.src.Exceptions
{
    public class AuthoriationException : Exception
    {
        public AuthoriationException()
            :base("Неверный логин или пароль"){ }

    }
}
