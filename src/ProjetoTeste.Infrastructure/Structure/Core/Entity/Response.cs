namespace ProjetoEstagioAPI.Models
{
    public class Response<TEntity>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public TValue? Value { get; set; }
    }
}