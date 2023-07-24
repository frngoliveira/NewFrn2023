using AutoMapper;
using FRN.Application._1._1_Interface;
using FRN.Domain._2._1_Interface;
using FRN.Domain._2._2_Entity;
using System.Data.SqlClient;

namespace FRN.Application._1._2_AppService
{
    public class ProdutoAppService: BaseService, IProdutoAppService 
    {
        private readonly IProductRepository _produtoRepository;

        public ProdutoAppService(IMapper mapper,
                                 IProductRepository produtoRepository,
                                 IDomainNotificationHandler notificator) : base (notificator, mapper)
        {
            _produtoRepository = produtoRepository;
        }

        public IEnumerable<Product> Get()
        {
            try
            {
                var result = _produtoRepository.Get();
                if (result == null)
                {
                    NotifyError("Não foram encontrados dados na Pesquisa");
                    return null;
                }
                return _mapper.Map<IEnumerable<Product>>(result);
            }
            catch (Exception ex)
            {
                if(ex is SqlException)
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

        public void Post(Product product)
        {
            try
            {
                _produtoRepository.Post(product);
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

        public void Put(Product product)
        {
            try
            {
                _produtoRepository.Put(product);
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
