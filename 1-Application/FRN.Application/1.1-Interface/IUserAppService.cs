using FRN.Domain._2._2_Entity;

namespace FRN.Application._1._1_Interface
{
    public interface IUserAppService
    {
        Users Get(Users user);
        IEnumerable<Users> GetAllUser();
        void Post(Users user);
        void Put(Users user);
        IEnumerable<Users> GetAllUserById(int id);
        void Delete(Users user);
    }
}
