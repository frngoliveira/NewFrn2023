using AutoMapper;
using FRN.Application._1._1_Interface;
using FRN.Domain._2._1_Interface;
using FRN.Domain._2._2_Entity;
using System.Data.SqlClient;

namespace FRN.Application._1._2_AppService
{
    public class UserAppService : BaseService, IUserAppService
    {
        private readonly IUserRepository _userRepository;
        public UserAppService(IMapper mapper,
                                 IUserRepository userRepository,
                                 IDomainNotificationHandler notificator) : base(notificator, mapper)
        {
            _userRepository = userRepository;
        }
        public Users Get(Users user)
        {
            try
            {
                var result = _userRepository.Get(user);                
                return _mapper.Map<Users>(result);
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    NotifyError(ex.Message.ToString());
                    return null;
                }
                else
                {
                    NotifyError(ex.Message.ToString());
                    return null;
                }
            }
        }

        public IEnumerable<Users> GetAllUser()
        {
            try
            {
                var result = _userRepository.GetAllUser();
                if (result == null)
                {
                    NotifyError("Não foram encontrados dados na Pesquisa");
                    return null;
                }
                return _mapper.Map<IEnumerable<Users>>(result);
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    NotifyError(ex.Message.ToString());
                    return null;
                }
                else
                {
                    NotifyError(ex.Message.ToString());
                    return null;
                }
            }
        }

        public IEnumerable<Users> GetAllUserById(int id)
        {
            try
            {
                var result = _userRepository.GetAllUserById(id);

                if (result == null)
                {
                    NotifyError("Não foram encontrados dados na Pesquisa");
                    return null;
                }
                return _mapper.Map<IEnumerable<Users>>(result);
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    NotifyError(ex.Message.ToString());
                    return null;
                }
                else
                {
                    NotifyError(ex.Message.ToString());
                    return null;
                }
            }
        }

        public void Post(Users user)
        {
            try
            {
                _userRepository.Post(user);
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    NotifyError(ex.Message.ToString());
                }
                else
                {
                    NotifyError(ex.Message.ToString());
                }
            }
        }

        public void Put(Users user)
        {
            try
            {
                _userRepository.Put(user);
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    NotifyError(ex.Message.ToString());
                }
                else
                {
                    NotifyError(ex.Message.ToString());
                }
            }
        }

        public  void Delete(Users user)
        {
            try
            {
                _userRepository.Delete(user);
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    NotifyError(ex.Message.ToString());
                }
                else
                {
                    NotifyError(ex.Message.ToString());
                }
            }
        }
    }
}
