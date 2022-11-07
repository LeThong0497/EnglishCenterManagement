namespace EnglishCenter.Business.Interfaces
{
    public interface IJwtAuthenticationManage
    {
      string  Authenticate(string userName, string passWord);
    }
}
