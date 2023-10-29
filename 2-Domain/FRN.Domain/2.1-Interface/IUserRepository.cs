using FRN.Domain._2._2_Entity;

namespace FRN.Domain._2._1_Interface
{
    public interface IUserRepository
    {
        Users Get(Users user);
        IEnumerable<Users> GetAllUser();
        void Post(Users user);
        void Put(Users user);
        IEnumerable<Users> GetAllUserById(int id);
        void Delete(Users user);

    }
}
