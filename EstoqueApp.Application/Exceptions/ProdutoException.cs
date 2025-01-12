namespace EstoqueApp.Application.Exceptions
{
    public class DeleteProdutoException : Exception
    {
        public DeleteProdutoException(string message)
            : base(message)
        {
        }
    }
}
