using BrainThud.Web.Model;

namespace BrainThud.Web.Helpers
{
    public interface IUserHelper 
    {
        Configuration CreateUserConfiguration(string nameIdentifier);
    }
}