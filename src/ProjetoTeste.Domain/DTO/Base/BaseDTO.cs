namespace ProjetoTeste.Domain.DTO.Base
{
    public class BaseDTO<TDTO> where TDTO : BaseDTO<TDTO>
    {
        public long Id { get; set; }
    }
    public class BaseDTO_0 : BaseDTO<BaseDTO_0> { }
}